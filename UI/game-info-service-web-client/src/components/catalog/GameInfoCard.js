import {
  CardActionArea,
  Card,
  CardMedia,
  CardContent,
  Typography,
} from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import RatingView from "../common/RatingView";

const useStyles = makeStyles({
  card: {
    minWidth: 210,
    minHeight: 300,
  },
  rating: {
    backgroundColor: "#e1b12c",
  },
  image: {
    height: 200,
    width: 300,
  },
});

export default function GameInfoCard(props) {
  const classes = useStyles();

  return (
    <CardActionArea href={`/catalog/${props.gameName}`}>
      {/* <CardMedia
        className={classes.media}
        src="https://images5.alphacoders.com/690/thumb-1920-690653.png"
      /> */}
      <Card className={classes.card}>
        <img className={classes.image} src={props.gameImageSource}></img>
        <CardContent>
          <Typography>{props.gameName}</Typography>
          <RatingView value={props.gameRating} />
        </CardContent>
      </Card>
    </CardActionArea>
  );
}
