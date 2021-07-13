import axios from "axios";

export async function signInUserRequest(username, password) {
  return await axios.post("identity/Account/SignInUser", {
    UserName: username,
    Password: password,
    ClientId: "ReactWebClient",
    ClientSecret: "a6a0ece0-0052-4678-82ae-ecc8817d489d",
    GrantType: "password",
    Scope: "openid",
  });
}

export async function registerUserRequest(
  username,
  email,
  password,
  country,
  city,
  birthdayDate
) {
  return await axios.post("identity/Account/Register", {
    UserName: username,
    Email: email,
    Password: password,
    Country: country,
    City: city,
    BirthdayDate: birthdayDate,
  });
}
