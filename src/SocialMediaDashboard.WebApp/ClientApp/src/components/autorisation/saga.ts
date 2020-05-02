
import { put, call, takeLatest, takeEvery } from 'redux-saga/effects';
import ActionTypes from './actionTypes';
import { actionCreators } from './actions';

export const AuthSaga = [
    takeLatest(ActionTypes.ON_LOGIN, loginAsync),
]

function* loginAsync() {
    try {
        //TODO: Some work
        //yield call(() => {});;
        yield put(actionCreators.onLoginSuccess());
    } catch (error) {
        yield put(actionCreators.onLoginError());
    }
}

export default AuthSaga;