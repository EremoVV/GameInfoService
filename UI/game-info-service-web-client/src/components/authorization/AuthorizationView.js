import {makeStyles} from '@material-ui/core/styles';
import {Avatar, Button, ButtonGroup, Grid} from '@material-ui/core';

const useStyles = makeStyles({
  gridItem: {
    padding: 10
  },
});

function ClearAuthorizationCookies(){
  document.cookie = "Authorization="
}

function AuthorizationCheck(){
  if(getBearerToken() === "" || getBearerToken() == null) return false;
  else return true;
}

function getBearerToken(){
  return document.cookie.split('=')[1];
}

export default function AuthorizationView(props){
  const isLoggedIn = props.isLoggedIn;
  const classes = useStyles();
  if(isLoggedIn){
    return(
        <Grid justifycontent="flex-end" direction="row-reverse" container className={classes.grid}>
          <Grid item className={classes.gridItem}>
            <Avatar alt="U">
              <Button href="/profile"/>
            </Avatar>
          </Grid>
          <Grid item className={classes.gridItem}>
            <ButtonGroup size="large">
              <Button variant="outlined" color="inherit" href="/catalog" onClick={ClearAuthorizationCookies}>Logout</Button>
            </ButtonGroup>
          </Grid>
        </Grid>
    );
  }
  else{
    return(
      <Grid container justifycontent="flex-end" direction="row-reverse">
        <Grid item>
        <ButtonGroup size="large">
          <Button className="button" color="inherit" href="/login">Sign In</Button>
        </ButtonGroup>
        </Grid>
      </Grid>
    );
  }
}

export {AuthorizationCheck, AuthorizationView, getBearerToken, ClearAuthorizationCookies}