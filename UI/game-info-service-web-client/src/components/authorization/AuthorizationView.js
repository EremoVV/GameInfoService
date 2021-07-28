import { makeStyles } from "@material-ui/core/styles";
import {
  Box,
  Avatar,
  Button,
  ButtonGroup,
  Grid,
  Typography,
} from "@material-ui/core";
import { clearAuthorizationCookies } from "../../api/authorization/authorizationApi";

const useStyles = makeStyles({
  authorizationBox: {
    display: "flex",
    marginLeft: "30%",
  },
});

export default function AuthorizationView(props) {
  const isLoggedIn = props.isLoggedIn;
  const classes = useStyles();
  if (isLoggedIn) {
    return (
      <Box className={classes.authorizationBox}>
        <Avatar alt="U">
          <Button href="/profile" />
        </Avatar>
        <Button
          color="inherit"
          href="/catalog"
          onClick={clearAuthorizationCookies}
        >
          <Typography>Logout</Typography>
        </Button>
      </Box>
    );
  } else {
    return (
      <Box className={classes.authorizationBox}>
        <Button className="button" color="inherit" href="/login">
          <Typography>Sign In</Typography>
        </Button>
      </Box>
    );
  }
}
