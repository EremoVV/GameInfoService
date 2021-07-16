import React from "react";
import "./App.css";
import "./index.css";

import {
  BrowserRouter,
  Route,
  Switch,
  Redirect,
  useParams,
} from "react-router-dom";

import {
  Typography,
  Button,
  ButtonGroup,
  AppBar,
  Toolbar,
  Grid,
} from "@material-ui/core";

import DashboardIcon from "@material-ui/icons/Dashboard";
import InfoIcon from "@material-ui/icons/Info";

import { authorizationCheck } from "./api/authorization/authorizationApi";
import AuthorizationView from "./components/authorization/AuthorizationView";
import SignInView, {
  SignInFormikView,
} from "./components/authorization/SignInView";
import RegisterView, {
  RegisterViewFormik,
} from "./components/authorization/RegisterView";
import CatalogView from "./components/catalog/CatalogView";
import ProfileView from "./components/profile/ProfileView";
import GameInfoView from "./components/catalog/GameInfoView";
import Welcoming from "./components/Welcoming";
import ErrorBoundary from "./errorHandling/ErrorBoundary";

import ErrorComp from "./components/error";
import GameInfoUpdateView from "./components/catalog/GameInfoUpdateView";
import GameInfoCreateView from "./components/catalog/GameInfoCreateView";

// let clientCredentials = {
//   clientId : "ReactWebClient",
//   clientSecret : "a6a0ece0-0052-4678-82ae-ecc8817d489d",
//   grantType : "password",
//   scope : 'openid'
// };

// var gameInfoIdentityConfig = {
//   authority: "https://localhost:5000",
//   cliend_id: "React",
//   redurect_uri: "",
//   response_type: "id_token token",
//   scope: ["openid", "CatalogAPI"],
//   post_logout_redirect_uri: "http://localhost:6001/api/catalog/Catalog/Index"
// };

// function GameInfoCardManageButtons(props){
//   return(
//     <ButtonGroup>
//       <Button>
//         Wishlist
//       </Button>
//     </ButtonGroup>
//   );
// }

const axios = require(`axios`).default;
axios.defaults.baseURL = "https://localhost:44361/api/";

function GetGameInfoView() {
  let { name } = useParams();
  return <GameInfoView name={name} />;
}

function App() {
  return (
    <BrowserRouter>
      <AppBar position="static">
        <Toolbar>
          <Grid container>
            <Grid item>
              <Grid
                container
                spacing={3}
                alignContent="center"
                alignItems="center"
              >
                <Grid item>
                  <Typography>Game Info Service</Typography>
                </Grid>
                <Grid item>
                  <ButtonGroup variant="text" size="large">
                    <Button color="inherit" variant="outlined" href="/catalog">
                      <DashboardIcon />
                      <Typography>Catalog</Typography>
                    </Button>
                    <Button color="inherit" variant="outlined" href="/about">
                      <InfoIcon />
                      <Typography>About</Typography>
                    </Button>
                  </ButtonGroup>
                </Grid>
              </Grid>
            </Grid>
          </Grid>
          <AuthorizationView isLoggedIn={authorizationCheck()} />
        </Toolbar>
      </AppBar>
      <Grid
        container
        direction="column"
        alignItems="center"
        justifycontent="center"
      >
        <Grid item>
          <Switch>
            <Route path="/about" component={Welcoming} />
            <Route path="/catalog/create" component={GameInfoCreateView} />
            <Route
              path="/catalog/update/:name"
              component={GameInfoUpdateView}
            />
            <Route path="/catalog/:name" component={GetGameInfoView} />
            <Route exact path="/catalog" strict component={CatalogView} />
            <Route path="/login">
              <SignInFormikView />
            </Route>
            <Route path="/register">
              <RegisterViewFormik />
            </Route>
            <Route path="/profile">
              <ProfileView name="user" />
            </Route>
            <Route path="/error">
              <ErrorBoundary>
                <ErrorComp />
              </ErrorBoundary>
            </Route>
            <Redirect to="/about" />
          </Switch>
        </Grid>
      </Grid>
    </BrowserRouter>
  );
}

export default App;
