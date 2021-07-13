import React, { useState, useEffect } from "react";
import { Grid, CardActionArea, Card, Typography } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";

import GameInfoCard from "./GameInfoCard";
import { getProductsRequest } from "../../api/catalog/catalogApi";

const axios = require(`axios`).default;
axios.defaults.baseURL = "https://localhost:44361/api/";

export default function CatalogView() {
  const [catalog, setCatalog] = useState([]);

  useEffect(() => {
    getProductsRequest()
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
