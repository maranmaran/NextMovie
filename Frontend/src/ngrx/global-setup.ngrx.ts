import { ActionReducerMap, MetaReducer } from '@ngrx/store/src/models';
import { environment } from 'src/environments/environment';
import { clearState } from './clear-state.reducer';
import { movieReducer } from './movies/movie.reducers';
import { MovieState } from './movies/movie.state';

export interface AppState {
    movie: MovieState
}

export const reducers: ActionReducerMap<AppState> = {
    movie: movieReducer
};


export const metaReducers: MetaReducer<AppState>[] =
    !environment.production ?
        [clearState] :
        [clearState]

export const CoreEffects = [];
