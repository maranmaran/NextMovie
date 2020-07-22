import { createReducer, on } from '@ngrx/store';
import { Action, ActionReducer } from '@ngrx/store/src/models';
import { Movie } from 'src/models/movie.model';
import { PagedResult } from 'src/models/page-result.model';
import * as MovieActions from './movie.actions';
import { adapterMovie, MovieInitialState, MovieState } from './movie.state';

export const movieReducer: ActionReducer<MovieState, Action> = createReducer(
    MovieInitialState,

    // FETCH
    on(MovieActions.moviesFetched, (state: MovieState, payload: { entity: PagedResult<Movie> }) => {
        state = adapterMovie.addMany(payload.entity.results, state);
        return {
            ...state,
            page: payload.entity.page,
            totalPages: payload.entity.totalPages,
            totalResults: payload.entity.totalResults,
        };
    }),

    // RESET PAGING
    on(MovieActions.resetPaging, (state: MovieState) => {
        state = adapterMovie.removeAll(state);
        return {
            ...state,
            page: undefined,
            totalPages: undefined,
            totalResults: undefined
        };
    }),

);

// get the selectors
export const {
    selectIds,
    selectEntities,
    selectAll,
    selectTotal,
} = adapterMovie.getSelectors();
