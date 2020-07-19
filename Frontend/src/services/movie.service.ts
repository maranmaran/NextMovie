import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { request } from 'http';
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
            .post<PagedResult<Movie>>(this.url + 'GetTopRated/', request)
            .pipe(catchError(this.handleError));
    }

    getPopular(page: number = 1) {
        return this.http
            .post<ExportResponse>(this.url + 'GetTopRated/', request)
            .pipe(catchError(this.handleError));
    }

    addMovie(movieId: number, liked: boolean) {
        return this.http
            .post<PagedResult<Movie>>(this.url + 'GetTopRated/', request)
            .pipe(catchError(this.handleError));
    }

    getRecommendations() {
        return this.http
            .post<PagedResult<Movie>>(this.url + 'GetTopRated/', request)
            .pipe(catchError(this.handleError));
    }

}
