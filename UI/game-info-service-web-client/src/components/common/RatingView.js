import { Avatar } from "@material-ui/core";
import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles({
  rating: {
    width: "100px",
    height: "100px",
  },
  red: {
    backgroundColor: "#575fcf",
  },
  yellow: {
    backgroundColor: "#4bcffa",
  },
  green: {
    backgroundColor: "#ffa801",
  },
  greenBright: {
    backgroundColor: "#ff3f34",
  },
});

export default function RatingView(props) {
  const classes = useStyles();
  const num = Number(props.value).toPrecision(2);
  if (props.value >= 9) {
    return (
      <Avatar variant={props.variant} className={classes.greenBright}>
        {num}
      </Avatar>
    );
  }
  if (props.value <= 4) {
    return (
      <Avatar variant={props.variant} className={classes.red}>
        {num}
      </Avatar>
    );
  }
  if (props.value > 4 && props.value < 7) {
    return (
      <Avatar variant={props.variant} className={classes.yellow}>
        {num}
      </Avatar>
    );
  }
  if (props.value >= 7 && props.value < 9) {
    return (
      <Avatar variant={props.variant} className={classes.green}>
        {num}
      </Avatar>
    );
  }
  return <Avatar>{num}</Avatar>;
}
