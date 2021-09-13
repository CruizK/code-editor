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

export default paletteToRGB;