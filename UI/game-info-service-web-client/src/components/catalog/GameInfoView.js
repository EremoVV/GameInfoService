import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Container, makeStyles, Typography } from "@material-ui/core";
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
    display: "flex",
    marginRight: "50%",
  },
  gameInfo: {
    marginLeft: 10,
    marginTop: 50,
    padding: 5,
  },
  image: {
    margin: "50px 50px 50px 50px",
    width: 900,
    height: 508,
  },
});

const ratingLabels = {
  0: "",
  1: " 1/10 Awful",
  2: " 2/10 Waste of time",
  3: " 3/10 Bad",
  4: " 4/10 Meh",
  5: " 5/10 5Have problems",
  6: " 6/10 Not bad",
  7: " 7/10 Good",
  8: " 8/10 Nice",
  9: " 9/10 Awesome!",
  10: " 10/10 Masterpiece!",
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
      .catch((error) => {
        console.log(error);
      });
  }, []);
  return (
    <Container className={classes.container}>
      <img
        className={classes.image}
        alt={gameData.picturePath}
        src={`http://localhost:8080/Images/${gameData.picturePath}`}
      />
      <Box className={classes.gameInfo}>
        <Typography variant="h4">{gameData.name}</Typography>
        <Typography variant="h5">Rating</Typography>
        <RatingView variant="square" value={gameData.rating} />
        <Typography variant="h5">{gameData.description}</Typography>
        <Typography variant="h6">
          {new Date(gameData.releaseDate).toDateString()}
        </Typography>
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
