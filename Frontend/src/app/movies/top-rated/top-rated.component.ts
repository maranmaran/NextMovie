import { Component, OnInit } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { take } from 'rxjs/operators';
import { environment } from './../../../environments/environment';
import { Movie } from './../../../models/movie.model';
import { PagedResult } from './../../../models/page-result.model';
import { MovieService } from './../../../services/movie.service';

@Component({
  selector: 'app-top-rated',
  templateUrl: './top-rated.component.html',
  styleUrls: ['./top-rated.component.scss']
})
export class TopRatedComponent implements OnInit {

  imageUrl = environment.tmdbImagesUrl;

  movies: Movie[];
  page: number;
  totalPages: number;

  // all user movies and whether they liked them or not
  userMovies = new Map<number, boolean>();


  constructor(
    private movieService: MovieService,
    public media: MediaObserver
  ) { }

  ngOnInit(): void {
    this.getUserMovies();
    this.get();
  }

  search(query: string) {
    throw Error("Not implemented");
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

  get(page = 1) {
    if (page == this.totalPages) {
      throw Error("No more pages");
    }

    this.movieService.getTopRated(page)
      .pipe(take(1))
      .subscribe(
        (movies: PagedResult<Movie>) => {
          this.page = movies.page;
          this.totalPages = movies.totalPages;
          this.movies = movies.results;
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

}
