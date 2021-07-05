import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';
import {Typography, Link, Button, TextField, AppBar, Toolbar} from '@material-ui/core';
import {UserManager} from 'oidc-client'
import {BrowserRouter, Route, Switch} from 'react-router-dom'

let clientCredentials = {
  clientId : "ReactWebClient",
  clientSecret : "a6a0ece0-0052-4678-82ae-ecc8817d489d",
  grantType : "password",
  scope : 'openid'
};

var config = {
  authority: "https://localhost:5000",
  cliend_id: "React",
  redurect_uri: "",
  response_type: "id_token token",
  scope: "openid",
  post_logout_redirect_uri: "http://localhost:6000/Catalog/Index"
}

var gameInfoUserManager = new UserManager(config)

let bearerToken = ""
class LoginView extends React.Component{
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
    bearerToken = data.token;
  }

  render(){
    return(
      <div>
        <label>{this.state.token}</label>
      <form>
      <div>
        <TextField id="LoginField" label="Username:" onChange={e =>{this.state.username = e.target.value}}/>
      </div>
      <div>
        <TextField id="PasswordField" type="password" label="Password:" onChange={e=> {this.state.password = e.target.value}}/>
      </div>
    </form>
    <Button name="Confirm" variant="contained" color="primary" onClick={this.SendLogin}>Log In</Button>
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
      {/* <div>
        <TextField id="NameField" label="Name:"/>
      </div>
      <div>
        <TextField id="SurNameField" label="SurName:"/>
      </div> */}
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

class UsersView extends React.Component{
  render(){
    return(
      <div>
        <Button variant="contained" color="secondary">GetUsers</Button>
      </div>
    );
  }
}

class CatalogView extends React.Component{
  constructor(props){
    super(props)
    this.state = {
      catalog : {}
    }
    this.getCatalogItems = this.getCatalogItems.bind(this)
  }

  getCatalogItems(){
    getCatalogItemsRequest()
    .then(response => {this.state.catalog = response.json()});
  }
  render(){
    return(
      <div>
        <div>Item1</div>
        <div>Item2</div>
        <Button variant="contained" color="secondary" onClick={this.getCatalogItems}>Get catalog items</Button>
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
  return fetch("https://localhost:44307/Account/SignInUser", requestLogin);
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
  fetch("https://localhost:44307/Account/Register}", requestRegister);
}

function getUsersRequest(){
  const requestUsers = {
    method: "GET",
    headers: {
      'Content-Type': 'application/json'
  }
  }
  return fetch("https://localhost:44307/Account/GetUsers", requestUsers)
}

function getCatalogItemsRequest(){
  alert(`BearerToken : ${bearerToken}`)
  const requestCatalogItems = {
    method : "GET",
    headers : {
      'Contnet-Type' : 'applications/json',
      'www-authenticate' : `Bearer ${bearerToken}`
    }
  }
  return fetch("https://localhost:44307/Catalog/Index", requestCatalogItems)
}



class Main extends React.Component{
  render(){
    return(
      <div>
        <div>
          <CatalogView/>
        </div>
        <div>
          <LoginView/>
        </div>
        {/* <div>
          <RegisterView/>
        </div> */}
      </div>

    );
  }
}

function MainWindow(){
  return(
    <html>
      <head>
        <title>Game Info Service</title>
      </head>
      <body>

        <BrowserRouter>
        <AppBar position="static">
          <Toolbar>
          <Button color="inherit" href="/catalog">Catalog</Button>
          <Button color="inherit" href="/login">Login</Button>
          <Button color="inherit" href="/register">Register</Button>
          </Toolbar>
        </AppBar>
          <Switch>
            <Route path="/catalog">
              <CatalogView/>
            </Route>
            <Route path="/login">
              <LoginView/>
            </Route>
            <Route path="/register">
              <RegisterView/>
            </Route>
          </Switch>
        </BrowserRouter>

      </body>
    </html>

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
