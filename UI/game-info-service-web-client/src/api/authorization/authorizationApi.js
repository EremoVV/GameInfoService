import { getBearerToken } from "../../components/authorization/AuthorizationView";
import axios from "axios";

export default function setCurrentAuthorziationHeader() {
  axios.defaults.headers.common["Authorization"] = `Bearer ${getBearerToken()}`;
}
