import {Avatar, CardActionArea, Card, CardMedia, CardContent, Typography} from '@material-ui/core';
import {makeStyles} from '@material-ui/core/styles';

const useStyles = makeStyles({
    card: {
      minWidth: 210,
      minHeight: 300,
    },
  });

export default function GameInfoCard(gameImage, gameName, gameRating){
    const classes = useStyles();
    return (
      <CardActionArea href={`/catalog/${gameName}`}>
      <Card className={classes.card}>
        <CardMedia>
          {/* {gameImage} */}
        </CardMedia>
        <CardContent>
          <Typography>
            {/* {gameName} */}
          </Typography>
          <Avatar>{gameRating}</Avatar>
        </CardContent>
      </Card>
    </CardActionArea>
    );
  }