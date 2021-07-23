import axios from "axios";
import CookeManager from "js-cookie-manager";

const authorizationCookieKey = "Authorization";
let cookieManager = new CookeManager();

export default function setCurrentAuthorziationHeader() {
  axios.defaults.headers.common["Authorization"] = `Bearer ${getBearerToken()}`;
}

export function clearAuthorizationCookies() {
  cookieManager.remove(authorizationCookieKey);
}
export function authorizationCheck() {
  if (getBearerToken() === "" || getBearerToken() == null) return false;
  else return true;
}

export function getBearerToken() {
  return cookieManager.get(authorizationCookieKey);
}
