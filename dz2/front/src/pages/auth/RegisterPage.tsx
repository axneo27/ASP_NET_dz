import { Box, Button, FormControl, FormLabel, TextField, Typography } from "@mui/material";
import { useFormik } from "formik";
import * as yup from "yup";
import axios from "axios";
import { apiUrl } from "../../env";
import { useDispatch } from "react-redux";
import { login } from "../../store/slices/authSlice";
import { useNavigate } from "react-router";

interface RegisterForm {
  email: string;
  userName: string;
  password: string;
  firstName?: string;
  lastName?: string;
}

const schema = yup.object({
  email: yup.string().email().required(),
  userName: yup.string().required(),
  password: yup.string().min(6).required(),
  firstName: yup.string().optional(),
  lastName: yup.string().optional(),
});

const RegisterPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const formik = useFormik<RegisterForm>({
    initialValues: { email: "", userName: "", password: "" },
    validationSchema: schema,
    onSubmit: async (values: RegisterForm) => {
      try {
        const { data } = await axios.post(`${apiUrl}/auth/register`, values);
        const token: string = data.payload;
        if (token) {
          dispatch(login(token));
          navigate("/", { replace: true });
        }
      } catch (e) {
        console.error(e);
      }
    },
  });

  return (
    <Box maxWidth={480} mx="auto" my={4} component="form" onSubmit={formik.handleSubmit}>
      <Typography variant="h4" mb={2}>Sign up</Typography>
      <FormControl fullWidth margin="dense">
        <FormLabel>Email</FormLabel>
        <TextField name="email" value={formik.values.email} onChange={formik.handleChange} />
      </FormControl>
      <FormControl fullWidth margin="dense">
        <FormLabel>Username</FormLabel>
        <TextField name="userName" value={formik.values.userName} onChange={formik.handleChange} />
      </FormControl>
      <FormControl fullWidth margin="dense">
        <FormLabel>Password</FormLabel>
        <TextField type="password" name="password" value={formik.values.password} onChange={formik.handleChange} />
      </FormControl>
      <FormControl fullWidth margin="dense">
        <FormLabel>First name</FormLabel>
        <TextField name="firstName" value={formik.values.firstName ?? ""} onChange={formik.handleChange} />
      </FormControl>
      <FormControl fullWidth margin="dense">
        <FormLabel>Last name</FormLabel>
        <TextField name="lastName" value={formik.values.lastName ?? ""} onChange={formik.handleChange} />
      </FormControl>
      <Button type="submit" variant="contained" sx={{ mt: 2 }}>Create account</Button>
    </Box>
  );
};

export default RegisterPage;
