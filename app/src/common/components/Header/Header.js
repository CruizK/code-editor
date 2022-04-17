import { Box, Flex, Grid, GridItem, Heading, HStack } from "@chakra-ui/layout";
import { Image, textDecoration } from "@chakra-ui/react";
import { Avatar } from "@chakra-ui/avatar";
import { Menu, MenuButton, MenuItem, MenuList } from "@chakra-ui/menu";
import SNoLink from "@Components/SNoLink/SNoLink";
import { loggedIn } from "@Modules/Auth/Auth";
import { ChevronDownIcon } from "@chakra-ui/icons";
import { getRole, getName } from "@Utils/jwt";
import { useCookies } from "react-cookie";

/**
 * Contains the shared header for each page. Only render user icon if logged in.
 */
 function Header(props) {
    const [cookies, setCookie, removeCookie] = useCookies(["user"]);
    const isLoggedIn = loggedIn(cookies.user);
    const userRole = (isLoggedIn) ? getRole(cookies.user) : "None";
    const username = (isLoggedIn) ? getName(cookies.user) : "<name>";
    
    const profileImage = "/defaults/avatar.png"; // TODO: Update with actual avatar.

    return(
        <Box height="50px" bgColor="ce_darkgrey" w="100%" color="ce_white">
            <Grid templateColumns="repeat(5, 1fr)" gap={6} pl={5} pr={5}>
                <GridItem>
                    <SNoLink href="/home"><Image src="/siu_logo.png" alt="SIU Logo" maxHeight="50px" /></SNoLink>
                </GridItem>
                <GridItem colSpan={2} colEnd={6}>
                    <Flex height="100%" justifyContent="right" alignItems="center">
                        {isLoggedIn && 
                        <HStack spacing={6}>
                            {(userRole == "Teacher") && 
                                <SNoLink href="/dashboard/teacher" borderBottom="2px" borderColor="transparent" _hover={{ borderBottom: "2px"}}>My Content</SNoLink>
                            }
                            {(userRole == "Student") &&
                                <SNoLink href="/dashboard" borderBottom="2px" borderColor="transparent" _hover={{ borderBottom: "2px"}}>My Courses</SNoLink>
                            }
                            <Box id="name">
                            <Menu>                                
                                <MenuButton fontWeight="bold" borderBottom="2px" borderColor="transparent" _hover={{ borderBottom: "2px"}}>
                                    {username}
                                    <ChevronDownIcon width="2em" />
                                </MenuButton>
                                <MenuList color="ce_black">
                                    <MenuItem><SNoLink href="/auth/settings">My Account</SNoLink></MenuItem>
                                    <MenuItem><SNoLink href="/auth/logout">Sign Out</SNoLink></MenuItem>
                                </MenuList>
                            </Menu>
                            </Box>
                        </HStack>
                        }
                        
                    </Flex>
                </GridItem>
            </Grid>
        </Box>
    )
}

export default Header;