import { createAction, props } from "@ngrx/store";
import { Movie } from 'src/models/movie.model';
import { PagedResult } from 'src/models/page-result.model';

export const moviesFetched = createAction(
    '[MoviesAPI] Fetched',
    props<{ entity: PagedResult<Movie> }>()
);

export const resetPaging = createAction(
    '[MoviesAPI] Reset paging',
)
