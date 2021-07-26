import { Container, makeStyles, Typography } from "@material-ui/core";

const useStyles = makeStyles({
  container: {},
  text: {
    margin: "auto",
  },
});

export default function () {
  const classes = useStyles();
  return (
    <Container classNmae={classes.container}>
      <Typography className={classes.text}>Element not found!</Typography>
    </Container>
  );
}
