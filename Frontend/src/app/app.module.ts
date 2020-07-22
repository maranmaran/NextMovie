import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { MaterialElevationDirective } from 'src/directives/elevation.directive';
import { environment } from 'src/environments/environment';
import { ErrorInterceptor } from 'src/interceptors/error.interceptor';
import { HttpInterceptor } from 'src/interceptors/http.interceptor';
import { CoreEffects, reducers } from 'src/ngrx/global-setup.ngrx';
import { MovieService } from './../services/movie.service';
import { MaterialModule } from './angular-material.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { MovieComponent } from './movies/movie-list/movie/movie.component';
import { UserMoviesComponent } from './movies/user-movies/user-movies.component';

@NgModule({
  declarations: [
    AppComponent,
    MaterialElevationDirective,
    MovieListComponent,
    MovieComponent,
    UserMoviesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    StoreModule.forRoot(reducers, {
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true,
      }
    }),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
    EffectsModule.forRoot(CoreEffects),
    VirtualScrollerModule
  ],
  providers: [
    MovieService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
