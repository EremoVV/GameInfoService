import {
  Box,
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
    minHeight: 250,
  },
  rating: {
    marginLeft: 20,
  },
  cardMedia: {
    height: 190,
    width: 300,
  },
  cardContent: {
    display: "flex",
    alignItems: "center",
  },
  ralativeBox: {
    position: "relative",
  },
  absoluteBox: {
    position: "absolute",
    top: 10,
    left: 10,
  },
  gameTitle: {
    margin: "auto",
  },
});

export default function GameInfoCard(props) {
  const classes = useStyles();

  return (
    <CardActionArea href={`/catalog/${props.gameName}`}>
      <Card className={classes.card} style={{ position: "relative" }}>
        <CardMedia
          className={classes.cardMedia}
          image={`http://localhost:8080/Images/${props.gameImageSource}`}
        />
        <CardContent className={classes.cardContent}>
          <Typography className={classes.gameTitle} variant="h6">
            {props.gameName}
          </Typography>
          <Box style={{ position: "absolute", top: "53%", left: "83%" }}>
            <RatingView className={classes.rating} value={props.gameRating} />
          </Box>
        </CardContent>
      </Card>
    </CardActionArea>
  );
}

export function GameInfoCardCustom(props) {
  const classes = useStyles();
  return (
    <Box className={classes.ralativeBox}>
      <CardMedia
        className={classes.image}
        image={`http://localhost:8080/Images/${props.gameImageSource}`}
      />
      <Box className={classes.absoluteBox}>
        <RatingView className={classes.rating} value={props.gameRating} />
      </Box>
    </Box>
  );
}
