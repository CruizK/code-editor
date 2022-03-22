import { useToast } from "@chakra-ui/react";
import { maxAgeInHours, verify } from "@Modules/Auth/Auth";
import { useRouter } from "next/router";
import { useEffect } from "react";
import { useCookies } from "react-cookie";

function Verify() {
  const router = useRouter();
  const [cookies, setCookie] = useCookies(["user"]);
  const toast = useToast();

  router.query.token


  useEffect( async () => {
    if(router.query.token) {
      try {
        const res = await verify(router.query.token);

        const token = res.data;
  
        if(token) {
          setCookie("user", token, {
              path: "/",
              maxAge: maxAgeInHours * 60 * 60, //seconds
              sameSite: true,
          })
  
          router.push('/home');
        }
      }
      catch(e) {
        router.push('/');
        toast({
          title: 'Verification Token Invalid',
          status: 'error',
          duration: 3000,
          isClosable: true,
          position: 'top'
        })
      }
    }
  }, [router])

  return (
    <div>Verifying...</div>
  )
}

export default Verify;