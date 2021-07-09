import {Grid, Typography} from '@material-ui/core';

export default function Welcoming(){
    return(
        <Grid container>
          <Grid item>
            <Grid container>
              <Grid item>
                <Typography variant="h2">Welcome to Game Info Service!</Typography>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
    );
  }