import { Route, Routes } from "react-router";
import "./App.css";
import DefaultLayout from "./components/layouts/DefaultLayout";
import MainPage from "./pages/mainPage/MainPage";
import LoginPage from "./pages/auth/LoginPage";
import RegisterPage from "./pages/auth/RegisterPage";
import GenrePage from "./pages/genre/GenrePage";
import TrackCreatePage from "./pages/track/TrackCreatePage";
import { login } from "./store/slices/authSlice";
import { useDispatch } from "react-redux";
import { useEffect } from "react";

function App() {
    const dispatch = useDispatch();

    useEffect(() => {
        const token = localStorage.getItem("token");
        if(token) {
            dispatch(login(token));
        }
    }, [dispatch])

    return (
        <Routes>
            <Route path="/" element={<DefaultLayout />}>
                <Route index element={<MainPage />} />
                <Route path="login" element={<LoginPage />} />
                <Route path="register" element={<RegisterPage />} />
                <Route path="genres" element={<GenrePage />} />
                <Route path="track/create" element={<TrackCreatePage />} />
            </Route>
        </Routes>
    );
}

export default App;
