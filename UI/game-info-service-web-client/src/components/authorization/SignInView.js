import {Button, Grid, TextField} from '@material-ui/core';

import React from 'react';
import {AuthorizationCheck} from './AuthorizationView'

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

export default class SignInView extends React.Component{
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