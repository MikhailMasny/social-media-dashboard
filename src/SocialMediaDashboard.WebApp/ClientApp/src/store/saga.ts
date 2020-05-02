import { all } from 'redux-saga/effects';
import AuthSaga from '../components/autorisation/saga'

export default function* rootSaga() {
    yield all([
        ...AuthSaga,
    ]);
}
