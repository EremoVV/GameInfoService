import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';
import {Avatar, Typography, Link, Button, ButtonGroup, Card, CardMedia, CardActionArea, TextField, AppBar, Toolbar, CardContent, Grid, SvgIcon, createMuiTheme} from '@material-ui/core';
import {UserManager} from 'oidc-client';
import {BrowserRouter, Route, Switch} from 'react-router-dom';
import {makeStyles} from '@material-ui/core/styles';
import Rating from '@material-ui/lab/Rating';
import DashboardIcon from '@material-ui/icons/Dashboard';

let clientCredentials = {
  clientId : "ReactWebClient",
  clientSecret : "a6a0ece0-0052-4678-82ae-ecc8817d489d",
  grantType : "password",
  scope : 'openid'
};

var gameInfoIdentityConfig = {
  authority: "https://localhost:5000",
  cliend_id: "React",
  redurect_uri: "",
  response_type: "id_token token",
  scope: ["openid", "CatalogAPI"],
  post_logout_redirect_uri: "http://localhost:6001/api/catalog/Catalog/Index"
};

const theme = createMuiTheme({
  spacing: 4,
})

const useStyles = makeStyles({
  login: {
    padding: '0 30 30 30px'
  },
  card: {
    minWidth: 210,
    minHeight: 300,
  },
  grid: {
    maxWidth: 1903
  },
  catalogGrid: {
    padding: theme.spacing(2)
  },
  gridItem: {
    padding: 10
  },
});


function getBearerToken(){
  return document.cookie.split('=')[1];
}

function ProfileView(props){
  return (
    <div>
      <h1>Welcome to your profile!</h1>
      <Avatar>{props.name}</Avatar>
    </div>
  );
}

class SignInView extends React.Component{
  constructor(props){
    super(props);

    this.state = {
      username : "",
      password : "",
      token : []
    }

    this.sendLogin = this.sendLogin.bind(this)
  }

  async sendLogin(){
    const response = await sendLoginRequest(this.state.username, this.state.password);
    const data = await response.json();
    document.cookie = `Authorization=${data.accessToken}; Secure`;
  }

  render(){
    return(
      <div>
      <form>
      <div>
        <TextField id="LoginField" label="Username:" onChange={e =>{this.state.username = e.target.value}}/>
      </div>
      <div>
        <TextField id="PasswordField" type="password" label="Password:" onChange={e=> {this.state.password = e.target.value}}/>
      </div>
    </form>
    <Button name="Confirm" variant="contained" color="primary" onClick={this.sendLogin}>Log In</Button>
    <p>or</p>
    <Button href="/register" variant="contained" color="primary">Register</Button>
    </div>
    );
  }
}

class RegisterView extends React.Component{
  constructor(props){
    super(props);
    this.state = {
      username : "",
      password : "",
      email : "example@mail.com",
      country : "",
      city : "",
      birthdayDate : new Date()
    }
  }
  render(){
    return(
      <div>
      <form>
      <div>
        <TextField id="UsernameField" label="Username:" onChange={e => this.state.username = e.target.value}/>
      </div>
      <div>
        <TextField id="EmailField" type="email" label="Email:" onChange={e => this.state.email = e.target.value}/>
      </div>
      <div>
        <TextField id="PasswordField" type="password" label="Password:" onChange={e => this.state.password = e.target.value}/>
      </div>
      <div>
        <TextField id="PasswordConfirmField" type="password" label="Confirm password:"/>
      </div>
      <div>
        <TextField id="CountryField" label="County:" onChange={e => this.state.country = e.target.value}/>
      </div>
      <div>
        <TextField id="CityField" label="City:" onChange={e => this.state.city = e.target.value}/>
      </div>
      <div>
        <TextField id="BirthdayDateField" label="Birthday:" type="date" defaultValue="2000-01-01" onChange={e => this.state.birthdayDate = e.target.value}/>
      </div>
    </form>
    <Button name="Confirm" variant="contained" color="primary" onClick={() => {sendRegisterRequest(this.state.username, this.state.email, this.state.password, this.state.country, this.state.city, this.state.birthdayDate)}}>Register</Button>
    </div>
    );
  }
}

const GameInfoCard = ({gameImage, gameName, gameRating}) => {
  const classes = useStyles();
  return (
    <CardActionArea>
    <Card className={classes.card}>
      <CardMedia>
        {gameImage}
      </CardMedia>
      <CardContent>
        <Typography>
          {gameName}
        </Typography>
        {/* <Rating precision={0.1} max={10} readOnly value={gameRating} size="small"></Rating> */}
        <Avatar>{gameRating}</Avatar>
      </CardContent>
    </Card>
  </CardActionArea>
  );
}

class CatalogView extends React.Component{
  constructor(props){
    super(props)
    this.state = {
      catalog : [],
    }
    this.getCatalogItems = this.getCatalogItems.bind(this)
  }
  componentDidMount(){
    this.getCatalogItems()
  }
  async getCatalogItems(){
    let response = await getCatalogItemsRequest();
    let data = await response.json();
    this.state.catalog = data;
    this.forceUpdate();
    //.then(response => {this.state.catalog = response.json()});
  }
  render(){
    return(
      <div>
          <h1>Games:</h1>
          <Grid container spacing={3} direction="row" alignItems="flex-start" >
          {this.state.catalog.map(game => (
              <Grid item >
                <GameInfoCard href={`/${game.name}`} gameImage={game.picture} gameName={game.name} gameRating={game.rating}/>  
              </Grid>
            ))}
         </Grid>
      </div>
    );
  }
}

function sendLoginRequest(username, password){
  const requestLogin = {
    method: "POST",
    headers: {
      'Content-Type': 'application/json',

  },
    body: JSON.stringify({
      UserName: username,
      Password: password,
      ClientId: "ReactWebClient",
      ClientSecret: "a6a0ece0-0052-4678-82ae-ecc8817d489d",
      GrantType: "password",
      Scope : "openid"
    })
  };
  return fetch("https://localhost:44361/api/identity/Account/SignInUser", requestLogin);
}

function sendRegisterRequest(
  username, email, password, country, city, birthdayDate
){
  const requestRegister = {
    method: "POST",
    headers: {
      'Content-Type': 'application/json'
    },
    body : JSON.stringify({
      UserName : username,
      Email : email,
      Password : password,
      Country : country,
      City : city,
      BirthdayDate : birthdayDate
    })
  };
  fetch("https://localhost:44361/api/identity/Account/Register}", requestRegister);
}

function getCatalogItemsRequest(){
  const requestCatalogItems = {
    method : "GET",
    headers : {
      'Contnet-Type' : 'applications/json',
      'Authorization' : `Bearer ${getBearerToken()}`
    }
  }
  return fetch("https://localhost:44361/api/catalog/Catalog/Index", requestCatalogItems)
}

function ClearCookies(){
  document.cookie = "Authorization="
}


function MyButton(){
  const classes = useStyles();
  return <Button className={classes.login}>Button</Button>
}


function AuthorizationCheck(){
  if(getBearerToken() === "" || getBearerToken == null) return false;
  else return true;
}

function AuthorizationView(props){
  const isLoggedIn = props.isLoggedIn;
  const classes = useStyles();
  if(isLoggedIn){
    return(
      <div>
        <Grid container className={classes.grid}>
          <Grid item className={classes.gridItem}>
            <Avatar alt="U">
              <Button href="/profile"/>
            </Avatar>
          </Grid>
          <Grid item className={classes.gridItem}>
            <ButtonGroup size="large">
              <Button variant="outlined" color="inherit" href="/catalog" onClick={ClearCookies}>Logout</Button>
            </ButtonGroup>
          </Grid>
        </Grid>
      </div>
    );
  }
  else{
    return(
      <ButtonGroup size="large">
        <Button className="button" color="inherit" href="/login">Sign In</Button>
      </ButtonGroup>
    );
  }

}

function Welcoming(){
  return(
    <div>
      <h3>Welcome to our site!</h3>
    </div>
  );
}

function MainWindow(){
  return(
    <div>
      <BrowserRouter>
        <AppBar position="static">
          <Toolbar>

            <ButtonGroup variant="text" size="large">
              <Button color="inherit" variant="outlined" href="/home">
                <SvgIcon><path d="M10 20v-6h4v6h5v-8h3L12 3 2 12h3v8z"/></SvgIcon>
                Game Info Service
              </Button>
              <Button color="inherit" variant="outlined" href="/catalog">
                <DashboardIcon/>
                Catalog
              </Button>
            </ButtonGroup>
            <AuthorizationView isLoggedIn={AuthorizationCheck()}/>
          </Toolbar>
        </AppBar>
          <Switch>
            <Route path="/home">
              <Welcoming/>
            </Route>
            <Route path="/catalog">
              <CatalogView/>
            </Route>
            <Route path="/login">
              <div>
                <div>
                <SignInView/>
                </div>
              </div>
           </Route>
            <Route path="/register">
              <RegisterView/> 
            </Route>
            <Route path="/profile">
              <ProfileView name="user"/>
            </Route>
          </Switch>
      </BrowserRouter>
    </div>
  );
}

ReactDOM.render(
  MainWindow(),
  document.getElementById('root')
);
 





// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
