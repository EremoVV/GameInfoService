import axios from "axios";
import setCurrentAuthorizationHeader from "../authorization/authorizationApi";

export async function getGameInfosRequest() {
  setCurrentAuthorizationHeader();
  return await axios.get("catalog/Catalog/Index");
}

export async function getGameInfoRequest(gameName) {
  setCurrentAuthorizationHeader();
  return await axios.get(`catalog/Catalog/GetGameInfoByName`, {
    params: { name: gameName },
  });
}

export async function deleteGameInfoRequest(gameName) {
  setCurrentAuthorizationHeader();
  return await axios.delete("catalog/Catalog/DeleteInfo", {
    params: { name: gameName },
  });
}
