import React, { useState, useEffect } from "react";
import jwt_decode from "jwt-decode";
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
import { GameInfoCardCustom } from "./GameInfoCard";
import {
  gameInfoListGetRequest,
  gameInfoDeleteRequest,
} from "../../api/catalog/catalogApi";
import {
  authorizationCheck,
  clearAuthorizationCookies,
  getBearerToken,
} from "../../api/authorization/authorizationApi";

import MoreVertIcon from "@material-ui/icons/MoreVert";

const axios = require(`axios`).default;
axios.defaults.baseURL = "https://localhost:44361/api/";

const useStyles = makeStyles({
  gameInfoCardBox: {
    marginBottom: "20px",
    marginLeft: "20px",
    marginRight: "20px",
  },
  gameInfoCardContainer: {
    margin: "auto",
  },
  addButton: {
    // backgroundColor: "#1e272e",
    height: "264px",
    width: "296px",
  },
  addIcon: {
    position: "relative",
    marginLeft: "47%",
    marginTop: "40%",
  },
  wishlistButton: {
    background: "linear-gradient(45deg, #e67e22 30%, #e74c3c 90%)",
    marginRight: 5,
    color: "white",
  },
  buttonsBox: {
    marginLeft: "5%",
  },
  crudButtonBox: {
    marginTop: 5,
    paddingTop: 5,
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

function AddGameInfoButton(props) {
  if (authorizationCheck()) {
    return (
      <CardActionArea className={props.className} href="catalog/create">
        <Card className={props.className}>
          <AddIcon className={props.iconClassname} />
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
        } else {
          throw error;
        }
      });
  }, []);

  return (
    <Container>
      <Grid className={classes.gameInfoCardContainer} container spacing={2}>
        <Grid item md>
          <AddGameInfoButton
            className={classes.addButton}
            iconClassname={classes.addIcon}
          />
        </Grid>
        {catalog.map(function (game) {
          return (
            <Grid style={{ position: "relative" }} item lg={3} key={game.id}>
              <GameInfoCard
                gameImageSource={game.picturePath}
                gameName={game.name}
                gameRating={game.rating}
              />
              {/* <MoreVertIcon
                style={{
                  position: "absolute",
                  top: "75%",
                  left: "85%",
                  // color: "#F8F8FF",
                }}
                fontSize="large"
              /> */}
              <Box className={classes.buttonsBox}>
                <Button className={classes.wishlistButton}>Wishlist</Button>
                <ButtonGroup
                  variant="contained"
                  className={classes.crudButtonBox}
                >
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
              </Box>
            </Grid>
          );
        })}
      </Grid>
    </Container>
  );
}
