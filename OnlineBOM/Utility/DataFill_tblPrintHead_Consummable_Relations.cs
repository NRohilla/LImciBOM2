using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication3.Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            List<tblRelationsQuery> PrintHeadQuery = new List<tblRelationsQuery>();
            List<tblPrntHD_Consmbl_Relations> relations = new List<tblPrntHD_Consmbl_Relations>();
            List<datatabletolist> objdatatabletolist = new List<datatabletolist>();
            DataSet dataSet = new DataSet();
            int TotalRecordsInserted = 0;

            //PrintHeadQuery.Add(new tblRelationsQuery("PH1", "PH2", "PH3", "Consummable No", "Series"));
            #region //1010 Ink Series 
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1010", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1010", "8800"));
            #endregion
            #region //1014 Ink Series
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "plus", "1014", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "plus", "1014", "8800"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1014", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1014", "8800"));
            #endregion
            #region //1016 Ink Series
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1016", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1016", "8800"));
            #endregion
            #region //1018 Ink Series
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1018", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1018", "8800"));
            #endregion
            #region 1240 Ink Series
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "plus", "1240", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "plus", "1240", "8800"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1240", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1240", "8800"));
            #endregion
            #region 1243 Ink Series
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1243", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1243", "8800"));
            #endregion
            #region 1248 Ink Series
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "plus", "1248", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "plus", "1248", "8800"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1248", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1248", "8800"));
            #endregion
            #region 1281 Ink Series
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1281", "8900"));
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1281", "8800"));
            #endregion
            #region 1290 Ink Series
            PrintHeadQuery.Add(new tblRelationsQuery("mk11", "MIDI", "", "1290", "8900"));
            #endregion
            System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(@"data source=NITINR;Initial Catalog=OnlineBOM;User ID=sa;Password=pass@123;");
            sqlConn.Open();

            using (SqlDataAdapter dataAdapter
                    = new SqlDataAdapter("select * from BOMItems", sqlConn))
            {
                // fill the DataSet using our DataAdapter 
                dataAdapter.Fill(dataSet);

                objdatatabletolist = (from DataRow row in dataSet.Tables[0].Rows
                                      select new datatabletolist
                                      {
                                          ItemID = row["ITEMID"].ToString(),
                                          Desc = row["DESCRIPTION"].ToString().ToLower()

                                      }).ToList();


            }

            foreach (var item in PrintHeadQuery)
            {
                //Get All the Printheads 
                var GetPrinthead = objdatatabletolist.Where(p => p.Desc.Contains("printhead") && p.Desc.Contains(item.printhead1.ToLower())
                   && p.Desc.Contains(item.printhead2.ToLower())).ToList();

                if (!string.IsNullOrEmpty(item.printhead3))
                    GetPrinthead = GetPrinthead.Where(p => p.Desc.Contains(item.printhead3.ToLower())).ToList();

                //"7900,5900,5900DC"
                if (item.Series.Contains(","))
                {
                    foreach (var itemMoreSeries in item.Series.Split(','))
                    {
                        //Get All the consummables
                        var GetConsummable = objdatatabletolist.Where(p => p.Desc.Contains("ink") && p.Desc.Contains(item.CnsmmableQury.ToLower()) && p.Desc.Contains(itemMoreSeries.ToLower())).ToList();
                        foreach (var itemConsumm in GetConsummable)
                        {
                            foreach (var itemPrintHeads in GetPrinthead)
                            {
                                relations.Add(new tblPrntHD_Consmbl_Relations(itemPrintHeads.ItemID, itemPrintHeads.Desc, itemConsumm.ItemID, itemConsumm.Desc));
                            }
                        }
                    }
                }
                else
                {
                    //Get All the consummables
                    var GetConsummable = objdatatabletolist.Where(p => p.Desc.Contains("ink") && p.Desc.Contains(item.CnsmmableQury.ToLower()) && p.Desc.Contains(item.Series)).ToList();
                    foreach (var itemPrintHeads in GetPrinthead)
                    {
                        foreach (var itemConsumm in GetConsummable)
                        {
                            relations.Add(new tblPrntHD_Consmbl_Relations(itemPrintHeads.ItemID, itemPrintHeads.Desc, itemConsumm.ItemID, itemConsumm.Desc));
                        }
                    }
                }
            }

            foreach (var itemRelations in relations)
            {
                SqlCommand Cmd = new SqlCommand("insert into tblPrntHD_Consmbl_Relations values('" + itemRelations.PrintHeadID + "','" + itemRelations.PrintHeadDesc + "','" + itemRelations.ConsummableID + "','" + itemRelations.ConsummableDesc + "')", sqlConn);
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                TotalRecordsInserted += Cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Total " + TotalRecordsInserted + " records inserted. Please click any key to continue!");
        }
    }

    class tblPrntHD_Consmbl_Relations
    {
        public tblPrntHD_Consmbl_Relations(string prnthd, string PrintHDDesc, string cnsmmbl, string ConsmDesc)
        {
            PrintHeadID = prnthd;
            PrintHeadDesc = PrintHDDesc;
            ConsummableID = cnsmmbl;
            ConsummableDesc = ConsmDesc;
        }

        public string ConsummableID { get; set; }
        public string ConsummableDesc { get; set; }
        public string PrintHeadID { get; set; }
        public string PrintHeadDesc { get; set; }
    }

    class tblRelationsQuery
    {
        public tblRelationsQuery(string prnthd1, string prnthd2, string prnthd3, string cnsmmbl, string series)
        {
            printhead1 = prnthd1;
            printhead2 = prnthd2;
            printhead3 = prnthd3;
            CnsmmableQury = cnsmmbl;
            Series = series;
        }

        public string printhead1 { get; set; }
        public string printhead2 { get; set; }
        public string printhead3 { get; set; }
        public string CnsmmableQury { get; set; }
        public string Series { get; set; }
    }

    class datatabletolist
    {

        public string ItemID { get; set; }
        public string Desc { get; set; }
    }
}