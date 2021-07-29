import { Button, Container, TextField, makeStyles } from "@material-ui/core";
import { useFormik } from "formik";
import * as yup from "yup";
import { gameInfoCreateRequest } from "../../api/catalog/catalogApi";
import { DatePicker } from "@material-ui/pickers";

const validationSchema = yup.object({
  gameName: yup.string("Enter title").required("Title required"),
  gameDescription: yup
    .string("Enter description")
    .required("Description required"),
  //gameRating: yup.number().max(10).min(0),
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

function sendGameInfoCreate(
  gameName,
  gameDescription,
  gameRating,
  gameReleaseDate
) {
  gameInfoCreateRequest(gameName, gameDescription, gameRating, gameReleaseDate)
    .then((response) => {
      alert(response.data);
      postGameInfoCreateRedirect();
    })
    .catch((error) => console.log(error));
}

function postGameInfoCreateRedirect() {
  window.location.replace("/catalog");
}

export default function GameInfoCreateView() {
  const classes = useStyles();
  const formik = useFormik({
    initialValues: {
      gameName: "",
      gameDescription: "",
      gameRating: [],
      gameReleaseDate: new Date(),
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      sendGameInfoCreate(
        values.gameName,
        values.gameDescription,
        values.gameRating,
        values.gameReleaseDate
      );
    },
  });
  return (
    <Container>
      <form className={classes.form} onSubmit={formik.handleSubmit}>
        <TextField
          required
          id="gameName"
          label="Title:"
          value={formik.values.gameName}
          onChange={formik.handleChange}
          error={formik.touched.gameName && Boolean(formik.errors.gameName)}
        />
        <TextField
          className={classes.textInput}
          required
          multiline
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
          required
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
          Add game title
        </Button>
      </form>
    </Container>
  );
}
