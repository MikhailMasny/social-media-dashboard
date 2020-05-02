import ActionTypes from "./actionTypes";


export interface LoginActionInterface { type: ActionTypes, data: object | null }

export const actionCreators = {
    onLogin: (userName: string, password: string) => ({ type: ActionTypes.ON_LOGIN, data: { userName, password } } as LoginActionInterface),
    onLoginSuccess: () => ({ type: ActionTypes.ON_LOGIN_SUCCESS } as LoginActionInterface),
    onLoginError: () => ({ type: ActionTypes.ON_LOGIN_ERROR } as LoginActionInterface),
};