import React, { useState } from "react";
import {
  Button,
  Container,
  Grid,
  TextField,
  makeStyles,
} from "@material-ui/core";
import { registerUserRequest } from "../../api/identity/identityApi";
import { useFormik } from "formik";
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
  },
  textField: {
    marginBottom: "20px",
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

export function RegisterViewFormik() {
  const classes = useStyles();
  const formik = useFormik({
    initialValues: {
      username: "",
      email: "",
      password: "",
      confirmPassword: "",
      country: "",
      city: "",
      birthdayDate: [],
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
    <Container>
      <form className={classes.form} onSubmit={formik.handleSubmit}>
        <TextField
          className={classes.textField}
          id="username"
          label="Username:"
          value={formik.values.username}
          onChange={formik.handleChange}
          error={formik.touched.username && Boolean(formik.errors.username)}
          helperText={formik.touched.username && formik.errors.username}
        />
        <TextField
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
        <TextField
          className={classes.textField}
          id="birthdayDate"
          type="date"
          value={formik.values.birthdayDate}
          onChange={formik.handleChange}
          error={
            formik.touched.birthdayDate && Boolean(formik.errors.birthdayDate)
          }
          helperText={formik.touched.birthdayDate && formik.errors.birthdayDate}
        />
        <Button color="primary" type="submit">
          Register user
        </Button>
      </form>
    </Container>
  );
}

export default function RegisterView(props) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("example@email.com");
  const [country, setCountry] = useState("");
  const [city, setCity] = useState("");
  const [birthdayDate, setBirthdayDate] = useState(new Date());

  return (
    <Grid
      container
      direction="column"
      alignItems="center"
      justifycontent="center"
      spacing={2}
    >
      <Grid item>
        <TextField
          id="UsernameField"
          label="Username:"
          onChange={(e) => setUsername(e.target.value)}
        />
      </Grid>
      <Grid item>
        <TextField
          id="EmailField"
          type="email"
          label="Email:"
          onChange={(e) => setEmail(e.target.value)}
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
        <TextField
          id="PasswordConfirmField"
          type="password"
          label="Confirm password:"
        />
      </Grid>
      <Grid item>
        <TextField
          id="CountryField"
          label="County:"
          onChange={(e) => setCountry(e.target.value)}
        />
      </Grid>
      <Grid item>
        <TextField
          id="CityField"
          label="City:"
          onChange={(e) => setCity(e.target.value)}
        />
      </Grid>
      <Grid item>
        <TextField
          id="BirthdayDateField"
          label="Birthday:"
          type="date"
          defaultValue="2000-01-01"
          onChange={(e) => setBirthdayDate(e.target.value)}
        />
      </Grid>
      <Grid item>
        <Button
          name="Confirm"
          variant="contained"
          color="primary"
          onClick={() => {
            registerUser(
              username,
              email,
              password,
              country,
              city,
              birthdayDate
            );
          }}
        >
          Register
        </Button>
      </Grid>
    </Grid>
  );
}
