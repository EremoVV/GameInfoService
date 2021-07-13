import axios from "axios";
import { getBearerToken } from "../../components/authorization/AuthorizationView";

export async function getProductsRequest() {
  axios.defaults.headers.common["Authorization"] = `Bearer ${getBearerToken()}`;
  return await axios.get("catalog/Catalog/Index");
}

export async function getGameInfo(gameName) {
  axios.defaults.headers.common["Authorization"] = `Bearer ${getBearerToken()}`;
  return await axios.get(`catalog/Catalog/GetGameInfoByName`, {
    params: { name: gameName },
  });
}
