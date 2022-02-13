import { Text } from "@chakra-ui/react";
import ContentStateInlineStyle from "draft-js/lib/ContentStateInlineStyle";
import React from "react";

const sizeMapping = {
  'one': 'xl',
  'two': 'lg',
  'three': 'md',
  'four': 'sm',
  'five': 'xs',
  'six': 'xs'
}

class CustomText extends React.Component {

  constructor(props) {
    super(props);

    console.log(props);
  }

  render() {
    return <Text fontSize={this.props.blockProps.fontSize}>{this.props.block.getText()}</Text>
  }
}

export default function blockRenderer(contentBlock) {
  const type = contentBlock.getType();
  const text = contentBlock.getText();
  console.log(text);
  // Format is header-size
  if(type.startsWith('header')) {
    const size = type.split('-')[1];
    return {
      component: CustomText,
      props: {
        fontSize: sizeMapping[size],
        children: text
      }
    }
  }
}