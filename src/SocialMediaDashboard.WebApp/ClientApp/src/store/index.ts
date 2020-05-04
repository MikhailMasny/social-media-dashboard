import * as Autorisation from "../components/autorisation/reducer";

export interface ApplicationState {
	autorisation: Autorisation.AurorisationState | undefined;
}

export const reducers = {
	autorisation: Autorisation.reducer,
};

export interface AppThunkAction<TAction> {
	(dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
