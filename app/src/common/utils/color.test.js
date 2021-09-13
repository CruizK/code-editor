import paletteToRGB from "@Utils/color";

test('Palette to RGB should properly convert hex color codes to RGB with opacity', () => {
    expect(paletteToRGB("ce_grey", 0.3)).toBe("rgba(200,199,193,0.3)");
})