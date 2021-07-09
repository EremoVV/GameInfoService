import React from 'react';
import {Grid, CardActionArea, Card, Typography} from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';

import GameInfoCard from './GameInfoCard';
import {getBearerToken} from '../authorization/AuthorizationView'

function getCatalogItemsRequest(){
    const requestCatalogItems = {
      method : "GET",
      headers : {
        'Contnet-Type' : 'applications/json',
        'Authorization' : `Bearer ${getBearerToken()}`
      }
    }
    return fetch("https://localhost:44361/api/catalog/Catalog/Index", requestCatalogItems)
  }

export default class CatalogView extends React.Component{
    constructor(props){
      super(props)
      this.state = {
        catalog : [],
      }
      this.getCatalogItems = this.getCatalogItems.bind(this)
    }
    componentDidMount(){
      this.getCatalogItems()
    }
    async getCatalogItems(){
      let response = await getCatalogItemsRequest();
      let data = await response.json();
      this.state.catalog = data;
      this.forceUpdate();
      //.then(response => {this.state.catalog = response.json()});
    }
    render(){
      return(
        <Grid container direction="column" alignItems="center">
          <Grid item>
            <Typography variant="h2">Games:</Typography>
          </Grid>
          <Grid item>
          <Grid container direction="row" spacing={2}>
                {this.state.catalog.map(function(game){
                  return(
                    <Grid item>
                      <GameInfoCard gameImage={game.picture} gameName={game.name} gameRating={game.rating}/>
                    </Grid>
                  );
                })}
                <Grid item>
                  <CardActionArea href="/catalog/add">
                  <Card>
                    <AddIcon/>
                  </Card>
                  </CardActionArea>
                </Grid>
           </Grid>
          </Grid>
        </Grid>
      );
    }
  }