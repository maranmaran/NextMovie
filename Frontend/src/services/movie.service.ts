import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { Movie } from 'src/models/movie.model';
import { PagedResult } from 'src/models/page-result.model';
import { BaseService } from './base,service';

@Injectable()
export class MovieService extends BaseService {

    constructor(
        private httpDI: HttpClient,
    ) {
        super(httpDI, "Movies");
    }

    getTopRated(page: number = 1) {
        return this.http
            .get<PagedResult<Movie>>(this.url + 'GetTopRated/' + page)
            .pipe(catchError(this.handleError));
    }

    getPopular(page: number = 1) {
        return this.http
            .get<PagedResult<Movie>>(this.url + 'GetTopRated/' + page)
            .pipe(catchError(this.handleError));
    }

    getRecommendations() {
        return this.http
            .get<PagedResult<Movie>>(this.url + 'GetTopRated/')
            .pipe(catchError(this.handleError));
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
