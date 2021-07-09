import {Avatar, Grid, Typography} from '@material-ui/core';

export default function ProfileView(props){
    return (
      <Grid container direction="column" alignItems="center" spacing={2}>
        <Grid item>
          <Typography variant="h3">Welcome to your profile!</Typography>
        </Grid>
        <Grid item>
          <Avatar size="lagre">{props.name}</Avatar>
        </Grid>
      </Grid>
    );
  }