import React, { useState, useEffect } from "react";
import {
  Box,
  Button,
  ButtonGroup,
  Grid,
  CardActionArea,
  Card,
  Typography,
  makeStyles,
  Container,
} from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";

import GameInfoCard from "./GameInfoCard";
import {
  gameInfoListGetRequest,
  gameInfoDeleteRequest,
  userNameRequest,
} from "../../api/catalog/catalogApi";
import {
  authorizationCheck,
  clearAuthorizationCookies,
} from "../../api/authorization/authorizationApi";

const axios = require(`axios`).default;
axios.defaults.baseURL = "https://localhost:44361/api/";

const useStyles = makeStyles({
  gameInfoCardBox: {
    marginBottom: "20px",
    marginLeft: "20px",
    marginRight: "20px",
  },
  addButton: {
    // backgroundColor: "#1e272e",
    height: "300px",
    width: "296px",
  },
  addIcon: {
    marginLeft: "auto",
    marginRight: "auto",
  },
});

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

function loginRedirect() {
  window.location.replace("/login");
}

function CreateButton(props) {
  if (authorizationCheck()) {
    return (
      <CardActionArea className={props.className} href="catalog/create">
        <Card className={props.className}>
          <Box display="flex">
            <AddIcon className={props.iconClassname} />
          </Box>
        </Card>
      </CardActionArea>
    );
  }
  return null;
}

export default function CatalogView() {
  const [catalog, setCatalog] = useState([]);
  const classes = useStyles();

  useEffect(() => {
    gameInfoListGetRequest()
      .then((response) => {
        setCatalog(response.data);
      })
      .catch((error) => {
        console.log(error);
        if (error.response) {
          if (error.response.status === 401) {
            clearAuthorizationCookies();
            loginRedirect();
          }
        }
      });
  }, []);

  return (
    <Container>
      <Typography variant="h2">Games:</Typography>
      <Grid container spacing={2}>
        <Grid item xs>
          <CreateButton className={classes.addButton} />
        </Grid>
        {catalog.map(function (game) {
          return (
            <Grid item sm={3} id={game.id}>
              <GameInfoCard
                gameImageSource={game.picturePath}
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
      </Grid>
    </Container>
  );
}
