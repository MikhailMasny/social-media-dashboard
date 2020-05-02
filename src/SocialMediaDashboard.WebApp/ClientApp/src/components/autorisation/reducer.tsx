import { Action, Reducer } from 'redux';
import { LoginActionInterface } from './actions';
import { ON_LOGIN_SUCCESS, ON_LOGIN_ERROR, ON_LOGIN } from './actionTypes';

export interface AurorisationState {
    isLoggedIn: boolean;
}

export const reducer: Reducer<AurorisationState> = (state: AurorisationState | undefined, incomingAction: Action): AurorisationState => {
    if (state === undefined) {
        return { isLoggedIn: false };
    }

    const action = incomingAction as LoginActionInterface;
    switch (action.type) {
        case ON_LOGIN:
            return state;
        case ON_LOGIN_SUCCESS:
            state.isLoggedIn = true;
            return state;
        case ON_LOGIN_ERROR:
            state.isLoggedIn = false;
            return state;
        default:
            return state;
    }
};
