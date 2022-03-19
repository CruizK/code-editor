import { Button } from "@chakra-ui/button";
import { Icon, ChevronLeftIcon, ChevronRightIcon } from "@chakra-ui/icons";
import { Image } from "@chakra-ui/image";
import { Box, Flex, Grid, GridItem, Heading, HStack, VStack } from "@chakra-ui/layout";
import { Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay } from "@chakra-ui/modal";
import { Tooltip } from "@chakra-ui/tooltip";
import paletteToRGB, { getRainbowAtIteration } from "@Utils/color";
import { useEffect, useState } from "react";
import Router from 'next/router';
import { Text, useStyleConfig } from "@chakra-ui/react";
import CourseAvatar from "@Modules/Courses/components/CourseAvatar/CourseAvatar";

/**
 * A component that allows horizontal, incremental scrolling
 * @param {{
 *      items: Array,
 *  }} props
 */
function Carousel(props) {
    const styles = useStyleConfig("Carousel", {});

    const { items } = props;
    const itemsPerPage = 3;
    const [page, setPage] = useState(1);

    const [subsetOfItems, setSub] = useState([]);

    function goToCourse(id) {
        let redirect = `/courses/${id}`; 
        Router.push(redirect);
    }

    // this grabs the current 'page' we're on
    useEffect(() => {
        const startIndex = (page * itemsPerPage) - itemsPerPage;
        // .slice end is non-inclusive
        // additionally, if the value passed into end exceeds the maximum size of the array, slice will just cut off at the last element
        const endIndex = page * itemsPerPage;
        setSub(items.slice(startIndex, endIndex));
    }, [page, items]);

    /**
     * This lowers the the page number by one
     * We don't want weird behavior with decreasing the page number, so setting a lower bound to it works best 
     */
    function decrementPage() { 
        setPage(Math.max(1, page - 1));
    }

    /**
     * This increases the page number by one
     * We don't want weird behavior with increasing the page number, so setting an upper bound to it works best 
     */
    function incrementPage() { 
        setPage(Math.min(page + 1, Math.ceil(items.length / itemsPerPage)));
    }

    return (
        <HStack spacing="35px" mt="15px" mb="15px" alignContent="center" justifyContent="start">
            <ChevronLeftIcon onClick={decrementPage}
                color={(page != 1) ? "ce_white" : "transparent"} 
                bgColor={(page != 1) ? "ce_mainmaroon" : "transparent"}
                boxSize="2em" borderRadius="2xl" 
            />
            {subsetOfItems.map((item, subsetIndex) => {
                const { id, title, author } = item;
                
                // page is assumed to at minimum be one, so this is fine to do
                let colorIterator = (((page - 1) * itemsPerPage) + subsetIndex) % 32;
                let color = getRainbowAtIteration(colorIterator, 0.3);

                return (
                    <Tooltip label={title} aria-label={title} placement="right" borderRadius="md">
                        <VStack __css={styles} borderColor={color} bgColor={color} spacing={0} onClick={() => goToCourse(id)}>
                            <Flex height="50%" w="100%" justifyContent="right" pr={1}>
                                <CourseAvatar identifier={id} />
                            </Flex>
                            <Flex alignItems="end"
                                height="50%" w="100%" pl={1}
                                color="ce_white" fontWeight="bold" fontFamily="button" fontSize="md"
                            >
                                <Text isTruncated>
                                    {title.toUpperCase()}
                                </Text>
                            </Flex>
                        </VStack>
                    </Tooltip>
                );
            })}
            <ChevronRightIcon onClick={incrementPage}
                color={(page != Math.ceil(items.length / itemsPerPage)) ? "ce_white" : "transparent"} 
                bgColor={(page != Math.ceil(items.length / itemsPerPage)) ? "ce_mainmaroon" : "transparent"}
                boxSize="2em" borderRadius="2xl" 
            />
        </HStack>
    )
}

export default Carousel;