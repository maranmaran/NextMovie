import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { IPageInfo } from 'ngx-virtual-scroller';
import { Observable } from 'rxjs';
import { take, tap } from 'rxjs/operators';
import { Movie } from 'src/models/movie.model';
import { AppState } from 'src/ngrx/global-setup.ngrx';
import { SubSink } from 'subsink';
import { MovieService } from '../../../services/movie.service';
import { movies } from './../../../ngrx/movies/movie.selectors';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.scss']
})
export class MovieListComponent implements OnInit, OnDestroy {

  movies$: Observable<Movie[]>;
  movieCount: number;

  // all user movies and whether they liked them or not
  userMovies = new Map<number, boolean>();
  private _subs = new SubSink();

  constructor(
    private movieService: MovieService,
    private store: Store<AppState>
  ) { }

  ngOnInit(): void {

    this.getUserMovies();
    this.getMovies();

    this.movies$ = this.store.select(movies).pipe(tap(movies => this.movieCount = movies.length));

    this._subs.add(
      // this.onScroll(),
    )
  }

  ngOnDestroy() {
    this._subs.unsubscribe();
  }

  search(query: string) {
    throw Error("Not implemented");
  }

  getMovies() {
    this.movieService.getMovies()
      .pipe(take(1))
      .subscribe();
  }

  getUserMovies() {
    this.movieService.getUserMovies()
      .pipe(take(1))
      .subscribe(
        (userMovies: { id: number, liked: boolean }[]) => {
          const keyValue = userMovies.map(x => [x.id, x.liked] as [number, boolean]);
          this.userMovies = new Map<number, boolean>(keyValue)
        },
        err => console.log(err)
      )
  }

  timeoutFn: any;
  prevent = false;
  onLikeMovie(movie: Movie) {

    this.timeoutFn = setTimeout(_ => {

      if (this.prevent) {
        return this.prevent = false;
      }

      if (this.userMovies.has(movie.id) && this.userMovies.get(movie.id) == true) {
        this.deleteMovie(movie.id);
      }
      else if (this.userMovies.has(movie.id) && this.userMovies.get(movie.id) == false) {
        this.updateMovie(movie.id);
      } else {
        this.addMovie(movie.id, true);
      }

    }, 300);
  }

  onDislikeMovie(movie: Movie) {
    this.prevent = true;
    clearTimeout(this.timeoutFn);

    if (this.userMovies.has(movie.id) && this.userMovies.get(movie.id) == false) {
      this.deleteMovie(movie.id);
    }
    else if (this.userMovies.has(movie.id) && this.userMovies.get(movie.id) == true) {
      this.updateMovie(movie.id);
    } else {
      this.addMovie(movie.id, false);
    }
  }

  addMovie(id, liked) {
    this.movieService.addMovie(id, liked)
      .pipe(take(1))
      .subscribe(
        _ => this.userMovies.set(id, liked),
        err => console.log(err)
      );
  }

  updateMovie(id) {
    this.movieService.updateMovie(id, !this.userMovies.get(id))
      .pipe(take(1))
      .subscribe(
        _ => this.userMovies.set(id, !this.userMovies.get(id)),
        err => console.log(err)
      );
  }

  deleteMovie(id) {
    this.movieService.deleteMovie(id)
      .pipe(take(1))
      .subscribe(
        _ => this.userMovies.delete(id),
        err => console.log(err)
      );
  }

  trackByFn(movie: Movie) {
    return movie.id;
  }

  fetchMore(event: IPageInfo) {

    if (!this.movieCount || this.movieCount == 0)
      return;

    if (event.endIndex !== this.movieCount - 1)
      return;

    this.getMovies();
  }

}
