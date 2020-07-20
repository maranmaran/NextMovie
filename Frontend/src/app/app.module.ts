import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialElevationDirective } from 'src/directives/elevation.directive';
import { ErrorInterceptor } from 'src/interceptors/error.interceptor';
import { HttpInterceptor } from 'src/interceptors/http.interceptor';
import { MovieService } from './../services/movie.service';
import { MaterialModule } from './angular-material.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PopularComponent } from './movies/popular/popular.component';
import { RecommendedComponent } from './movies/recommended/recommended.component';
import { TopRatedComponent } from './movies/top-rated/top-rated.component';


@NgModule({
  declarations: [
    AppComponent,
    TopRatedComponent,
    PopularComponent,
    RecommendedComponent,
    MaterialElevationDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule
  ],
  providers: [
    MovieService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
