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

const useStyles = makeStyles({
  card: {
    minWidth: 210,
    minHeight: 300,
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
          <Avatar>{props.gameRating}</Avatar>
          <Button>Add to wishlist</Button>
          <ButtonGroup>
            <Button href={`/catalog/update/${props.gameName}`}>Update</Button>
            <Button>Remove</Button>
          </ButtonGroup>
        </CardContent>
      </Card>
    </CardActionArea>
  );
}
