import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {Button} from '@material-ui/core';
import {TextField} from '@material-ui/core'

let ClientCredentials = {
  clientId : "ReactWebClient",
  clientSecret : "a6a0ece0-0052-4678-82ae-ecc8817d489d",
  grantType : "password",
  scope : 'openid'
};

function LoginView(){

    const [username, setUsername] = useState()
    const [password, setPassword] = useState()

    function SendLogin(){
      const requestLogin = {
        method: "POST",
        headers: {
          'Content-Type': 'application/problem+json; charset=utf-8',
    
      },
        body: JSON.stringify({
          userName: username,
          Password: password,
          ClientId: ClientCredentials.clientId,
          ClientSecret: ClientCredentials.clientSecret,
          GrantType: ClientCredentials.grantType,
          Scope : ClientCredentials.scope
        })
      };
      fetch("https://localhost:44307/Account/SignInUser", requestLogin);
    }

    return(
      <div>
        <form>
        <div>
          <TextField id="LoginField" label="Username:" onChange={e => setUsername(e.target.value)}/>
        </div>
        <div>
          <TextField id="PasswordField" type="password" label="Password:" onChange={e=> setPassword(e.target.value)}/>
        </div>
      </form>
      <Button name="Confirm" variant="contained" color="primary" onClick={SendLogin}>Log In</Button>
      </div>
    );
}


class LoginViewClass extends React.Component{
  constructor(props){
    super(props);

    this.state = {password : "username"}
    this.state = {username : ""}
  }
  
  render(){
    return(
      <div>
      <form>
      <div>
        <TextField id="LoginField" label="Username:" defaultValue={this.state.username} onChange={e => {this.setState({password : e.target.value})}}/>
      </div>
      <div>
        <TextField id="PasswordField" type="password" label="Password:" onChange={e=> {this.state.password = e.target.value}}/>
      </div>
    </form>
    <Button name="Confirm" variant="contained" color="primary" onClick={() => {SendLogin(this.state.username, this.state.password)}}>Log In</Button>
    </div>
    );
  }
}

function SendLogin(username, password){
  alert(`${username} ${password}`)
  const requestLogin = {
    method: "POST",
    headers: {
      'Content-Type': 'application/json',

  },
    body: JSON.stringify({
      UserName: "user",
      Password: password,
      ClientId: "ReactWebClient",
      ClientSecret: "a6a0ece0-0052-4678-82ae-ecc8817d489d",
      GrantType: "password",
      Scope : "openid"
    })
  };
  fetch("https://localhost:44307/Account/SignInUser", requestLogin);
}


// function Login(){
//   return(
//     <div>
//       <form>
//         <h1>{ResponseFromApi}</h1>
//       <div>
//         <TextField id="LoginField" label="Username:"/>
//       </div>
//       <div>
//         <TextField id="PasswordField" type="password" label="Password:"/>
//       </div>
//     </form>
//     <Button name="Confirm" variant="contained" color="primary" onClick={SendLogin}>Log In</Button>
//     </div>
//   );
// }

function Register(props){
  return(
    <div>
    <form>
    <div>
      <TextField id="UsernameField" label="Username:"/>
    </div>
    {/* <div>
      <TextField id="NameField" label="Name:"/>
    </div>
    <div>
      <TextField id="SurNameField" label="SurName:"/>
    </div> */}
    <div>
      <TextField id="EmailField" type="email" label="Email:"/>
    </div>
    <div>
      <TextField id="PasswordField" type="password" label="Password:"/>
    </div>
    <div>
      <TextField id="PasswordConfirmField" type="password" label="Confirm password:"/>
    </div>
    <div>
      <TextField id="CountryField" label="County:"/>
    </div>
    <div>
      <TextField id="CityField" label="City:"/>
    </div>
    <div>
      <TextField id="BirthdayDateField" label="Birthday:" type="date" defaultValue="2000-01-01"/>
    </div>
  </form>
  <Button name="Confirm" variant="contained" color="primary">Register</Button>
  </div>
  );
}
function GetUsers(){
  const requestLogin = {
    method: "GET",
    headers: {
      'Content-Type': 'application/json',

  }
  }
  fetch("https://localhost:44307/Account/GetUsers")
}

function SendRegister(){
  const requestRegister = {
    method: "POST",

  };
  fetch("https://localhost:44307/Account/Register}", requestRegister);
}

ReactDOM.render(
    <LoginViewClass/>,
    document.getElementById('root')
  );


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
