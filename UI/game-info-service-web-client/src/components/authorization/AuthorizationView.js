import { makeStyles } from "@material-ui/core/styles";
import { Avatar, Button, ButtonGroup, Grid } from "@material-ui/core";
import { clearAuthorizationCookies } from "../../api/authorization/authorizationApi";

const useStyles = makeStyles({
  gridItem: {
    padding: 10,
  },
});

export default function AuthorizationView(props) {
  const isLoggedIn = props.isLoggedIn;
  const classes = useStyles();
  if (isLoggedIn) {
    return (
      <Grid
        justifycontent="flex-end"
        direction="row-reverse"
        container
        className={classes.grid}
      >
        <Grid item className={classes.gridItem}>
          <Avatar alt="U">
            <Button href="/profile" />
          </Avatar>
        </Grid>
        <Grid item className={classes.gridItem}>
          <ButtonGroup size="large">
            <Button
              variant="outlined"
              color="inherit"
              href="/catalog"
              onClick={clearAuthorizationCookies}
            >
              Logout
            </Button>
          </ButtonGroup>
        </Grid>
      </Grid>
    );
  } else {
    return (
      <Grid container justifycontent="flex-end" direction="row-reverse">
        <Grid item>
          <ButtonGroup size="large">
            <Button className="button" color="inherit" href="/login">
              Sign In
            </Button>
          </ButtonGroup>
        </Grid>
      </Grid>
    );
  }
}
