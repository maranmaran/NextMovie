import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PopularComponent } from './movies/popular/popular.component';
import { RecommendedComponent } from './movies/recommended/recommended.component';
import { TopRatedComponent } from './movies/top-rated/top-rated.component';


const routes: Routes = [
  { path: 'top-rated', component: TopRatedComponent },
  { path: 'popular', component: PopularComponent },
  { path: 'recommended', component: RecommendedComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
