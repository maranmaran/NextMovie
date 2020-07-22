import { createEntityAdapter, EntityState } from '@ngrx/entity';
import { Movie } from 'src/models/movie.model';

export interface MovieState extends EntityState<Movie> {
    page: number;
    totalPages: number;
    totalResults: number;
}

export const adapterMovie = createEntityAdapter<Movie>();

export const MovieInitialState: MovieState = adapterMovie.getInitialState({
    page: undefined,
    totalPages: undefined,
    totalResults: undefined,
})
