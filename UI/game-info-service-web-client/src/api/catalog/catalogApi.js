import axios from "axios";
import setCurrentAuthorizationHeader from "../authorization/authorizationApi";

export async function gameInfoListGetRequest() {
  setCurrentAuthorizationHeader();
  return await axios.get("catalog/Catalog/Index");
}

export async function gameInfoGetRequest(gameName) {
  return await axios.get(`catalog/Catalog/GetGameInfoByName`, {
    params: { name: gameName },
  });
}

export async function gameInfoDeleteRequest(gameName) {
  return await axios.delete("catalog/Catalog/DeleteInfo", {
    params: { name: gameName },
  });
}

export async function gameInfoCreateRequest(
  gameName,
  gameDescriprion,
  gameRating,
  gameReleaseDate
) {
  return await axios.post("catalog/Catalog/AddGameInfo", {
    Name: gameName,
    Description: gameDescriprion,
    Rating: gameRating,
    ReleaseDate: gameReleaseDate,
  });
}

export async function gameInfoUpdateRequest(
  gameId,
  gameName,
  gameDescriprion,
  gameRating,
  gameReleaseDate
) {
  return await axios.post("catalog/Catalog/UpdateGameInfo", {
    Id: gameId,
    Name: gameName,
    Description: gameDescriprion,
    Rating: gameRating,
    ReleaseDate: gameReleaseDate,
  });
}

export async function userNameRequest() {
  return await axios.get("catalog/Catalog/Header");
}
