import {
  Avatar,
  CardActionArea,
  Card,
  CardMedia,
  CardContent,
  Typography,
  ButtonGroup,
  Button,
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
});

export default function GameInfoCard(props) {
  const classes = useStyles();

  return (
    <CardActionArea href={`/catalog/${props.gameName}`}>
      <Card className={classes.card}>
        <CardMedia>{/* {props.gameImage} */}</CardMedia>
        <CardContent>
          <Typography>{props.gameName}</Typography>
          <RatingView value={props.gameRating} />
        </CardContent>
      </Card>
    </CardActionArea>
  );
}
