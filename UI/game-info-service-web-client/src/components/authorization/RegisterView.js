import React from 'react';
import {Button, Grid, TextField} from '@material-ui/core';

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

export default class RegisterView extends React.Component{
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