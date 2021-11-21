import { Button } from "@chakra-ui/button";
import { ChevronLeftIcon, ChevronRightIcon } from "@chakra-ui/icons";
import { Box, Grid, GridItem, HStack } from "@chakra-ui/layout";
import { Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay } from "@chakra-ui/modal";
import { useEffect, useState } from "react";

/**
 * A component that allows horizontal, incremental scrolling
 * @param {{
 *      items: Array,
 *  }} props
 */
function Carousel(props) {
    const { items } = props;
    const itemsPerPage = 4;
    const [page, setPage] = useState(1);

    const [subsetOfItems, setSub] = useState([]);

    // this grabs the current 'page' we're on
    useEffect(() => {
        const startIndex = (page * itemsPerPage) - itemsPerPage;
        // .slice end is non-inclusive
        // additionally, if the value passed into end exceeds the maximum size of the array, slice will just cut off at the last element
        const endIndex = page * itemsPerPage;
        setSub(items.slice(startIndex, endIndex));
    }, [page]);

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
        <HStack spacing="35px" mt="15px" mb="15px" alignContent="center" justifyContent="center">
            {page != 1 &&
                <ChevronLeftIcon onClick={decrementPage} />
            }
            {subsetOfItems.map((item) => {
                const { id, title, author } = item;
                let smallTitle = (title.length > 6) ? title.substr(0, 6) + "..." : title;
                return (
                    <Grid height="70px" w="122px" border="solid green 1px" borderRadius="md" bgColor="green">
                        <GridItem colStart={2} colSpan={2} display="flex" justifyContent="right" mr={1}>
                            Java
                        </GridItem>
                        <GridItem colSpan={2}  display="flex" alignItems="end">
                            {smallTitle}
                        </GridItem>
                    </Grid>
                );
            })}
            {page != Math.ceil(items.length / itemsPerPage) &&
                <ChevronRightIcon onClick={incrementPage} />
            }
        </HStack>
    )
}

export default Carousel;