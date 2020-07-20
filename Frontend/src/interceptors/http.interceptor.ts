import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class HttpInterceptor implements HttpInterceptor {

    constructor(
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        req = req.clone({
            url: environment.apiUrl + req.url,
            withCredentials: true,
            // headers: this.appSettingsService.apiHeaders
        });


        return next.handle(req)
            .pipe(
            );
    }
}
