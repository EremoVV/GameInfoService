import React, { useState } from "react";
import { Button, Grid, TextField } from "@material-ui/core";
import { registerUserRequest } from "../../api/identity/identityApi";

// function sendRegisterRequest(
//   username,
//   email,
//   password,
//   country,
//   city,
//   birthdayDate
// ) {
//   const requestRegister = {
//     method: "POST",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify({

//     }),
//   };
//   fetch(
//     "https://localhost:44361/api/identity/Account/Register}",
//     requestRegister
//   );
// }

function registerUser(username, email, password, country, city, birthdayDate) {
  registerUserRequest(
    username,
    email,
    password,
    country,
    city,
    birthdayDate
  ).catch((error) => console.log(error));
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
