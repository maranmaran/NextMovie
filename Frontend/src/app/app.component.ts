import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatButtonToggleChange, MatButtonToggleGroup } from '@angular/material/button-toggle';
import { NavigationEnd, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { AppState } from 'src/ngrx/global-setup.ngrx';
import { SubSink } from 'subsink';
import { MovieService } from './../services/movie.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  constructor(
    public router: Router,
    private movieService: MovieService,
    private store: Store<AppState>
  ) { }

  activeRoute: string;
  @ViewChild(MatButtonToggleGroup) _toggleGroup: MatButtonToggleGroup;

  private _subs = new SubSink();

  ngOnInit() {

    this._subs.add(
      this.onRouteChange(),
    )

    setTimeout(_ => {
      this._subs.add(
        this.onToggleChange()
      );
    })
  }

  onToggleChange() {
    return this._toggleGroup.change
      .pipe(
        tap((change: MatButtonToggleChange) => this.movieService.setMoviesFetchEndpoint(change.value)),
        switchMap(_ => this.movieService.getMovies())
      )
      .subscribe()
  }

  onRouteChange() {
    return this.router.events
      .pipe(
        filter(e => e instanceof NavigationEnd),
        map(e => (e as NavigationEnd).url)
      )
      .subscribe(url => {
        this.activeRoute = url
      });
  }

  ngOnDestroy(): void {
    this._subs.unsubscribe();
  }
}
