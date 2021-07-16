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
  gameInfoListGetRequest,
  gameInfoDeleteRequest,
} from "../../api/catalog/catalogApi";
import { authorizationCheck } from "../../api/authorization/authorizationApi";

const axios = require(`axios`).default;
axios.defaults.baseURL = "https://localhost:44361/api/";

function sendDeleteRequest(name) {
  gameInfoDeleteRequest(name)
    .then((response) => {
      alert(response.data);
      postGameInfoDeleteRedirect();
    })
    .catch((error) => console.log(error));
}

function postGameInfoDeleteRedirect() {
  window.location.replace("/catalog");
}

function createButton() {
  if (authorizationCheck()) {
    return (
      <CardActionArea href="catalog/create">
        <Card>
          <AddIcon />
        </Card>
      </CardActionArea>
    );
  }
}

export default function CatalogView() {
  const [catalog, setCatalog] = useState([]);

  useEffect(() => {
    gameInfoListGetRequest()
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
              <Grid item key={game.id}>
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
          <Grid item>{createButton()}</Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
