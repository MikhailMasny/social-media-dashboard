import { applyMiddleware, combineReducers, createStore } from "redux";
import createSagaMiddleware from "redux-saga";
import { composeWithDevTools } from "redux-devtools-extension";
import { connectRouter, routerMiddleware } from "connected-react-router";
import { History } from "history";
import { ApplicationState, reducers } from "./";
import saga from "./saga";

export default function configureStore(history: History, initialState?: ApplicationState) {
	const sagaMiddleware = createSagaMiddleware();
	const middleware = [
		sagaMiddleware,
		routerMiddleware(history)
	];

	const rootReducer = combineReducers({
		...reducers,
		router: connectRouter(history)
	});

	const enhancers = [];
	const windowIfDefined = typeof window === "undefined" ? null : window as any;
	if (windowIfDefined && windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__) {
		enhancers.push(windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__());
	}

	const store = createStore(
		rootReducer,
		initialState,
		composeWithDevTools(applyMiddleware(...middleware), ...enhancers)
	);

	sagaMiddleware.run(saga);

	return store;
}
