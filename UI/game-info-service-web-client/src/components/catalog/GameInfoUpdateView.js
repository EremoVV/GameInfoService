import { Button, Container, TextField, makeStyles } from "@material-ui/core";
import { useFormik } from "formik";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import * as yup from "yup";
import {
  gameInfoUpdateRequest,
  gameInfoGetRequest,
} from "../../api/catalog/catalogApi";
import { DatePicker } from "@material-ui/pickers";

const validationSchema = yup.object({
  gameRating: yup.number().max(10).min(0),
});

const useStyles = makeStyles({
  form: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  textInput: {
    marginBottom: "10px",
  },
  button: {
    marginBottom: "10px",
  },
});

function sendGameInfoUpdate(
  gameId,
  gameName,
  gameDescription,
  gameRating,
  gameReleaseDate
) {
  gameInfoUpdateRequest(
    gameId,
    gameName,
    gameDescription,
    gameRating,
    gameReleaseDate
  )
    .then((response) => {
      alert(response.data);
      postGameInfoUpdateRedirect();
    })
    .catch((error) => console.log(error));
}

function postGameInfoUpdateRedirect() {
  window.location.replace("/catalog");
}

export default function GameInfoUpdateView() {
  const { name } = useParams();
  const classes = useStyles();
  const formik = useFormik({
    initialValues: {
      gameId: 0,
      gameName: "",
      gameDescription: "",
      gameRating: 1,
      gameReleaseDate: new Date(),
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      sendGameInfoUpdate(
        values.gameId,
        values.gameName,
        values.gameDescription,
        values.gameRating,
        values.gameReleaseDate
      );
    },
  });

  useEffect(() => {
    gameInfoGetRequest(name)
      .then((response) => {
        const data = response.data;
        formik.setFieldValue("gameId", data.id);
        formik.setFieldValue("gameName", data.name);
        formik.setFieldValue("gameDescription", data.description);
        formik.setFieldValue("gameRating", data.rating);
        formik.setFieldValue("gameReleaseDate", data.releaseDate);
      })
      .catch((error) => console.log(error));
  }, []);
  return (
    <Container>
      <form className={classes.form} onSubmit={formik.handleSubmit}>
        <TextField
          id="gameName"
          label="Title:"
          value={formik.values.gameName}
          onChange={formik.handleChange}
          error={formik.touched.gameName && Boolean(formik.errors.gameName)}
        />
        <TextField
          multiline
          className={classes.textInput}
          id="gameDescription"
          label="Description:"
          value={formik.values.gameDescription}
          onChange={formik.handleChange}
          error={
            formik.touched.gameDescription &&
            Boolean(formik.errors.gameDescription)
          }
        />
        <TextField
          className={classes.textInput}
          id="gameRating"
          label="Rating:"
          type="number"
          value={formik.values.gameRating}
          onChange={formik.handleChange}
          error={formik.touched.gameRating && Boolean(formik.errors.gameRating)}
        />
        <DatePicker
          required
          id="gameReleaseDate"
          label="Release date"
          openTo="year"
          format="dd/MM/yyyy"
          views={["year", "month", "date"]}
          value={formik.values.gameReleaseDate}
          onChange={(data) => {
            formik.setFieldValue("gameReleaseDate", data);
          }}
        />
        <Button className={classes.button} color="primary" type="submit">
          Update game info
        </Button>
      </form>
    </Container>
  );
}
