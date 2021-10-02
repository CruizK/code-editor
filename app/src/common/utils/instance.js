
import axios from "axios";

// Create an instance using the config defaults provided by the library
const instance = axios.create();

// request url
// usually just our domain, but it might change for dev if we dont have the api under the same url as the frontend
instance.defaults.baseURL = process.env.NEXT_PUBLIC_API;

// START HEADERS

// post
instance.defaults.headers.post['Content-Type'] = 'application/json';

// END HEADERS


export default instance;