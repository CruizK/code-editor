import { extendTheme } from "@chakra-ui/react";
import AccordionStyle, { AccordionBoxStyle } from "@Common/styles/Accordion.style";
import ButtonStyle from "@Common/styles/Button.style";
import HeadingStyle from "@Common/styles/Heading.style";
import InputStyle from "@Common/styles/Input.style";
import MainStyle from "@Common/styles/Main.style";
import MenuStyle from "@Common/styles/Menu.style";
import SelectStyle from "@Common/styles/Select.style";
import TagStyle from "@Common/styles/Tag.style";
import TextareaStyle from "@Common/styles/Textarea.style";
import CarouselStyle from "@Common/styles/Carousel.style";
import PopoverStyle from "@Common/styles/Popover.style";
import CourseBoxStyle from "@Common/styles/CourseBox.style";

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
  ce_yellow_dark: "rgba(255, 168, 0, 0.4)", // just RGB-ified ce_yellow with 0.4 opacity
  ce_blue: "#246181",
  ce_green: "#658d1b",
  ce_backgroundtan: "#dad6cb",
  ce_backgroundlighttan: "#f2f1ed",
  ce_gold: "#c69250",
  ce_brightyellow: "#f1b434",
  ce_brightblue: "#298fc2",
  ce_brightgreen: "#6da800",
  languages: {
    python: "#ffa800",
    java: "#246181",
    javascript: "#246181",
    html: "#660000",
    css: "#000000",
    csharp: "#7d2196"
  },
  difficulties: {
    easy: "#880000",
    medium: "#FFD700",
    hard: "#228B22",
  }
};

const config = {
  initialColorMode: "light",
  useSystemColorMode: false
}

const fonts = {
  body: "Open Sans, san-serif",
  button: "Open Sans",
  input: "Open Sans",
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

const styles = {
  global: {
    "*::placeholder": {
      color: "ce_black",
    },
    body: {
      color: "ce_black",
    },
    '.demo-wrapper': {
      h1: {
        fontSize: 'lg',
        lineHeight: '100%',
        my: '0'
      },
      h2: {
        fontSize: 'md',
        lineHeight: '100%',
        my: '0'
      },
    },
    '.markdown-renderer': {
      maxHeight: '450px',
      whitespace: 'pre-wrap',
      //maxWidth: ['350px', '350px', '350px', '350px', '350px', '768px'],
      fontFamily: 'arial',
      pt: '5px',
      mx: '5px'
    }
  },
}

const components = {
  Input: InputStyle,
  Heading: HeadingStyle,
  Button: ButtonStyle,
  Accordion: AccordionStyle,
  AccordionBox: AccordionBoxStyle,
  Tag: TagStyle,
  Menu: MenuStyle,
  Textarea: TextareaStyle,
  Main: MainStyle,
  Select: SelectStyle,
  Carousel: CarouselStyle,
  Popover: PopoverStyle,
  CourseBox: CourseBoxStyle
}

const theme = extendTheme({ colors, config, fonts, fontSizes, styles, components })

export default theme;