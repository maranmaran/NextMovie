import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromMovie from './movie.reducers';
import { MovieState } from './movie.state';

export const selectMovieState = createFeatureSelector<MovieState>("movie");

export const movieIds = createSelector(
    selectMovieState,
    fromMovie.selectIds
);

export const movieEntities = createSelector(
    selectMovieState,
    fromMovie.selectEntities
);

export const movies = createSelector(
    selectMovieState,
    fromMovie.selectAll,
);

export const moviesCount = createSelector(
    selectMovieState,
    fromMovie.selectTotal
);

export const pagingInfo = createSelector(
    selectMovieState,
    state => ({ page: state.page, totalPages: state.totalPages })
)
