import { ON_LOGIN, ON_LOGIN_SUCCESS, ON_LOGIN_ERROR } from "./actionTypes"

export interface LoginActionInterface { type: string, data: object }

export const actionCreators = {
    onLogin: (userName: string, password: string) => ({ type: ON_LOGIN, data: { userName, password } } as LoginActionInterface),
    onLoginSuccess: () => ({ type: ON_LOGIN_SUCCESS } as LoginActionInterface),
    onLoginError: () => ({ type: ON_LOGIN_ERROR } as LoginActionInterface),
};