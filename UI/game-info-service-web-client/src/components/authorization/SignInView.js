import { Button, Grid, TextField } from "@material-ui/core";
import * as yup from "yup";
import { useFormik } from "formik";

import React, { useState } from "react";
import { AuthorizationCheck } from "./AuthorizationView";
import { signInUserRequest } from "../../api/identity/identityApi";

const validationSchema = yup.object({
  username: yup.string("Enter username").required("username required"),
  password: yup.string("Enter your password").required("Password is required"),
});

export function SignInFormikView() {
  const formik = useFormik({
    initialValues: {
      username: "",
      password: "",
    },
    validationSchema: validationSchema,
    onSubmit: (values) => sendLogin(values.username, values.password),
  });
  return (
    <form onSubmit={formik.handleSubmit}>
      <TextField
        id="username"
        label="Username:"
        value={formik.values.username}
        onChange={formik.handleChange}
        error={formik.touched.username && Boolean(formik.errors.username)}
        helperText={formik.touched.username && formik.errors.username}
      />
      <TextField
        id="password"
        type="password"
        label="Password:"
        value={formik.values.password}
        onChange={formik.handleChange}
        error={formik.touched.password && formik.errors.password}
        helperText={formik.touched.password && formik.errors.password}
      />
      <Button name="Confirm" variant="contained" color="primary" type="submit">
        Log In
      </Button>
      <Button href="/register" variant="contained" color="primary">
        Register
      </Button>
    </form>
  );
}

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
