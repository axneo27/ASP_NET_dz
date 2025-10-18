export interface User {
    id: string;
    userName: string;
    email: string;
    fisrtName: string;
    lastName: string;
    avatar: string;
    roles: string[];
}

export interface JwtData {
    exp: number;
    iss: string;
    aud: string;
}

export type JwtPayload = User & JwtData;

export interface AuthState {
    isAuth: boolean;
    user: User | null;
}