import ActionTypes from "./actionTypes";


export interface LoginActionInterface { type: ActionTypes, data: object | null }

export const actionCreators = {
    onLogin: (userName: string, password: string) => ({ type: ActionTypes.ON_LOGIN, data: { userName, password } } as LoginActionInterface),
    onLoginSuccess: () => ({ type: ActionTypes.ON_LOGIN_SUCCESS } as LoginActionInterface),
    onLoginError: () => ({ type: ActionTypes.ON_LOGIN_ERROR } as LoginActionInterface),

    onLogOut: () => ({ type: ActionTypes.ON_LOGOUT } as LoginActionInterface),
    onLogOutSuccess: () => ({ type: ActionTypes.ON_LOGOUT_SUCCESS } as LoginActionInterface),
    onLogOutError: () => ({ type: ActionTypes.ON_LOGOUT_ERROR } as LoginActionInterface),
};