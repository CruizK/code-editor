import { Box, Image } from "@chakra-ui/react";
import { courseSvg } from "@Modules/Courses/Courses";


function CourseAvatar(props) {
    const defaultOptions = {
        foreground: [255, 255, 255, 255],               // rgba white
        background: [255, 255, 255, 0],         // rgba transparent
        margin: 0.2,                              // 20% margin
        size: 128,                                // 128px square
        format: 'svg'                             // use SVG instead of PNG
    };
    const { identifier, options = defaultOptions } = props;
    
    const svgData = courseSvg(identifier.toString(), options);

    return (
        <Image src={"data:image/svg+xml;base64," + svgData} />
    )
}

export default CourseAvatar;