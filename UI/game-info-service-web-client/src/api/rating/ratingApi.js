import axios from "axios";

export async function gameInfoRatingAppendRequest(userId, gameInfoId, rating) {
  return await axios.post("rating/Rating/AppendRating", {
    Id: 0,
    UserId: userId,
    GameInfoId: gameInfoId,
    Rating: rating,
  });
}

export async function getGameInfoRatingRequest(userId, gameId) {
  let params = { UserId: userId, GameId: gameId };
  return await axios.get("rating/Rating/GetGameInfoRating", {
    params,
  });
}
