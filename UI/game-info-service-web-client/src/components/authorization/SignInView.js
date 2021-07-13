import { Button, Grid, TextField } from "@material-ui/core";

import React, { useState } from "react";
import { AuthorizationCheck } from "./AuthorizationView";
import { signInUserRequest } from "../../api/identity/identityApi";

function sendLogin(username, password) {
  signInUserRequest(username, password)
    .then(function (response) {
      document.cookie = `Authorization=${response.data.accessToken}; Secure`;
    })
    .catch((error) => console.log(error))
    .then(() => postLoginRedirect());
}

function postLoginRedirect() {
  if (AuthorizationCheck()) {
    window.location.replace("/catalog");
  }
}

export default function SignInView(props) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  return (
    <Grid container direction="column" alignItems="center" spacing={2}>
      <Grid item>
        <TextField
          id="LoginField"
          label="Username:"
          onChange={(e) => setUsername(e.target.value)}
        />
      </Grid>
      <Grid item>
        <TextField
          id="PasswordField"
          type="password"
          label="Password:"
          onChange={(e) => setPassword(e.target.value)}
        />
      </Grid>
      <Grid item>
        <Grid container spacing={2}>
          <Grid item>
            <Button
              name="Confirm"
              variant="contained"
              color="primary"
              onClick={() => sendLogin(username, password)}
            >
              Log In
            </Button>
          </Grid>
          <Grid item>
            <Button href="/register" variant="contained" color="primary">
              Register
            </Button>
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
