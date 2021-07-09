import React from 'react';
import {Grid, Typography} from '@material-ui/core';
import {getBearerToken} from '../authorization/AuthorizationView'
import Rating from '@material-ui/lab/Rating';

function getGameInfoRequest(gameName){
    const requestGameInfo = {
      method : "GET",
      headers : {
        'Contnet-Type' : 'applications/json',
        'Authorization' : `Bearer ${getBearerToken()}`
      }
    }
    return fetch(`https://localhost:44361/api/catalog/Catalog/GetGameInfoByName?name=${gameName}`, requestGameInfo);
  }

export default class GameInfoView extends React.Component{
    constructor(props){
      super(props)
      this.state = {
        name : this.props.name,
        data : {},
      }
    }
  componentDidMount(){
    this.LoadGameInfo();
  }
  
  async LoadGameInfo(){
    let response = await getGameInfoRequest(this.state.name);
    let data = await response.json();
    this.state.data = data;
    this.forceUpdate();
  }
  
    render(){
      return (
        <Grid container spacing={4}>
        <Grid item>
          <Typography>{this.state.data.picture}</Typography>
        </Grid>
        <Grid item>
          <Grid container direction="column" spacing={2}>
            <Grid item>
            <Typography variant="h4">{this.state.name}</Typography>
            </Grid>
            <Grid item>
              <Typography variant="h5">{this.state.data.description}</Typography>
            </Grid>
            <Grid item>
              <Typography variant="h6">{this.state.data.releaseDate}</Typography>
            </Grid>
            <Grid item>
              <Rating size="large" precision={0.1} max={10} value={Number(this.state.data.rating)} disabled/>
            </Grid>
            <Grid item>
            {this.state.data.rating}
            </Grid>
          </Grid>
        </Grid>
      </Grid>
      );
    }
  }