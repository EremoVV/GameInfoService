import { Container, Grid, makeStyles, Typography } from "@material-ui/core";

const useStyles = makeStyles({
  text: {
    margin: "auto",
  },
});

export default function Welcoming() {
  const classes = useStyles();
  return (
    <Container>
      <Typography className={classes.text} variant="h2">
        Welcome to Game Info Service!
      </Typography>
    </Container>
  );
}
