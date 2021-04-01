USE [master]
GO
/****** Object:  Database [OnlineBOM]    Script Date: 4/1/2021 11:35:53 PM ******/
CREATE DATABASE [OnlineBOM] ON  PRIMARY 
( NAME = N'OnlineBOM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\OnlineBOM.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineBOM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\OnlineBOM_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineBOM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineBOM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineBOM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineBOM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineBOM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineBOM] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineBOM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineBOM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineBOM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineBOM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineBOM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineBOM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineBOM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineBOM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineBOM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineBOM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OnlineBOM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineBOM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineBOM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineBOM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineBOM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineBOM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineBOM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineBOM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OnlineBOM] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineBOM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineBOM] SET DB_CHAINING OFF 
GO
USE [OnlineBOM]
GO
/****** Object:  User [test]    Script Date: 4/1/2021 11:35:54 PM ******/
CREATE USER [test] FOR LOGIN [test] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Assembly]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assembly](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[vwProductsID] [bigint] NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[AssemblyCategoryID] [int] NOT NULL,
	[Device] [nvarchar](50) NULL,
 CONSTRAINT [PK_CapitalAssemblies] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssemblyCategory]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssemblyCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AssemblyCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOM]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BOM] [nvarchar](50) NOT NULL,
	[IsCustomParts] [bit] NULL,
 CONSTRAINT [PK_BOM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOMItems]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOMItems](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL,
	[SALEPRICE] [numeric](32, 16) NULL,
	[SALEUNIT] [nvarchar](10) NULL,
	[PRODTYPE] [varchar](10) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOMTemplate]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOMTemplate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BOM_ID] [int] NOT NULL,
	[BOMItem_ID] [bigint] NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [decimal](18, 1) NOT NULL,
	[IsQtyFixed] [bit] NOT NULL,
	[IsDiscountApply] [bit] NOT NULL,
	[MaximumQty] [decimal](18, 1) NOT NULL,
	[AssemblyID] [int] NULL,
	[IsInTotal] [bit] NOT NULL,
	[IsDecimalAllowed] [bit] NOT NULL,
	[ParentBOMItemID] [bigint] NULL,
 CONSTRAINT [PK_BomTemplate_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOMtoAssembly]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOMtoAssembly](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BOMid] [int] NOT NULL,
	[AssemblyID] [bigint] NOT NULL,
	[Device_ID] [bigint] NOT NULL,
	[PMPercentage] [numeric](18, 4) NOT NULL,
 CONSTRAINT [PK_BOMtoAssembly] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configuration_DEL]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuration_DEL](
	[ProjectMangementFee] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImplementationAssembly]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImplementationAssembly](
	[ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InternalBOMItems]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalBOMItems](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL,
 CONSTRAINT [PK_InternalBOMItems] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryOnHand]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryOnHand](
	[WAREHOUSE] [nvarchar](10) NULL,
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL,
	[ITEMGROUPID] [nvarchar](10) NULL,
	[AVAILPHYSICAL] [numeric](38, 16) NULL,
	[RESERVPHYSICAL] [numeric](38, 16) NULL,
	[TOTALPHYSICAL] [numeric](38, 16) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemCategory]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemCategory](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[ItemOrder] [int] NULL,
 CONSTRAINT [PK_BOMItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemSubCategory]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemSubCategory](
	[ItemCategory] [bigint] NOT NULL,
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ItemOrder] [int] NULL,
 CONSTRAINT [PK_ItemSubCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeadTimeForAssembly]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeadTimeForAssembly](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AssemblyID] [bigint] NOT NULL,
	[LeadTimeDays] [int] NOT NULL,
	[BOMID] [int] NOT NULL,
 CONSTRAINT [PK_LeadTimeForAssembly] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Opportunity]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opportunity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Opportunity] [nvarchar](1000) NULL,
	[CustomerCode] [nvarchar](100) NULL,
	[ClosedDate] [datetime] NULL,
	[Representative] [nvarchar](100) NULL,
	[CompanyName] [nvarchar](500) NULL,
	[Address] [nvarchar](1000) NULL,
	[CustomerType] [nvarchar](100) NULL,
	[DeliveryDate] [datetime] NULL,
	[QuoteNo] [nvarchar](100) NULL,
	[PONumber] [nvarchar](50) NULL,
	[Authorisation] [nvarchar](100) NULL,
	[Campaign] [nvarchar](50) NULL,
	[CampaignCode] [nvarchar](50) NULL,
	[Territory1ID] [nvarchar](50) NULL,
	[Territory1Split] [int] NULL,
	[Territory2ID] [nvarchar](50) NULL,
	[Territory2Split] [int] NULL,
	[DispatchAddress] [nvarchar](500) NULL,
	[AccountContactName] [nvarchar](100) NULL,
	[AccountContactTitle] [nvarchar](100) NULL,
	[AccountContactPhoneNo] [nvarchar](100) NULL,
	[AccountContactEmail] [nvarchar](50) NULL,
	[FinanceDeal] [nvarchar](100) NULL,
	[FinanceType] [nvarchar](100) NULL,
	[FinanceApproved] [nvarchar](50) NULL,
	[FinanceTotalAmount] [decimal](18, 2) NULL,
	[FinancePeriod] [int] NULL,
	[InkUsage] [nvarchar](50) NULL,
	[SolventUsage] [nvarchar](50) NULL,
	[Comments] [nvarchar](1000) NULL,
	[SalesPerson] [nvarchar](100) NULL,
	[CHOPComments] [nvarchar](1000) NULL,
	[LeadTime] [int] NOT NULL,
	[PMComments] [nvarchar](1000) NULL,
	[DateTimeStamp] [datetime] NULL,
	[SaleTypeID] [int] NULL,
 CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OpportunityBOMList]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpportunityBOMList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OpportunityID] [int] NOT NULL,
	[BOMID] [int] NOT NULL,
	[BOMItemsID] [bigint] NOT NULL,
	[Qty] [decimal](18, 1) NOT NULL,
	[ItemPrice] [decimal](18, 2) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CustomDescription] [nvarchar](100) NULL,
	[CustomCode] [nvarchar](50) NULL,
	[Discount] [decimal](18, 2) NULL,
	[FinalAgreedPrice] [decimal](18, 2) NULL,
	[IsDiscountApply] [bit] NULL,
	[PriceAfterDiscount] [numeric](18, 2) NULL,
	[State] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[MaximumQty] [decimal](18, 1) NULL,
	[UpdatedDatetime] [datetime] NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[IsInTotal] [bit] NOT NULL,
	[IsDecimalAllowed] [bit] NOT NULL,
	[IsDeleted] [bit] NULL,
	[VersionNum] [int] NULL,
 CONSTRAINT [PK_OpportunityBOMList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParentChildPart]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentChildPart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [bigint] NOT NULL,
	[ChildID] [bigint] NOT NULL,
 CONSTRAINT [PK_ParentChildPart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartToCapitalAssembly_DEL]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartToCapitalAssembly_DEL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PartID] [bigint] NOT NULL,
	[AssemblyID] [bigint] NOT NULL,
 CONSTRAINT [PK_PartToCapitalAssembly] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartToCategory]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartToCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PartID] [bigint] NOT NULL,
	[CategoryID] [bigint] NOT NULL,
	[SubCategoryID] [int] NULL,
 CONSTRAINT [PK_PartToCategory] PRIMARY KEY CLUSTERED 
(
	[PartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PMFeeNotChargable]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PMFeeNotChargable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AssemblyID] [bigint] NOT NULL,
 CONSTRAINT [PK_PMFeeChargable_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PMFeeNotChargable_DEL]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PMFeeNotChargable_DEL](
	[PartID] [bigint] NOT NULL,
	[Charge] [bit] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_pmFeeChargable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectMilestones]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMilestones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OpportunityID] [int] NOT NULL,
	[BOMID] [int] NOT NULL,
	[DepositPerc] [decimal](18, 2) NOT NULL,
	[Deposit] [decimal](18, 2) NOT NULL,
	[PreDeliveryPerc] [decimal](18, 2) NOT NULL,
	[PreDelivery] [decimal](18, 2) NOT NULL,
	[FinalPerc] [decimal](18, 2) NOT NULL,
	[Final] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProjectMilestones] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleType]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SaleType] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblConsmbl_Solv_Clnr_Relations]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblConsmbl_Solv_Clnr_Relations](
	[RelationID] [int] IDENTITY(1,1) NOT NULL,
	[ConsummableID] [nvarchar](20) NULL,
	[SolventID] [nvarchar](20) NULL,
	[CleanerID] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[RelationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPrntHD_Consmbl_Relations]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPrntHD_Consmbl_Relations](
	[Relationid] [int] IDENTITY(1,1) NOT NULL,
	[PrintHeadID] [nvarchar](20) NULL,
	[PrintHeadDesc] [nvarchar](100) NULL,
	[ConsummableID] [nvarchar](20) NULL,
	[ConsummableDesc] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Relationid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teritory]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teritory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teritory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vw_Assemblies]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vw_Assemblies](
	[RECID] [bigint] NULL,
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL,
	[SALEUNIT] [nvarchar](10) NULL,
	[PRODTYPE] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vw_BOMItemsForTemplate]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vw_BOMItemsForTemplate](
	[RECID] [bigint] NULL,
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vw_BOMitemsInTemplate]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vw_BOMitemsInTemplate](
	[RECID] [bigint] NULL,
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vw_BOMitemsNotInCategory]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vw_BOMitemsNotInCategory](
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL,
	[RECID] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vw_InventoryOnHand]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vw_InventoryOnHand](
	[WAREHOUSE] [nvarchar](10) NULL,
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL,
	[ITEMGROUPID] [nvarchar](10) NULL,
	[AVAILPHYSICAL] [numeric](38, 16) NULL,
	[RESERVPHYSICAL] [numeric](38, 16) NULL,
	[TOTALPHYSICAL] [numeric](38, 16) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vw_Opportunity]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vw_Opportunity](
	[opportunity] [nvarchar](300) NULL,
	[cvn_SalespersonName] [nvarchar](200) NULL,
	[cvn_PrimaryContactName] [nvarchar](160) NULL,
	[map_QuoteNumber] [nvarchar](100) NULL,
	[Name] [nvarchar](160) NULL,
	[Address] [nvarchar](4000) NULL,
	[telephone1] [nvarchar](50) NULL,
	[cvn_erpcode] [nvarchar](250) NULL,
	[EMailAddress1] [nvarchar](110) NULL,
	[JobTitle] [nvarchar](100) NULL,
	[OpportunityId] [uniqueidentifier] NULL,
	[TerritoryIdName] [nvarchar](200) NULL,
	[CLOSEDDATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vw_Products]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vw_Products](
	[RECID] [bigint] NULL,
	[ITEMID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL,
	[SALEPRICE] [numeric](32, 16) NULL,
	[SALEUNIT] [nvarchar](10) NULL,
	[PRODTYPE] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vw_Territory]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vw_Territory](
	[SALESDISTRICTID] [nvarchar](20) NULL,
	[DESCRIPTION] [nvarchar](60) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BOM] ADD  CONSTRAINT [DF_BOM_IsCustomParts]  DEFAULT ((1)) FOR [IsCustomParts]
GO
ALTER TABLE [dbo].[BOMTemplate] ADD  CONSTRAINT [DF_BOMTemplate_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[BOMTemplate] ADD  CONSTRAINT [DF_BOMTemplate_IsQuantityEditable]  DEFAULT ((1)) FOR [IsQtyFixed]
GO
ALTER TABLE [dbo].[BOMTemplate] ADD  CONSTRAINT [DF_BOMTemplate_IsDiscountApply]  DEFAULT ((0)) FOR [IsDiscountApply]
GO
ALTER TABLE [dbo].[BOMTemplate] ADD  CONSTRAINT [DF_BOMTemplate_MaximumQty]  DEFAULT ((0)) FOR [MaximumQty]
GO
ALTER TABLE [dbo].[BOMTemplate] ADD  CONSTRAINT [DF_BOMTemplate_AssemblyID]  DEFAULT ((0)) FOR [AssemblyID]
GO
ALTER TABLE [dbo].[BOMTemplate] ADD  CONSTRAINT [DF_BOMTemplate_IsInTotal]  DEFAULT ((1)) FOR [IsInTotal]
GO
ALTER TABLE [dbo].[BOMTemplate] ADD  CONSTRAINT [DF_BOMTemplate_IsDecimalAllowed]  DEFAULT ((0)) FOR [IsDecimalAllowed]
GO
ALTER TABLE [dbo].[BOMtoAssembly] ADD  CONSTRAINT [DF_BOMtoAssembly_PMPercentage]  DEFAULT ((0)) FOR [PMPercentage]
GO
ALTER TABLE [dbo].[Opportunity] ADD  CONSTRAINT [DF_Opportunity_Teritory1Split]  DEFAULT ((0)) FOR [Territory1Split]
GO
ALTER TABLE [dbo].[Opportunity] ADD  CONSTRAINT [DF_Opportunity_Teritory2Split]  DEFAULT ((0)) FOR [Territory2Split]
GO
ALTER TABLE [dbo].[Opportunity] ADD  CONSTRAINT [DF_Opportunity_LeadTime]  DEFAULT ((0)) FOR [LeadTime]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  CONSTRAINT [DF_OpportunityBOMList_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  CONSTRAINT [DF_OpportunityBOMList_FinalAgreedPrice]  DEFAULT ((0)) FOR [FinalAgreedPrice]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  CONSTRAINT [DF_OpportunityBOMList_IsDiscountApply]  DEFAULT ((0)) FOR [IsDiscountApply]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  CONSTRAINT [DF_OpportunityBOMList_PriceAfterDiscount]  DEFAULT ((0)) FOR [PriceAfterDiscount]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  CONSTRAINT [DF_OpportunityBOMList_State]  DEFAULT ((0)) FOR [State]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  CONSTRAINT [DF_OpportunityBOMList_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  CONSTRAINT [DF_OpportunityBOMList_IsInTotal]  DEFAULT ((1)) FOR [IsInTotal]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  CONSTRAINT [DF_OpportunityBOMList_IsDecimalAllowed]  DEFAULT ((1)) FOR [IsDecimalAllowed]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[OpportunityBOMList] ADD  DEFAULT (NULL) FOR [VersionNum]
GO
ALTER TABLE [dbo].[Assembly]  WITH CHECK ADD  CONSTRAINT [FK_Assembly_AssemblyCategory] FOREIGN KEY([AssemblyCategoryID])
REFERENCES [dbo].[AssemblyCategory] ([ID])
GO
ALTER TABLE [dbo].[Assembly] CHECK CONSTRAINT [FK_Assembly_AssemblyCategory]
GO
ALTER TABLE [dbo].[Assembly]  WITH CHECK ADD  CONSTRAINT [FK_Assembly_BOMItems] FOREIGN KEY([vwProductsID])
REFERENCES [dbo].[BOMItems] ([RECID])
GO
ALTER TABLE [dbo].[Assembly] CHECK CONSTRAINT [FK_Assembly_BOMItems]
GO
ALTER TABLE [dbo].[BOMtoAssembly]  WITH CHECK ADD  CONSTRAINT [FK_BOMtoAssembly_Assembly] FOREIGN KEY([AssemblyID])
REFERENCES [dbo].[Assembly] ([ID])
GO
ALTER TABLE [dbo].[BOMtoAssembly] CHECK CONSTRAINT [FK_BOMtoAssembly_Assembly]
GO
ALTER TABLE [dbo].[BOMtoAssembly]  WITH CHECK ADD  CONSTRAINT [FK_BOMtoAssembly_BOM] FOREIGN KEY([BOMid])
REFERENCES [dbo].[BOM] ([ID])
GO
ALTER TABLE [dbo].[BOMtoAssembly] CHECK CONSTRAINT [FK_BOMtoAssembly_BOM]
GO
ALTER TABLE [dbo].[BOMtoAssembly]  WITH CHECK ADD  CONSTRAINT [FK_BOMtoAssembly_BOMItems] FOREIGN KEY([Device_ID])
REFERENCES [dbo].[BOMItems] ([RECID])
GO
ALTER TABLE [dbo].[BOMtoAssembly] CHECK CONSTRAINT [FK_BOMtoAssembly_BOMItems]
GO
ALTER TABLE [dbo].[ItemCategory]  WITH CHECK ADD  CONSTRAINT [FK_ItemCategory_ItemCategory] FOREIGN KEY([ID])
REFERENCES [dbo].[ItemCategory] ([ID])
GO
ALTER TABLE [dbo].[ItemCategory] CHECK CONSTRAINT [FK_ItemCategory_ItemCategory]
GO
ALTER TABLE [dbo].[ItemSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ItemSubCategory_ItemCategory1] FOREIGN KEY([ItemCategory])
REFERENCES [dbo].[ItemCategory] ([ID])
GO
ALTER TABLE [dbo].[ItemSubCategory] CHECK CONSTRAINT [FK_ItemSubCategory_ItemCategory1]
GO
ALTER TABLE [dbo].[LeadTimeForAssembly]  WITH CHECK ADD  CONSTRAINT [FK_LeadTimeForAssembly_BOM] FOREIGN KEY([BOMID])
REFERENCES [dbo].[BOM] ([ID])
GO
ALTER TABLE [dbo].[LeadTimeForAssembly] CHECK CONSTRAINT [FK_LeadTimeForAssembly_BOM]
GO
ALTER TABLE [dbo].[LeadTimeForAssembly]  WITH CHECK ADD  CONSTRAINT [FK_LeadTimeForAssembly_BOMItems] FOREIGN KEY([AssemblyID])
REFERENCES [dbo].[Assembly] ([ID])
GO
ALTER TABLE [dbo].[LeadTimeForAssembly] CHECK CONSTRAINT [FK_LeadTimeForAssembly_BOMItems]
GO
ALTER TABLE [dbo].[PartToCapitalAssembly_DEL]  WITH CHECK ADD  CONSTRAINT [FK_PartToCapitalAssembly_PartToCapitalAssembly] FOREIGN KEY([AssemblyID])
REFERENCES [dbo].[Assembly] ([ID])
GO
ALTER TABLE [dbo].[PartToCapitalAssembly_DEL] CHECK CONSTRAINT [FK_PartToCapitalAssembly_PartToCapitalAssembly]
GO
ALTER TABLE [dbo].[PartToCapitalAssembly_DEL]  WITH CHECK ADD  CONSTRAINT [FK_PartToCapitalAssembly_Products] FOREIGN KEY([PartID])
REFERENCES [dbo].[BOMItems] ([RECID])
GO
ALTER TABLE [dbo].[PartToCapitalAssembly_DEL] CHECK CONSTRAINT [FK_PartToCapitalAssembly_Products]
GO
ALTER TABLE [dbo].[PartToCategory]  WITH CHECK ADD  CONSTRAINT [FK_PartToCategory_ItemCategory] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[ItemCategory] ([ID])
GO
ALTER TABLE [dbo].[PartToCategory] CHECK CONSTRAINT [FK_PartToCategory_ItemCategory]
GO
ALTER TABLE [dbo].[PartToCategory]  WITH CHECK ADD  CONSTRAINT [FK_PartToCategory_ItemSubCategory] FOREIGN KEY([SubCategoryID])
REFERENCES [dbo].[ItemSubCategory] ([ID])
GO
ALTER TABLE [dbo].[PartToCategory] CHECK CONSTRAINT [FK_PartToCategory_ItemSubCategory]
GO
ALTER TABLE [dbo].[PMFeeNotChargable]  WITH CHECK ADD  CONSTRAINT [FK_PMFeeChargable_Assembly] FOREIGN KEY([AssemblyID])
REFERENCES [dbo].[Assembly] ([ID])
GO
ALTER TABLE [dbo].[PMFeeNotChargable] CHECK CONSTRAINT [FK_PMFeeChargable_Assembly]
GO
ALTER TABLE [dbo].[PMFeeNotChargable_DEL]  WITH CHECK ADD  CONSTRAINT [FK_pmFeeChargable_ItemCategory] FOREIGN KEY([PartID])
REFERENCES [dbo].[ItemCategory] ([ID])
GO
ALTER TABLE [dbo].[PMFeeNotChargable_DEL] CHECK CONSTRAINT [FK_pmFeeChargable_ItemCategory]
GO
/****** Object:  StoredProcedure [dbo].[CreateLead]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateLead]

@CustomerName varchar(50),
@LeadDate datetime,
@LeadBy varchar(50),
@LeadNumber varchar(50),
@NoOfWeeks Int,
@DelivaeryDate datetime,
@PONumber varchar(50),
@DispatchAddress varchar(500),
@DispatchName varchar(50),
@Title varchar(50),
@PhoneNumber varchar(50),
@Email varchar(50),
@CRMNumber varchar(50)

AS
BEGIN
	
	Declare @LeadID int

	SET NOCOUNT ON;


    -- Insert statements for procedure here
	INSERT INTO [dbo].[Lead] 
						(CustomerName,
						LeadDate,
						LeadBy,
						LeadNumber,
						NoOfWeeks,
						DelivaeryDate,
						PONumber,
						DispatchAddress,
						DispatchName,
						Title,
						PhoneNumber,
						Email,
						ReferenceNo)

	values	(@CustomerName,
		@LeadDate,
		@LeadBy,
		@LeadNumber,
		@NoOfWeeks,
		@DelivaeryDate,
		@PONumber,
		@DispatchAddress,
		@DispatchName,
		@Title,
		@PhoneNumber,
		@Email,
		@CRMNumber)

		SET @LeadID = @@IDENTITY
		INSERT INTO QuoteItemMaster (LeadID,ItemMasterID,IsSelected)
		SELECT @LeadID,ID,0 FROM [dbo].[ItemsMaster] ORDER BY id

END
GO
/****** Object:  StoredProcedure [dbo].[Get_AssemblyForBOMByOpportunityID]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[Get_AssemblyForBOMByOpportunityID]  
   
@OpportunityID int,  
@BOMID int  
  
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
  DECLARE @intState Int  
  Select @intState=MAX([State])  FROM OpportunityBOMList WHERE OpportunityID=@OpportunityID  
  
 SELECT vp.[ITEMID] as AssemblyCode,vp.[DESCRIPTION] as Area,c.Category,ol.QTY,a.Device,  
  ol.BOMItemsID,(ol.Qty*ol.ItemPrice) as price,ba.PMPercentage,o.QuoteNo , ol.versionnum 
    FROM OpportunityBOMList ol  
 INNER JOIN BOMtoAssembly ba on ol.BOMItemsID=ba.Device_ID and ol.BOMID=ba.BOMid  
 INNER JOIN [Assembly] a on a.ID=ba.AssemblyID  
 INNER JOIN vw_Products vp on vp.RECID=a.vwProductsID  
 INNER JOIN AssemblyCategory C on C.ID=A.AssemblyCategoryID  
 INNER JOIN Opportunity o on o.ID=ol.OpportunityID  
 WHERE ol.BOMid=@BOMID and ol.OpportunityID=@OpportunityID and ol.[State]=@intState and ol.Qty > 0  and ol.IsActive=1 
 --FROM BOMtoAssembly ba   
 --INNER JOIN [Assembly] a ON a.ID=ba.AssemblyID  
 --INNER JOIN AssemblyCategory C on C.ID=A.AssemblyCategoryID  
 --INNER JOIN vw_Products vp on vp.RECID=a.vwProductsID  
 --INNER JOIN BOMItems bi ON bi.ITEMID=vp.ITEMID  
 --LEFT OUTER JOIN (select * from OpportunityBOMList WHERE BOMid=@BOMID and OpportunityID=@OpportunityID and [State]=@intState) ol ON ol.BOMItemsID=bi.RECID  
 --WHERE ba.BOMid=@BOMID  
   
     
  
END  
GO
/****** Object:  StoredProcedure [dbo].[Get_BOMPriceTotalsByOpportunityID]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_BOMPriceTotalsByOpportunityID]
	
@OpportunityID int,
@BOMID int

AS
BEGIN
	
	SET NOCOUNT ON;

	 DECLARE @intState Int
	 Select @intState=MAX([State])  FROM OpportunityBOMList WHERE OpportunityID=@OpportunityID

	SELECT SUM(CASE WHEN o.IsInTotal=1 THEN
				(o.ItemPrice*o.Qty)
			ELSE
				0 
			END) AS TotalPrice, 
			max(o.discount) as Discount, 
			OpportunityID,BOMID,
			max(o.FinalAgreedPrice) as FinalAgreedPrice,
			0 as CTO,
			max(a.PONumber) as PONumber
	FROM OpportunityBOMList o
	INNER JOIN Opportunity a on a.ID=o.OpportunityID
	WHERE O.OpportunityID=@OpportunityID AND O.BOMID=@BOMID AND O.[State]=@intState
	GROUP BY OpportunityID,BOMID

  
   

END
GO
/****** Object:  StoredProcedure [dbo].[Get_OpportunityBOMChildItemsByBOMItemID]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_OpportunityBOMChildItemsByBOMItemID]
	
@OpportunityID int,
@BOMItemID nvarchar(50),
@BOMID int

AS
BEGIN
	
	SET NOCOUNT OFF;


		 SELECT 
			 @OpportunityID as OpportunityID,
			 0 as OpportunityBOMListID ,
			 t.BOM_ID as BOMID,
			 t.BOMItem_ID as BOMItemsID,
			 t.Price as ItemPrice,
			 t.Quantity as Qty,
			 i.[DESCRIPTION],
			 c.Category,
			 s.[Name] as SubCategory,
			 i.ITEMID,
			 CASE WHEN t.IsInTotal=1 
			 THEN
				(t.Price*t.Quantity) 
			 ELSE
				0
			 END as Price,
			 isnull(b.IsCustomParts,0) as IsCustomParts,
			 0 as FinalAgreedPrice,
			 0 as Discount,isnull(t.IsDiscountApply,0) as IsDiscountApply,
			 isnull(t.IsQtyFixed,0) as IsQtyFixed ,
			 0 as PriceAfterDiscount,
			 b.BOM,
			 t.MaximumQty,
			 (SELECT ClosedDate FROM Opportunity WHERE id=@OpportunityID) AS ClosedDate,
			 cast( isnull(si.AVAILPHYSICAL,0) as int) as Stock,
			 t.IsInTotal,
			 T.IsDecimalAllowed
		 FROM BOMTemplate t 
		 INNER JOIN BOM b ON b.ID=t.BOM_ID
		 INNER JOIN BOMItems i on i.RecID=t.BOMItem_ID
		 INNER JOIN PartToCategory P ON P.PartID=i.recID
		 INNER JOIN ItemCategory c on c.ID=p.CategoryID
		 LEFT OUTER JOIN InventoryOnHand si	ON si.ITEMID=i.ITEMID 
		 LEFT OUTER JOIN ItemSubCategory s on s.ID=p.SubCategoryID
		 WHERE t.BOM_ID=@BOMID and t.ParentBOMItemID=@BOMItemID
		 ORDER BY c.Category,s.[Name],i.ITEMID
		 	
    
END
GO
/****** Object:  StoredProcedure [dbo].[Get_OpportunityBOMItemsByOpportunityID]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[Get_OpportunityBOMItemsByOpportunityID]    
     
@OpportunityID int,    
@BOMID int,    
@NewBOM bit,    
@State int    ,
@versionnum int
    
AS    
BEGIN    
     
 SET NOCOUNT OFF;    
    
   if @NewBOM=1    
   SELECT     
    @OpportunityID as OpportunityID,    
    0 as OpportunityBOMListID ,    
    t.BOM_ID as BOMID,    
    t.BOMItem_ID as BOMItemsID,    
    t.Price as ItemPrice,    
    t.Quantity as Qty,    
    i.[DESCRIPTION],    
    c.Category,c.ItemOrder as CategoryOrder,    
    s.[Name] as SubCategory,s.ItemOrder as SubCategoryOrder,    
    i.ITEMID,    
    CASE WHEN t.IsInTotal=1     
    THEN    
    (t.Price*t.Quantity)     
    ELSE    
    0    
    END as Price,    
    isnull(b.IsCustomParts,0) as IsCustomParts,    
    0 as FinalAgreedPrice,    
    0 as Discount,isnull(t.IsDiscountApply,0) as IsDiscountApply,    
    isnull(t.IsQtyFixed,0) as IsQtyFixed ,    
    0 as PriceAfterDiscount,    
    b.BOM,    
    t.MaximumQty,    
    (SELECT ClosedDate FROM Opportunity WHERE id=@OpportunityID) AS ClosedDate,    
    cast( isnull(si.AVAILPHYSICAL,0) as int) as Stock,    
    t.IsInTotal,    
    T.IsDecimalAllowed,    
    0 as InkUsage    
   FROM BOMTemplate t     
   INNER JOIN BOM b ON b.ID=t.BOM_ID    
   INNER JOIN BOMItems i on i.RecID=t.BOMItem_ID    
   INNER JOIN PartToCategory P ON P.PartID=i.recID    
   INNER JOIN ItemCategory c on c.ID=p.CategoryID    
   LEFT OUTER JOIN InventoryOnHand si ON si.ITEMID=i.ITEMID     
   LEFT OUTER JOIN ItemSubCategory s on s.ID=p.SubCategoryID    
   WHERE t.BOM_ID=@BOMID and (t.ParentBOMItemID is null or t.ParentBOMItemID='')    
   ORDER BY c.Category,s.[Name],i.ITEMID    
    
  ELSE     
   BEGIN    
     IF @State<>0    
   BEGIN    
   --Check whether any items exist in the state    
   --IF not insert the record set from the previous state    
    
      DECLARE @Count int    
      SELECT @Count=COUNT(ID)    
   FROM OpportunityBOMList    
      WHERE OpportunityID=@OpportunityID and BOMID=@BOMID and IsActive=1 and [State]=@State    
       
      IF @Count=0     
    BEGIN    
       INSERT INTO OpportunityBOMList (OpportunityID,BOMID,BOMItemsID,Qty,ItemPrice,Price,CustomDescription,CustomCode,    
      Discount,FinalAgreedPrice,IsDiscountApply,PriceAfterDiscount,CreatedDateTime,MaximumQty,UpdatedDatetime,    
      IsActive,[State],IsInTotal,IsDecimalAllowed)    
    SELECT OpportunityID,    
      BOMID,    
      BOMItemsID,    
      Qty,    
      ItemPrice,    
      Price,    
      CustomDescription,    
      CustomCode,    
      Discount,    
      FinalAgreedPrice,    
      IsDiscountApply,    
      PriceAfterDiscount,    
      Getdate(),    
      MaximumQty,    
      UpdatedDatetime,    
      IsActive,    
      @State,    
      IsInTotal,    
      IsDecimalAllowed
    FROM OpportunityBOMList    
    WHERE OpportunityID=@OpportunityID and BOMID=@BOMID and IsActive=1 and [State]=(@State-1)    
    END       
   END    
       
       
    
    
      SELECT  pb.OpportunityID,    
    0 as OpportunityBOMListID ,    
    pb.BOMID,    
    pb.BOMItemsID,    
    pb.ItemPrice,    
    pb.Qty,    
    i.[DESCRIPTION],    
     c.Category,c.ItemOrder as CategoryOrder,    
    s.[Name] as SubCategory,s.ItemOrder as SubCategoryOrder,    
    i.ITEMID,    
    CASE WHEN pb.IsInTotal=1     
    THEN    
    (pb.ItemPrice*pb.Qty)     
    ELSE    
    0    
    END as Price,    
    isnull(b.IsCustomParts,0) as IsCustomParts,    
    pb.FinalAgreedPrice,    
    pb.Discount,    
    isnull(pb.IsDiscountApply,0)as IsDiscountApply,    
    isnull(t.IsQtyFixed,0) as IsQtyFixed ,    
    pb.PriceAfterDiscount,    
    b.BOM,    
    pb.MaximumQty,    
    O.ClosedDate,    
    cast( isnull(si.AVAILPHYSICAL,0) as int) as Stock,    
    pb.IsInTotal,    
    pb.IsDecimalAllowed,
	pb.versionnum,
    O.InkUsage    
   FROM OpportunityBOMList pb    
   INNER JOIN BOM b ON b.ID=pb.BOMID    
   INNER JOIN BOMItems i on i.RecID=pb.BOMItemsID    
   INNER JOIN PartToCategory P ON P.PartID=i.recID    
   INNER JOIN ItemCategory c on c.ID=p.CategoryID    
   INNER JOIN Opportunity O ON O.ID=PB.OpportunityID    
   LEFT OUTER JOIN InventoryOnHand si ON si.ITEMID=i.ITEMID     
   LEFT OUTER JOIN (SELECT DISTINCT BOM_ID,IsQtyFixed, BOMItem_ID FROM BOMTemplate where BOM_ID=@BOMID) t  ON PB.BOMItemsID=t.BOMItem_ID    
   LEFT OUTER JOIN ItemSubCategory s on s.ID=p.SubCategoryID    
   WHERE pb.OpportunityID=@OpportunityID and pb.BOMID=@BOMID and pb.IsActive=1 and pb.[State]=@State and pb.versionnum=@versionnum   
   ORDER BY  c.Category,s.[Name],i.ITEMID    
       
    END    
END 
GO
/****** Object:  StoredProcedure [dbo].[Get_OpportunityBOMList_BOMDownload]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[Get_OpportunityBOMList_BOMDownload]

@OpportunityID Int,
@BOMID int,
@State int

AS
BEGIN

	   	 
		 SELECT b.BOM,i.[DESCRIPTION],pb.Price as ItemPrice,
		 	CASE WHEN pb.IsInTotal=1 THEN
				(pb.ItemPrice*pb.Qty)
			ELSE
				0 
			END AS Price,
		  i.ITEMID as MatthewsCode,pb.Qty,c.Category,O.CompanyName,O.AccountContactEmail,O.DispatchAddress,O.QuoteNo,pb.Discount,
		   CASE WHEN pb.IsInTotal=1 THEN
				pb.PriceAfterDiscount
			ELSE
				0 
			END AS PriceAfterDiscount,
		  pb.FinalAgreedPrice
		 FROM OpportunityBOMList pb
			 INNER JOIN BOM b ON b.ID=pb.BOMID
			 INNER JOIN BOMItems i on i.RecID=pb.BOMItemsID
			 INNER JOIN PartToCategory P ON P.PartID=i.recID
			 INNER JOIN ItemCategory c on c.ID=p.CategoryID
			 INNER JOIN Opportunity O ON O.ID=PB.OpportunityID
			 LEFT OUTER JOIN ItemSubCategory s on s.ID=p.SubCategoryID
		 WHERE pb.OpportunityID=@OpportunityID and  pb.BOMID=@BOMID and pb.IsActive=1 and [State]=@State

	
END
GO
/****** Object:  StoredProcedure [dbo].[Get_OpportunityBOMListBYOpportunityID]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_OpportunityBOMListBYOpportunityID]
@OpportunityID int
AS
BEGIN

	SET NOCOUNT ON;
	 
	 DECLARE @intState Int
	 Select @intState=MAX([State])  FROM OpportunityBOMList WHERE OpportunityID=@OpportunityID

	  
	SELECT distinct b.ID as BOMID , 
		   b.BOM,
		   isnull(ol.TotalPrice,0) as TotalPrice ,
		    ol.OpportunityID,
			isnull(ol.Discount,0) as Discount,
			isnull(ol.PriceAfterDiscount,0) as PriceAfterDiscount,
			isnull(ol.FinalAgreedPrice,0) as FinalAgreedPrice, O.ClosedDate
	FROM BOM b
	INNER JOIN OpportunityBOMList l on l.BOMID=b.ID
	INNER JOIN Opportunity O ON O.ID=L.OpportunityID
	LEFT OUTER JOIN (SELECT BOMID,OpportunityID,	
					SUM(CASE WHEN IsInTotal=1 THEN
						 Price
					 ELSE
						 0 
						 END) as TotalPrice,
					SUM(CASE WHEN IsInTotal=1 THEN
						 PriceAfterDiscount
					 ELSE
						 0 
					 END) as PriceAfterDiscount,
					
					max(discount) as Discount,max(FinalAgreedPrice)  as FinalAgreedPrice
					 FROM OpportunityBOMList WHERE  IsActive=1 AND [State]=@intState group by OpportunityID,BOMID) ol on ol.BOMID=b.ID and l.OpportunityID=ol.OpportunityID -- IsActive=1 AND--Comented 18032021 RobertOVE
	WHERE l.OpportunityID=@OpportunityID AND [State]=@intState
	UNION
	SELECT distinct  
		b.id,b.BOM,
		0 as TotalPrice,
		@OpportunityID as OpportunityID ,
		0 as Discount,
		0 as PriceAfterDiscount,
		0 as FinalAgreedPrice,
		'' AS ClosedDate
	FROM BOM b 
	INNER JOIN BOMTemplate T ON T.BOM_ID=B.ID
	INNER JOIN BOMItems i on i.RecID=t.BOMItem_ID
	INNER JOIN PartToCategory P ON P.PartID=i.recID
	INNER JOIN ItemCategory c on c.ID=p.CategoryID
	WHERE b.id NOT IN(SELECT DISTINCT l.BOMID FROM OpportunityBOMList l WHERE OpportunityID=@OpportunityID)

	ORDER BY b.BOM

END
GO
/****** Object:  StoredProcedure [dbo].[Get_OpportunityByQuoteNo]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_OpportunityByQuoteNo]
@QuoteNo Varchar(50)	
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @ID varchar(100)
	DECLARE @MAXLeadTime int
	DECLARE @ClosedDate Datetime
	DECLARE @CalcDeliveryDate Datetime=GETDATE()
	DECLARE @Day int

    -- Insert statements for procedure here
	SELECT @ID=ID from Opportunity WHERE QuoteNo=@QuoteNo
	 PRINT @ID
	IF @ID<>''
	 BEGIN
			--Uppdate the closing date		
		   UPDATE Opportunity 
		   SET ClosedDate=vo.[CLOSEDDATE] AT TIME ZONE 'UTC' AT TIME ZONE 'AUS Eastern Standard Time' , 
		   CustomerCode=vo.cvn_erpcode,
		   Opportunity=vo.opportunity,
		   [Address]=vo.[Address],
		   CompanyName=vo.[Name],
		   QuoteNo=vo.map_QuoteNumber,
		   SalesPerson=vo.cvn_SalespersonName,
		   AccountContactName=vo.cvn_PrimaryContactName,
		   AccountContactTitle=vo.[JobTitle],
           AccountContactPhoneNo=vo.[telephone1],
		   AccountContactEmail=vo.[EMailAddress1]
		   FROM Opportunity o INNER JOIN vw_Opportunity vo on vo.map_QuoteNumber COLLATE SQL_Latin1_General_CP1_CI_AS=o.QuoteNo COLLATE SQL_Latin1_General_CP1_CI_AS
		   WHERE o.QuoteNo=@QuoteNo

		   
	       --Calculate the Dellivery Date
		  SELECT  @ClosedDate=ClosedDate,@Day=day(ClosedDate) FROM Opportunity  WHERE QuoteNo=@QuoteNo

		   IF @ClosedDate IS NOT NULL
			   BEGIN 
			  
				   SELECT @MAXLeadTime=max(LeadTimeDays)  FROM OpportunityBOMList ol 
							inner join Opportunity op on op.ID=ol.OpportunityID
							inner join BOMtoAssembly a on ol.BOMID=a.BOMid
							inner join LeadTimeForAssembly lt on lt.AssemblyID=a.AssemblyID and ol.BOMID=lt.BOMID
					WHERE op.QuoteNo=@QuoteNo

					IF @MAXLeadTime<>''
					 BEGIN
						 IF @DAY=6  --if a Friday start from Monday
						 BEGIN
							SET @MAXLeadTime=@MAXLeadTime+2
						 END

						SET @CalcDeliveryDate=DATEADD(DD,@MAXLeadTime,@ClosedDate)
					END
								
				END
		 	
		    SELECT *,@CalcDeliveryDate AS CalcDeliveryDate FROM Opportunity WHERE QuoteNo=@QuoteNo

	   END

	ELSE
	 BEGIN

	   INSERT INTO [dbo].[Opportunity]([Opportunity],[SalesPerson],[AccountContactName],[QuoteNo],[CompanyName],[Address],[AccountContactPhoneNo],[AccountContactEmail],[AccountContactTitle],[ClosedDate],[Territory1ID],[Territory1Split],[CustomerCode],[DateTimeStamp])
	   SELECT [opportunity]
			  ,[cvn_SalespersonName]
			  ,[cvn_PrimaryContactName]
			  ,[map_QuoteNumber]
			  ,[Name]
			  ,[Address]
			  ,[telephone1]
			  ,[EMailAddress1]
			  ,[JobTitle]
			  ,[CLOSEDDATE] AT TIME ZONE 'UTC' AT TIME ZONE 'AUS Eastern Standard Time' AS [CLOSEDDATE]
			  ,[TerritoryIdName]
			  ,100
			  ,[cvn_erpcode]
			  ,GETDATE()
		  FROM vw_Opportunity WHERE map_QuoteNumber=@QuoteNo

	       --Calculate the Dellivery Date
		  SELECT  @ClosedDate=ClosedDate,@Day=day(ClosedDate) FROM Opportunity  WHERE QuoteNo=@QuoteNo
	  
		   IF @ClosedDate IS NOT NULL
			   BEGIN 		 
				   SELECT @MAXLeadTime=max(LeadTimeDays)  FROM OpportunityBOMList ol 
							inner join Opportunity op on op.ID=ol.OpportunityID
							inner join BOMtoAssembly a on ol.BOMID=a.BOMid
							inner join LeadTimeForAssembly lt on lt.AssemblyID=a.AssemblyID and ol.BOMID=lt.BOMID
					WHERE op.QuoteNo=@QuoteNo

					IF @MAXLeadTime<>''
					 BEGIN
						 IF @DAY=6  --if a Friday start from Monday
						 BEGIN
							SET @MAXLeadTime=@MAXLeadTime+2
						 END

						SET @CalcDeliveryDate=DATEADD(DD,@MAXLeadTime,@ClosedDate)
					END
	
				END

	 	SELECT *,@CalcDeliveryDate AS CalcDeliveryDate from Opportunity WHERE QuoteNo=@QuoteNo
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Get_OpportunityList]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_OpportunityList]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT top 1000 *
  FROM [OnlineBOM].[dbo].[vw_Opportunity] order by [OpportunityId] desc

END
GO
/****** Object:  StoredProcedure [dbo].[Get_OpportunityPickingList_BOMDownload]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[Get_OpportunityPickingList_BOMDownload]

@OpportunityID Int,
@BOMID int,
@State int

AS
BEGIN

	   	 
		 SELECT   b.BOM,i.[DESCRIPTION], i.ITEMID as MatthewsCode,pb.Qty,O.CompanyName,O.AccountContactEmail,O.DispatchAddress,O.QuoteNo,vp.ITEMID as AssemblyCode,c.Category,a.[Description] as AssemblyDesc
		 FROM OpportunityBOMList pb
			 INNER JOIN BOM b ON b.ID=pb.BOMID
			 INNER JOIN BOMItems i on i.RecID=pb.BOMItemsID
			 INNER JOIN PartToCategory P ON P.PartID=i.recID
			 INNER JOIN Opportunity O ON O.ID=PB.OpportunityID
			 LEFT OUTER JOIN BOMTemplate t ON t.BOM_ID=PB.BOMID AND pb.BOMItemsID=t.BOMItem_ID
			 LEFT OUTER JOIN [Assembly] a on a.ID=t.AssemblyID
			 LEFT OUTER JOIN vw_Products vp on a.vwProductsID=vp.RECID
	         INNER JOIN ItemCategory c on c.ID=p.CategoryID
			 LEFT OUTER JOIN ItemSubCategory s on s.ID=p.SubCategoryID
		 WHERE pb.OpportunityID=@OpportunityID and  pb.BOMID=@BOMID and pb.IsActive=1 and [State]=@State and pb.Qty>0
		 ORDER BY a.[Description],c.ID 

	
END
GO
/****** Object:  StoredProcedure [dbo].[Get_PMFeeChargableAssemlyByBOMID]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_PMFeeChargableAssemlyByBOMID]
	
@BOMID bit

AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT VP.ITEMID
	FROM PMFeeChargable pm
	INNER JOIN [Assembly] a ON a.ID=pm.AssemblyID
	INNER JOIN BOMtoAssembly ba ON ba.AssemblyID=a.ID
	INNER JOIN vw_Products vp on vp.RECID=a.vwProductsID
	WHERE ba.BOMid=@BOMID


	
   

END
GO
/****** Object:  StoredProcedure [dbo].[Get_PMFeeNotChargableByBOMID]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Get_PMFeeNotChargableByBOMID]
	
@BOMID bit

AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT VP.ITEMID
	FROM PMFeeNotChargable pm
	INNER JOIN [Assembly] a ON a.ID=pm.AssemblyID
	INNER JOIN BOMtoAssembly ba ON ba.AssemblyID=a.ID
	INNER JOIN vw_Products vp on vp.RECID=a.vwProductsID
	WHERE ba.BOMid=@BOMID


	
   

END
GO
/****** Object:  StoredProcedure [dbo].[Get_ProjectMilestones]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_ProjectMilestones]

@OpportunityID int,
@BOMID int	

AS
BEGIN
	
	SET NOCOUNT ON;
	
	SELECT *
	FROM [ProjectMilestones]
	WHERE OpportunityID=@OpportunityID AND BOMID=@BOMID
		   

END
GO
/****** Object:  StoredProcedure [dbo].[Get_SaleTypes]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Get_SaleTypes]
	

AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM SaleType Order By ID
	



   

END
GO
/****** Object:  StoredProcedure [dbo].[Get_Territory]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Get_Territory]
	

AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM vw_Territory
	



   

END
GO
/****** Object:  StoredProcedure [dbo].[GetQuoteItemMasterByQuoteID]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuoteItemMasterByQuoteID]
@id Int	
AS
BEGIN

	SET NOCOUNT ON;
	  
	SELECT cim.ID,im.[Name],cim.IsSelected,lt.LineType,qlid.QuoteItemMasterID as Edit,qlid.TotalPrice from Lead c 
	INNER JOIN QuoteItemMaster cim on c.ID=cim.LeadID
	INNER JOIN ItemsMaster im on im.ID =cim.ItemMasterID
	INNER JOIN LineType lt on lt.ID=im.LineTypeID
	LEFT OUTER JOIN (select QuoteItemMasterID,LeadID, sum(price) as TotalPrice from QuoteLineItemDetail group by QuoteItemMasterID,LeadID) qlid on qlid.QuoteItemMasterID=cim.ID and qlid.LeadID=cim.LeadID
	WHERE c.ID=@id and im.IsActive=1
	ORDER BY lt.ID,im.[Name]
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_OpportunityBOMList]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insert_OpportunityBOMList]

@OpportunityID int,
@BOMID int,
@BOMItemsID bigint,
@Qty decimal(18, 1),
@ItemPrice decimal(18, 2),
@Price decimal(18, 2),
@CustomDescription nvarchar='',
@CustomCode nvarchar='',
@Discount decimal(18, 2),
@FinalAgreedPrice decimal(18, 2),
@PriceAfterDiscount decimal(18, 2),
@IsDiscountApply bit,
@MaximumQty decimal(18, 1),
@State int,
@IsInTotal bit,
@IsDecimalAllowed bit,
@InkUsage nvarchar(50)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @OpportunityBOMListID INT=0

	--Find the Item
	SELECT @OpportunityBOMListID=ID 
	FROM OpportunityBOMList 
	WHERE OpportunityID=@OpportunityID AND BOMID=@BOMID AND BOMItemsID=@BOMItemsID AND [State]=@State

	UPDATE Opportunity SET InkUsage=@InkUsage WHERE ID=@OpportunityID

	IF @OpportunityBOMListID<>0
	  
	   UPDATE OpportunityBOMList SET 
			   Qty=@Qty,
			   Price=(@Qty*@ItemPrice),
			   ItemPrice=@ItemPrice,
			   Discount=@Discount,
			   FinalAgreedPrice=@FinalAgreedPrice,
			   IsDiscountApply=@IsDiscountApply,
			   PriceAfterDiscount=@PriceAfterDiscount,
			   MaximumQty=@MaximumQty,
			   UpdatedDateTime=GETDATE(),
			   IsActive=1,
			   IsInTotal=@IsInTotal,  
			   IsDecimalAllowed=@IsDecimalAllowed
			   WHERE id=@OpportunityBOMListID
	  
	  ELSE
	  
	  INSERT INTO OpportunityBOMList
			      (OpportunityID
				  ,BOMID
				  ,BOMItemsID
				  ,Qty
				  ,ItemPrice
				  ,Price
				  ,CustomDescription
				  ,CustomCode
				  ,Discount
				  ,FinalAgreedPrice
				  ,IsDiscountApply
				  ,PriceAfterDiscount
				  ,[CreatedDateTime]
				  ,[MaximumQty]
				  ,IsActive
				  ,IsInTotal
				  ,IsDecimalAllowed)
		VALUES(@OpportunityID,
				@BOMID,
				@BOMItemsID,
				@Qty,
				@ItemPrice,
				@Price,
				@CustomDescription,
				@CustomCode,
				@Discount,
				@FinalAgreedPrice,
				@IsDiscountApply,
				@PriceAfterDiscount,
				GETDATE(),
				@MaximumQty,
				1
				,@IsInTotal
				,@IsDecimalAllowed)
	



END
GO
/****** Object:  StoredProcedure [dbo].[Insert_ProjectMilestones]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Insert_ProjectMilestones]

@OpportunityID int,
@BOMID int,
@DepositPerc decimal(18,2),	
@Deposit decimal(18,2),	
@PreDeliveryPerc decimal(18,2),	
@PreDelivery decimal(18,2),	
@FinalPerc decimal(18,2),	
@Final decimal(18,2)	

AS
BEGIN
	
	SET NOCOUNT ON;
	Declare @MilestoneID int=0

	SELECT @MilestoneID=id FROM ProjectMilestones WHERE OpportunityID=@OpportunityID AND BOMID=@BOMID

   IF @MilestoneID<>0 
    BEGIN 
      UPDATE ProjectMilestones SET 	DepositPerc=@DepositPerc, Deposit=@Deposit,PreDeliveryPerc=@PreDeliveryPerc,PreDelivery=@PreDelivery,FinalPerc=@FinalPerc,Final=@Final
	  WHERE OpportunityID=@OpportunityID AND BOMID=@BOMID
	END
   ELSE
    BEGIN 
	 INSERT INTO ProjectMilestones([OpportunityID],[BOMID],[DepositPerc],[Deposit],[PreDeliveryPerc],[PreDelivery],[FinalPerc],[Final])
	 VALUES(
			@OpportunityID ,
			@BOMID ,
			@DepositPerc,	
			@Deposit,	
			@PreDeliveryPerc,	
			@PreDelivery,	
			@FinalPerc,	
			@Final)
	END
		   

END
GO
/****** Object:  StoredProcedure [dbo].[Update_OpportunityAssemblyDetails]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_OpportunityAssemblyDetails]

@QuoteNo nvarchar(100),
@CompanyName nvarchar(500),
@Representative nvarchar(100)='',
@CustomerType nvarchar(100)='',
@DeliveryDate datetime=null,
@PONumber nvarchar(100)='',
@Authorisation nvarchar(100)='',
@Campaign nvarchar(50)='',
@CampaignCode nvarchar(50)='',
@SalesPerson nvarchar(100)='',
@SaleTypeID int


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	  UPDATE Opportunity SET  
				CompanyName=@CompanyName,
				Representative=@Representative,
				CustomerType=@CustomerType,
				DeliveryDate=@DeliveryDate,
				PONumber=@PONumber,
				Authorisation=@Authorisation,
				Campaign=@Campaign ,
				CampaignCode=@CampaignCode,
				SalesPerson=@SalesPerson,
				SaleTypeID=@SaleTypeID
	  WHERE QuoteNo=@QuoteNo
	



END
GO
/****** Object:  StoredProcedure [dbo].[Update_OpportunityBOMListIsActiveFlag]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_OpportunityBOMListIsActiveFlag]

@OpportunityID int,
@BOMID int,
@State int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	  
	   UPDATE OpportunityBOMList SET IsActive=0  WHERE OpportunityID=@OpportunityID and BOMID=@BOMID and [State]=@State
	  
END
GO
/****** Object:  StoredProcedure [dbo].[Update_OpportunityCHOPComments]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_OpportunityCHOPComments]

@QuoteNo nvarchar(100),
@CHOPComments nvarchar(1000)=''


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	  UPDATE Opportunity SET  CHOPComments=@CHOPComments						
	  WHERE QuoteNo=@QuoteNo
	



END
GO
/****** Object:  StoredProcedure [dbo].[Update_OpportunityConsumableDetails]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_OpportunityConsumableDetails]

@QuoteNo nvarchar(100),
@InkUsage nvarchar(100)='',
@SolventUsage nvarchar(100)='',
@Comments nvarchar(1000)=''


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	  UPDATE Opportunity SET  InkUsage=@InkUsage,
							  SolventUsage=@SolventUsage,
							  Comments=@Comments
	  WHERE QuoteNo=@QuoteNo
	



END
GO
/****** Object:  StoredProcedure [dbo].[Update_OpportunityCustomerDetails]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_OpportunityCustomerDetails]

@QuoteNo nvarchar(100),
@DispatchAddress nvarchar(500)='',
@AccountContactName nvarchar(100)='',
@AccountContactTitle nvarchar(100)='',
@AccountContactPhoneNo nvarchar(100)='',
@AccountContactEmail nvarchar(100)='',
@FinanceDeal nvarchar(100)='',
@FinanceType nvarchar(100)='',
@FinanceApproved nvarchar(100)='',
@FinanceTotalAmount decimal(18, 2)=0,
@FinancePeriod int=0


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	  UPDATE Opportunity SET  DispatchAddress=@DispatchAddress,
							  AccountContactName=@AccountContactName,
							  AccountContactTitle=@AccountContactTitle,
							  AccountContactPhoneNo=@AccountContactPhoneNo,
							  AccountContactEmail=@AccountContactEmail,
							  FinanceDeal=@FinanceDeal,
							  FinanceType=@FinanceType,
							  FinanceApproved=@FinanceApproved,
							  FinanceTotalAmount=@FinanceTotalAmount,
							  FinancePeriod=@FinancePeriod
	  WHERE QuoteNo=@QuoteNo
	



END
GO
/****** Object:  StoredProcedure [dbo].[Update_OpportunityPMComments]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_OpportunityPMComments]

@QuoteNo nvarchar(100),
@PMComments nvarchar(1000)=''


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	  UPDATE Opportunity SET  PMComments=@PMComments						
	  WHERE QuoteNo=@QuoteNo
	



END
GO
/****** Object:  StoredProcedure [dbo].[Update_OpportunityTerritorySplit]    Script Date: 4/1/2021 11:35:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_OpportunityTerritorySplit]

@QuoteNo nvarchar(100),
@Territory1ID nvarchar(1000)=0,
@Territory2ID nvarchar(1000)=0,
@Territory1Split int,
@Territory2Split int



AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	  UPDATE Opportunity SET  Territory1ID=@Territory1ID,Territory2ID=@Territory2ID,Territory1Split=@Territory1Split,Territory2Split=@Territory2Split						
	  WHERE QuoteNo=@QuoteNo
	



END
GO
USE [master]
GO
ALTER DATABASE [OnlineBOM] SET  READ_WRITE 
GO
