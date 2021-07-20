import { Avatar } from "@material-ui/core";
import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles({
  rating: {
    width: "100px",
    height: "100px",
  },
  red: {
    backgroundColor: "#c23616",
  },
  yellow: {
    backgroundColor: "#e1b12c",
  },
  green: {
    backgroundColor: "#44bd32",
  },
});

export default function RatingView(props) {
  const classes = useStyles();
  const num = Number(props.value).toPrecision(2);
  if (props.value <= 3) {
    return (
      <Avatar variant={props.variant} className={classes.red}>
        {num}
      </Avatar>
    );
  }
  if (props.value > 3 && props.value < 8) {
    return (
      <Avatar variant={props.variant} className={classes.yellow}>
        {num}
      </Avatar>
    );
  }
  if (props.value >= 8) {
    return (
      <Avatar variant={props.variant} className={classes.green}>
        {num}
      </Avatar>
    );
  }
  return <Avatar>{num}</Avatar>;
}
