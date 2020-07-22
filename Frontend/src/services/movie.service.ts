import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { catchError, filter, map, switchMap, take } from 'rxjs/operators';
import { Movie } from 'src/models/movie.model';
import { PagedResult } from 'src/models/page-result.model';
import { AppState } from 'src/ngrx/global-setup.ngrx';
import { moviesFetched, resetPaging } from './../ngrx/movies/movie.actions';
import { pagingInfo } from './../ngrx/movies/movie.selectors';
import { BaseService } from './base,service';

@Injectable()
export class MovieService extends BaseService {

    private _getMoviesEndpoint = 'GetTopRated/';

    constructor(
        private httpDI: HttpClient,
        private store: Store<AppState>
    ) {
        super(httpDI, "Movies");
    }

    setMoviesFetchEndpoint(type: 'top-rated' | 'popular' | 'random' | 'recommended') {
        switch (type) {
            case 'top-rated':
                this._getMoviesEndpoint = 'GetTopRated/';
                break;
            case 'popular':
                this._getMoviesEndpoint = 'GetPopular/';
                break;
            case 'random':
                this._getMoviesEndpoint = 'GetRandom/';
                break;
            case 'recommended':
                this._getMoviesEndpoint = 'GetRecommendations/';
                break;
        }

        this.store.dispatch(resetPaging());
    }

    getMovies() {
        return this.store.select(pagingInfo)
            .pipe(
                take(1),
                map(info => {
                    if (info.page == undefined) {
                        info.page = 0;
                        info.totalPages = 1;
                    }

                    info.page++;

                    return info;
                }),
                filter(info => info.page <= info.totalPages),
                switchMap(info => this.http.get<PagedResult<Movie>>(this.url + this._getMoviesEndpoint + info.page).pipe(take(1))),
                catchError(this.handleError),
                map(result => {
                    if (!(result instanceof Error)) {
                        this.store.dispatch(moviesFetched({ entity: result }))
                    }
                }),
                // retryWhen(errors => {
                //     return errors
                //         .pipe(
                //             delayWhen(() => timer(2000)),
                //             tap(() => console.log('retrying...'))
                //         );
                // })
            )
    }

    getUserMovies() {
        return this.http
            .get(this.url + 'GetAllUserMovies/')
            .pipe(catchError(this.handleError));
    }

    addMovie(id: number, liked: boolean) {
        const req = { id, liked };

        return this.http
            .post(this.url + 'AddMovie/', req)
            .pipe(catchError(this.handleError));
    }

    updateMovie(id: number, liked: boolean) {
        const req = { id, liked };

        return this.http
            .put(this.url + 'UpdateMovie/', req)
            .pipe(catchError(this.handleError));
    }

    deleteMovie(id: number) {
        return this.http
            .delete(this.url + 'DeleteMovie/' + id)
            .pipe(catchError(this.handleError));
    }

}
