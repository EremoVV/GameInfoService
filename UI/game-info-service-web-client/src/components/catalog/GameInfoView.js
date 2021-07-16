import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Grid, Typography } from "@material-ui/core";
import Rating from "@material-ui/lab/Rating";

import { gameInfoGetRequest } from "../../api/catalog/catalogApi";

// function getGameInfoRequest(gameName) {
//   const requestGameInfo = {
//     method: "GET",
//     headers: {
//       "Contnet-Type": "applications/json",
//       Authorization: `Bearer ${getBearerToken()}`,
//     },
//   };
//   return fetch(
//     `https://localhost:44361/api/catalog/Catalog/GetGameInfoByName?name=${gameName}`,
//     requestGameInfo
//   );
// }

export default function GameInfoView() {
  const [gameData, setGameData] = useState([]);
  const { name } = useParams();

  useEffect(() => {
    gameInfoGetRequest(name)
      .then((response) => {
        setGameData(response.data);
      })
      .catch((error) => console.log(error));
  }, []);
  return (
    <Grid container spacing={4}>
      <Grid item>
        <Typography>{gameData.picture}</Typography>
      </Grid>
      <Grid item>
        <Grid container direction="column" spacing={2}>
          <Grid item>
            <Typography variant="h4">{gameData.name}</Typography>
          </Grid>
          <Grid item>
            <Typography variant="h5">{gameData.description}</Typography>
          </Grid>
          <Grid item>
            <Typography variant="h6">{gameData.releaseDate}</Typography>
          </Grid>
          <Grid item>
            <Rating
              size="large"
              precision={0.1}
              max={10}
              value={Number(gameData.rating)}
              disabled
            />
          </Grid>
          <Grid item>{gameData.rating}</Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
