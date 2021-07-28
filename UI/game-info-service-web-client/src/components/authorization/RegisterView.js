import React, { useState } from "react";
import { Button, Grid, TextField, makeStyles } from "@material-ui/core";
import { registerUserRequest } from "../../api/identity/identityApi";
import { useFormik } from "formik";
import { DatePicker } from "@material-ui/pickers";
import * as yup from "yup";

function registerUser(username, email, password, country, city, birthdayDate) {
  registerUserRequest(username, email, password, country, city, birthdayDate)
    .then(() => postRegisterRedirect())
    .catch((error) => console.log(error));
}

function postRegisterRedirect() {
  window.location.replace("/login");
}

const useStyles = makeStyles({
  form: {
    display: "flex",
    flexDirection: "column",
    paddingTop: "5%",
    margin: "auto",
    width: "25%",
  },
  textField: {
    marginBottom: "20px",
  },
  birthdayDateField: {
    marginBottom: "20px",
  },
  button: {
    backgroundColor: "#1e272e",
  },
});

const validationSchema = yup.object({
  username: yup.string("Enter username").min(3).required("Username required"),
  email: yup
    .string("Enter email")
    .min(3)
    .email("Enter correct email")
    .required("Email required"),
  password: yup.string("Enter password").min(3).required("Password required"),
  confirmPassword: yup
    .string("Confirm password:")
    .required("Please confirm password"),
});

export default function RegisterView() {
  const classes = useStyles();
  const formik = useFormik({
    initialValues: {
      username: "",
      email: "",
      password: "",
      confirmPassword: "",
      country: "",
      city: "",
      birthdayDate: new Date(),
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      registerUser(
        values.username,
        values.email,
        values.password,
        values.country,
        values.city,
        values.birthdayDate
      );
    },
  });

  return (
    <form className={classes.form} onSubmit={formik.handleSubmit}>
      <TextField
        required
        className={classes.textField}
        id="username"
        label="Username:"
        value={formik.values.username}
        onChange={formik.handleChange}
        error={formik.touched.username && Boolean(formik.errors.username)}
        helperText={formik.touched.username && formik.errors.username}
      />
      <TextField
        required
        className={classes.textField}
        id="email"
        label="Email:"
        type="email"
        value={formik.values.email}
        onChange={formik.handleChange}
        error={formik.touched.email && Boolean(formik.errors.email)}
        helperText={formik.touched.email && formik.errors.email}
      />
      <TextField
        required
        className={classes.textField}
        id="password"
        label="Password:"
        type="password"
        value={formik.values.password}
        onChange={formik.handleChange}
        error={formik.touched.password && Boolean(formik.errors.password)}
        helperText={formik.touched.password && formik.errors.password}
      />
      <TextField
        required
        className={classes.textField}
        id="confirmPassword"
        label="Confirm password:"
        type="password"
        value={formik.values.confirmPassword}
        onChange={formik.handleChange}
        error={
          formik.touched.confirmPassword &&
          Boolean(formik.errors.confirmPassword)
        }
        helperText={
          formik.touched.confirmPassword && formik.errors.confirmPassword
        }
      />
      <TextField
        className={classes.textField}
        id="country"
        label="Contry:"
        value={formik.values.country}
        onChange={formik.handleChange}
        error={formik.touched.country && Boolean(formik.errors.country)}
        helperText={formik.touched.country && formik.errors.country}
      />
      <TextField
        className={classes.textField}
        id="city"
        label="City:"
        value={formik.values.city}
        onChange={formik.handleChange}
        error={formik.touched.city && Boolean(formik.errors.city)}
        helperText={formik.touched.city && formik.errors.city}
      />
      {/* <TextField
          className={classes.textField}
          id="birthdayDate"
          type="date"
          value={formik.values.birthdayDate}
          onChange={formik.handleChange}
          error={
            formik.touched.birthdayDate && Boolean(formik.errors.birthdayDate)
          }
          helperText={formik.touched.birthdayDate && formik.errors.birthdayDate}
        /> */}
      <DatePicker
        className={classes.birthdayDateField}
        id="birthdayDate"
        label="Birthday Date:"
        openTo="year"
        format="dd/MM/yyyy"
        views={["year", "month", "date"]}
        value={formik.values.gameReleaseDate2}
        onChange={(data) => {
          formik.setFieldValue("gameReleaseDate", data);
          console.log(data);
        }}
        error={
          formik.touched.birthdayDate && Boolean(formik.errors.birthdayDate)
        }
        helperText={formik.touched.birthdayDate && formik.errors.birthdayDate}
      />
      <Button
        className={classes.button}
        variant="contained"
        color="primary"
        type="submit"
      >
        Register
      </Button>
    </form>
  );
}
