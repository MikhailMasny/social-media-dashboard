import { all } from "redux-saga/effects";
import AuthSaga from "../components/autorisation/saga";

/**
 * Application root saga.
 * Register each other saga`s here.
 */
export default function* rootSaga() {
	yield all([
		...AuthSaga,
	]);
}
