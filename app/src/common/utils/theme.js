import { extendTheme } from "@chakra-ui/react";

// ce_ is prepended to differentiate from default css colors
const colors = {
  brand: {
    900: "#1a365d",
    800: "#153e75",
    700: "#2a69ac",
  },
  ce_black: "#000000",
  ce_darkgrey: "#333333",
  ce_middlegrey: "#666666",
  ce_grey: "#c8c7c1", //tested in color.test.js
  ce_lightgrey: "#e5e5ef",
  ce_whitesmoke: "#f5f5f5",
  ce_white: "#ffffff",
  ce_mainmaroon: "#660000",
  ce_hovermaroon: "#560000",
  ce_linkmaroon: "#720030",
  ce_yellow: "#ffa800",
  ce_blue: "#246181",
  ce_green: "#658d1b",
  ce_backgroundtan: "#dad6cb",
  ce_backgroundlighttan: "#f2f1ed",
  ce_gold: "c69250",
  ce_brightyellow: "#f1b434",
  ce_brightblue: "#298fc2",
  ce_brightgreen: "#6da800",
};

const config = {
  initialColorMode: "light",
  useSystemColorMode: false
}

const fonts = {
  body: "Ubuntu Mono, monospace",
  heading: "Open Sans",
  mono: "Ubuntu Mono, monospace",
}

const fontSizes = {
  xs: "13px",
  sm: "16px",
  md: "24px",
  lg: "36px",
  xl: "40px",
}

const theme = extendTheme({ colors, config, fonts, fontSizes })

export default theme;