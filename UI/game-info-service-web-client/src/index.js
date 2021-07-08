import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';
import {Avatar, Typography, Button, ButtonGroup, Card, CardMedia, CardActionArea, TextField, AppBar, Toolbar, CardContent, Grid, createMuiTheme} from '@material-ui/core';
import {BrowserRouter, Route, Switch, Redirect, useParams} from 'react-router-dom';
import {makeStyles} from '@material-ui/core/styles';
import Rating from '@material-ui/lab/Rating';
import DashboardIcon from '@material-ui/icons/Dashboard';
import InfoIcon from '@material-ui/icons/Info';

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
    <Grid container direction="column" alignItems="center" spacing={2}>
      <Grid item>
        <Typography variant="h3">Welcome to your profile!</Typography>
      </Grid>
      <Grid item>
        <Avatar size="lagre">{props.name}</Avatar>
      </Grid>
    </Grid>
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

    this.sendLogin = this.sendLogin.bind(this);
    this.postLoginRedirect = this.postLoginRedirect.bind(this);
  }

  async sendLogin(){
    const response = await sendLoginRequest(this.state.username, this.state.password);
    const data = await response.json();
    document.cookie = `Authorization=${data.accessToken}; Secure`;
    return(this.postLoginRedirect());
  }

  postLoginRedirect(){
    if(AuthorizationCheck()){
        window.location.replace("/catalog");
    }
  }

  render(){
    return(
      <Grid container direction="column" alignItems="center" spacing={2}>
        <Grid item>
          <TextField id="LoginField" label="Username:" onChange={e => this.setState({username: e.target.value})}/>
        </Grid>
        <Grid item>
        <TextField id="PasswordField" type="password" label="Password:" onChange={e=>this.setState({password: e.target.value})}/>
        </Grid>
        <Grid item>
        <Grid container spacing={2}>
          <Grid item>
          <Button name="Confirm" variant="contained" color="primary" onClick={this.sendLogin}>Log In</Button>
          </Grid>
          <Grid item>
          <Button href="/register" variant="contained" color="primary">Register</Button>
          </Grid>
        </Grid>
        </Grid>
      </Grid>
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
      <Grid container direction="column" alignItems="center" justifycontent="center" spacing={2}>
        <Grid item>
        <TextField id="UsernameField" label="Username:" onChange={e => this.setState({username : e.target.value})}/>
        </Grid>
        <Grid item>
        <TextField id="EmailField" type="email" label="Email:" onChange={e => this.setState({email : e.target.value})}/>
        </Grid>
        <Grid item>
        <TextField id="PasswordField" type="password" label="Password:" onChange={e => this.setState({password : e.target.value})}/>
        </Grid>
        <Grid item>
        <TextField id="PasswordConfirmField" type="password" label="Confirm password:"/>
        </Grid>
        <Grid item>
        <TextField id="CountryField" label="County:" onChange={e => this.setState({country : e.target.value})}/>
        </Grid>
        <Grid item>
        <TextField id="CityField" label="City:" onChange={e => this.setState({city : e.target.value})}/>
        </Grid>
        <Grid item>
        <TextField id="BirthdayDateField" label="Birthday:" type="date" defaultValue="2000-01-01" onChange={e => this.setState({birthdayDate : e.target.value})}/>
        </Grid>
        <Grid item>
        <Button name="Confirm" variant="contained" color="primary" onClick={() => {
          sendRegisterRequest(this.state.username, this.state.email, this.state.password, 
                              this.state.country, this.state.city, this.state.birthdayDate)
          }}>Register
          </Button>
        </Grid>
        </Grid>
    );
  }
}

const GameInfoCard = ({gameImage, gameName, gameRating}) => {
  const classes = useStyles();
  return (
    <CardActionArea href={`/catalog/${gameName}`} className={classes.CardActionArea}>
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
      <Grid container direction="column" alignItems="center">
        <Grid item>
        <h1>Games:</h1>
        </Grid>
        <Grid item>
        <Grid container direction="row" spacing={2}>
              {this.state.catalog.map(function(game){
                return(
                  <Grid item>
                    <GameInfoCard gameImage={game.picture} gameName={game.name} gameRating={game.rating}/>
                  </Grid>
                );
              })}
         </Grid>
        </Grid>
      </Grid>
    );
  }
}

class GameInfoView extends React.Component{
  constructor(props){
    super(props)
    this.state = {
      name : this.props.name,
      data : {},
    }
  }
componentDidMount(){
  this.LoadGameInfo();
}

async LoadGameInfo(){
  let response = await getGameInfoRequest(this.state.name);
  let data = await response.json();
  this.state.data = data;
  this.forceUpdate();
}

  render(){
    return (
      <Grid container spacing={4}>
      <Grid item>
        <Typography>{this.state.data.picture}</Typography>
      </Grid>
      <Grid item>
        <Grid container direction="column" spacing={2}>
          <Grid item>
          <Typography variant="h4">{this.state.name}</Typography>
          </Grid>
          <Grid item>
            <Typography variant="h5">{this.state.data.description}</Typography>
          </Grid>
          <Grid item>
            <Typography variant="h6">{this.state.data.releaseDate}</Typography>
          </Grid>
          <Grid item>
            <Rating size="large" precision={0.1} max={10} value={Number(this.state.data.rating)} disabled/>
          </Grid>
          <Grid item>
          {this.state.data.rating}
          </Grid>
        </Grid>
      </Grid>
    </Grid>
    );
  }
}


function GetGameInfoView(){
  let {name} = useParams();
  return(
    <GameInfoView name={name}/>
  );
}

function getGameInfoRequest(gameName){
  const requestGameInfo = {
    method : "GET",
    headers : {
      'Contnet-Type' : 'applications/json',
      'Authorization' : `Bearer ${getBearerToken()}`
    }
  }
  return fetch(`https://localhost:44361/api/catalog/Catalog/GetGameInfoByName?name=${gameName}`, requestGameInfo);
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

function AuthorizationCheck(){
  if(getBearerToken() === "" || getBearerToken() == null) return false;
  else return true;
}

function AuthorizationView(props){
  const isLoggedIn = props.isLoggedIn;
  const classes = useStyles();
  if(isLoggedIn){
    return(
        <Grid justifycontent="flex-end" direction="row-reverse" container className={classes.grid}>
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
    );
  }
  else{
    return(
      <Grid container justifycontent="flex-end" direction="row-reverse">
        <Grid item>
        <ButtonGroup size="large">
          <Button className="button" color="inherit" href="/login">Sign In</Button>
        </ButtonGroup>
        </Grid>
      </Grid>
    );
  }

}

function Welcoming(){
  return(
      <Grid container>
        <Grid item>
          <Grid container>
            <Grid item>

            </Grid>
            <Grid item>
              <Typography variant="h2">Welcome to Game Info Service!</Typography>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
  );
}

function MainWindow(){
  return(
      <BrowserRouter>
        <AppBar position="static">
          <Toolbar>
            <Grid container>
              <Grid item>
                <Grid container spacing={3} alignContent="center" alignItems="center">
                  <Grid item>
                  <Typography>Game Info Service</Typography>
                  </Grid>
                  <Grid item>
                    <ButtonGroup variant="text" size="large">
                      <Button color="inherit" variant="outlined" href="/catalog">
                        <DashboardIcon/>
                        <Typography>Catalog</Typography>
                      </Button>
                      <Button color="inherit" variant="outlined" href="/about">
                        <InfoIcon/>
                        <Typography>About</Typography>
                      </Button>
                     </ButtonGroup>
                  </Grid>
                </Grid>
              </Grid>
            </Grid>
            <AuthorizationView isLoggedIn={AuthorizationCheck()}/>
          </Toolbar>
        </AppBar>
        <Grid container direction="column" alignItems="center" justifycontent="center">
          <Grid item>
          <Switch>
          <Route path="/about" component={Welcoming}>
            </Route>
            <Route path="/catalog/:name">
              <GetGameInfoView/>
            </Route>
            <Route exact path="/catalog" strict>
              <CatalogView/>
            </Route>
            <Route path="/login">
              <SignInView/>
           </Route>
            <Route path="/register">
              <RegisterView/> 
            </Route>
            <Route path="/profile">
              <ProfileView name="user"/>
            </Route>
            <Redirect to="/about"/>
          </Switch>
          </Grid>
        </Grid>
      </BrowserRouter>
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
