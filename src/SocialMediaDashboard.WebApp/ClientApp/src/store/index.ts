import * as Counter from './Counter';
import * as Autorisation from '../components/autorisation/reducer';

export interface ApplicationState {
    counter: Counter.CounterState | undefined;
    autorisation: Autorisation.AurorisationState | undefined;
}

export const reducers = {
    counter: Counter.reducer,
    autorisation: Autorisation.reducer,
};

export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
