import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { UserMoviesComponent } from './movies/user-movies/user-movies.component';


const routes: Routes = [
  { path: 'movie-list', component: MovieListComponent },
  { path: 'user-movies', component: UserMoviesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
