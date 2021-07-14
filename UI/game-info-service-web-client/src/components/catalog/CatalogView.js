import React, { useState, useEffect } from "react";
import {
  Button,
  ButtonGroup,
  Grid,
  CardActionArea,
  Card,
  Typography,
} from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";

import GameInfoCard from "./GameInfoCard";
import {
  getGameInfosRequest,
  deleteGameInfoRequest,
} from "../../api/catalog/catalogApi";

const axios = require(`axios`).default;
axios.defaults.baseURL = "https://localhost:44361/api/";

function sendDeleteRequest(name) {
  deleteGameInfoRequest(name)
    .then((response) => alert(response))
    .catch((error) => console.log(error));
}

export default function CatalogView() {
  const [catalog, setCatalog] = useState([]);

  useEffect(() => {
    getGameInfosRequest()
      .then((response) => {
        setCatalog(response.data);
      })
      .catch((error) => console.log(error));
  }, []);

  return (
    <Grid container direction="column" alignItems="center">
      <Grid item>
        <Typography variant="h2">Games:</Typography>
      </Grid>
      <Grid item>
        <Grid container direction="row" spacing={2}>
          {catalog.map(function (game) {
            return (
              <Grid item>
                <GameInfoCard
                  gameImage={game.picture}
                  gameName={game.name}
                  gameRating={game.rating}
                />
                <Button>Add to wishlist</Button>
                <ButtonGroup>
                  <Button color="primary" href={`/catalog/update/${game.name}`}>
                    Update
                  </Button>
                  <Button
                    color="secondary"
                    onClick={() => {
                      sendDeleteRequest(game.name);
                    }}
                  >
                    Remove
                  </Button>
                </ButtonGroup>
              </Grid>
            );
          })}
          <Grid item>
            <CardActionArea>
              <Card>
                <AddIcon />
              </Card>
            </CardActionArea>
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
