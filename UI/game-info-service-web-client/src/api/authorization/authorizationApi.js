import axios from "axios";

export default function setCurrentAuthorziationHeader() {
  axios.defaults.headers.common["Authorization"] = `Bearer ${getBearerToken()}`;
}

export function clearAuthorizationCookies() {
  document.cookie = "Authorization=";
}

export function authorizationCheck() {
  if (getBearerToken() === "" || getBearerToken() == null) return false;
  else return true;
}

export function getBearerToken() {
  return document.cookie.split("=")[1];
}
