import { Action, Reducer } from 'redux';
import { LoginActionInterface } from './actions';
import ActionTypes from './actionTypes';

export interface AurorisationState {
    isLoggedIn: boolean;
}

export const reducer: Reducer<AurorisationState> = (state: AurorisationState | undefined, incomingAction: Action): AurorisationState => {
    if (state === undefined) {
        return { isLoggedIn: false };
    }
    //TODO: Remove redundant cases
    const action = incomingAction as LoginActionInterface;
    switch (action.type) {
        case ActionTypes.ON_LOGIN:
            return state;
        case ActionTypes.ON_LOGIN_SUCCESS:
            state.isLoggedIn = true;
            return state;
        case ActionTypes.ON_LOGIN_ERROR:
            state.isLoggedIn = false;
            return state;

        case ActionTypes.ON_LOGOUT:
            return state;
        case ActionTypes.ON_LOGOUT_SUCCESS:
            state.isLoggedIn = false;
            return state;
        case ActionTypes.ON_LOGOUT_ERROR:
            return state;
        default:
            return state;
    }
};
