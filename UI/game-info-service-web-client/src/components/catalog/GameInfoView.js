import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Container, Grid, makeStyles, Typography } from "@material-ui/core";
import Rating from "@material-ui/lab/Rating";
import { Box } from "@material-ui/core";

import { gameInfoGetRequest } from "../../api/catalog/catalogApi";
import RatingView from "../common/RatingView";

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

const useStyles = makeStyles({
  container: {
    justifyContent: "center",
  },
});

const ratingLabels = {
  0: "",
  1: "Awful",
  2: "Waste of time",
  3: "Bad",
  4: "Meh",
  5: "Have problems",
  6: "Not bad",
  7: "Good",
  8: "Nice",
  9: "Awesome!",
  10: "Masterpiece!",
};

export default function GameInfoView() {
  const [gameData, setGameData] = useState([]);
  const [gameInfoRating, setGameInfoRating] = useState(-1);
  const [hoverIndex, setHoverIndex] = useState(-1);
  const { name } = useParams();

  const classes = useStyles();

  useEffect(() => {
    gameInfoGetRequest(name)
      .then((response) => {
        setGameData(response.data);
      })
      .catch((error) => console.log(error));
  }, []);
  return (
    <Container>
      <Typography>{gameData.picturePath}</Typography>
      <img src={gameData.picturePath} />
      <Box className={classes.container}>
        <Typography variant="h4">{gameData.name}</Typography>
        <Typography variant="h5">Rating</Typography>
        <RatingView variant="square" value={gameData.rating} />
        <Typography variant="h5">{gameData.description}</Typography>
        <Typography variant="h6">{gameData.releaseDate}</Typography>
        <Typography>Rate this game!</Typography>
        <Rating
          size="large"
          max={10}
          value={Number(gameData.rating)}
          onChange={(e) => setGameInfoRating(e.target.value)}
          onChangeActive={(event, hoverIndex) => {
            setHoverIndex(hoverIndex);
          }}
        />
        {ratingLabels[hoverIndex !== -1 ? hoverIndex : 0]}
      </Box>
    </Container>
  );
}
