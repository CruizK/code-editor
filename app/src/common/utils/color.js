import theme from '@Utils/theme';

/**
 * A function to convert hex strings to an RGB color of given opacity.
 * @param paletteColor a string mapping to a key defined in theme.js
 * @param opacity a number 0-1, representing opacity as percentage.
 * @return A CSS-compatible string representation of the RGB color, with opacity set.
 */
function paletteToRGB(paletteColor, opacity) {
    let rawHex = theme.colors[paletteColor];
    let hex = rawHex.replace('#', '');

    if (hex.length === 3) {
        hex = `${hex[0]}${hex[0]}${hex[1]}${hex[1]}${hex[2]}${hex[2]}`;
    }    

    const r = parseInt(hex.substring(0, 2), 16);
    const g = parseInt(hex.substring(2, 4), 16);
    const b = parseInt(hex.substring(4, 6), 16);

    return `rgba(${r},${g},${b},${opacity})`;
};

/**
 * 
 * @param {Number} r Red
 * @param {Number} g Green
 * @param {Number} b Blue
 * @returns {String} Hex representation
 */
function RGBToHex(r, g, b) {
    const byteToHex = (byte) => {
        var hexMap = "0123456789ABCDEF";
        return String(hexMap.substr((byte >> 4) & 0x0F, 1)) + hexMap.substr(byte & 0x0F, 1);
    };
    return '#' + byteToHex(r) + byteToHex(b) + byteToHex(g);
}

/**
 * 
 * @param {Number} iteration Number less than 32
 * @param {Number} frequency Sine frequency, default 0.3.
 * @returns {String} Hex representation
 */
function getRainbowAtIteration(iteration, frequency=0.3) {
    let r = Math.sin(frequency*iteration + 0) * 127 + 128;
    let g = Math.sin(frequency*iteration + 2) * 127 + 128;
    let b = Math.sin(frequency*iteration + 4) * 127 + 128;

    return RGBToHex(r, g, b);
}

export { paletteToRGB as default, RGBToHex, getRainbowAtIteration };