import { Box, Button, TextField, makeStyles } from "@material-ui/core";
import * as yup from "yup";
import { useFormik } from "formik";

import React, { useState } from "react";
import { authorizationCheck } from "../../api/authorization/authorizationApi";
import { signInUserRequest } from "../../api/identity/identityApi";

const validationSchema = yup.object({
  username: yup.string("Enter username").required("Username required"),
  password: yup.string("Enter your password").required("Password is required"),
});

const useStyles = makeStyles({
  form: {
    display: "flex",
    alignItems: "center",
    flexDirection: "column",
    paddingTop: "5%",
  },
  textfield: {
    marginBottom: "10px",
  },
  button: {
    backgroundColor: "#1e272e",
    marginBottom: "5px",
    marginLeft: "5px",
    marginRight: "5px",
  },
});

export default function SignInView() {
  const classes = useStyles();
  const formik = useFormik({
    initialValues: {
      username: "",
      password: "",
    },
    validationSchema: validationSchema,
    onSubmit: (values) => sendLogin(values.username, values.password),
  });
  return (
    <form className={classes.form} onSubmit={formik.handleSubmit}>
      <TextField
        className={classes.textfield}
        id="username"
        label="Username:"
        value={formik.values.username}
        onChange={formik.handleChange}
        error={formik.touched.username && Boolean(formik.errors.username)}
        helperText={formik.touched.username && formik.errors.username}
      />
      <TextField
        className={classes.textfield}
        id="password"
        type="password"
        label="Password:"
        value={formik.values.password}
        onChange={formik.handleChange}
        error={formik.touched.password && Boolean(formik.errors.password)}
        helperText={formik.touched.password && formik.errors.password}
      />
      <Box display="flex">
        <Button
          className={classes.button}
          name="Confirm"
          variant="contained"
          color="primary"
          type="submit"
        >
          Log In
        </Button>
        <Button
          className={classes.button}
          href="/register"
          variant="contained"
          color="primary"
        >
          Register
        </Button>
      </Box>
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
  if (authorizationCheck()) {
    window.location.replace("/catalog");
  }
}
