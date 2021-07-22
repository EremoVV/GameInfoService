import { Avatar } from "@material-ui/core";
import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles({
  rating: {
    width: "100px",
    height: "100px",
  },
  bad: {
    backgroundColor: "#fc5c65",
  },
  average: {
    backgroundColor: "#f7b731",
  },
  good: {
    backgroundColor: "#26de81",
  },
  excellent: {
    backgroundColor: "#20bf6b",
  },
});

export default function RatingView(props) {
  const classes = useStyles();
  const num = Number(props.value).toPrecision(2);
  if (props.value >= 9) {
    return (
      <Avatar variant={props.variant} className={classes.excellent}>
        {num}
      </Avatar>
    );
  }
  if (props.value <= 4) {
    return (
      <Avatar variant={props.variant} className={classes.bad}>
        {num}
      </Avatar>
    );
  }
  if (props.value > 4 && props.value < 7) {
    return (
      <Avatar variant={props.variant} className={classes.average}>
        {num}
      </Avatar>
    );
  }
  if (props.value >= 7 && props.value < 9) {
    return (
      <Avatar variant={props.variant} className={classes.good}>
        {num}
      </Avatar>
    );
  }
  return <Avatar>{num}</Avatar>;
}
