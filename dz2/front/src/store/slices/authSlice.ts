import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { AuthState, JwtPayload, User } from "./types";
import { jwtDecode } from "jwt-decode";

const initState: AuthState = {
    isAuth: false,
    user: null
};

const authSlice = createSlice({
    name: "auth",
    initialState: initState,
    reducers: {
        login(state, action: PayloadAction<string>) {
            const token = action.payload;
            localStorage.setItem("token", token);
            const payload = jwtDecode<JwtPayload>(token);
            const user: User = {
                id: payload.id,
                userName: payload.userName,
                email: payload.email,
                fisrtName: payload.fisrtName,
                lastName: payload.lastName,
                avatar: payload.avatar,
                roles: payload.roles,
            };

            state.isAuth = true;
            state.user = user;
        },
        logout(state) {
            localStorage.removeItem("token");
            state.isAuth = false;
            state.user = null;
        }
    },
});

export const { login, logout } = authSlice.actions;
export default authSlice.reducer;
