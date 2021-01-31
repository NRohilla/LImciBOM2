//TypeError: cyclic object value(Firefox)
//TypeError: Converting circular structure to JSON(Chrome and Opera)
//TypeError: Circular reference in value argument not supported(Edge)

const getCircularReplacer = () => {
    const seen = new WeakSet();
    return (key, value) => {
        if (typeof value === "object" && value !== null) {
            if (seen.has(value)) {
                return;
            }
            seen.add(value);
        }
        return value;
    };
};
