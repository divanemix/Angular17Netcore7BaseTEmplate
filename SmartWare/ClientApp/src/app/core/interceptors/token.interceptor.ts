import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError, catchError, map } from 'rxjs';
import { Router } from '@angular/router';
import { AuthenticationService, NotificationService } from '../services';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('token');

    // Clone and modify the request
    let modifiedRequest = request.clone({
      headers: request.headers
        .set('Accept', 'application/json; charset=utf-8')
        .set('Content-Type', 'application/json; charset=utf-8')
    });

    if (token) {
      modifiedRequest = modifiedRequest.clone({
        setHeaders: { 'Authorization': `Bearer ${token}` }
      });
    }

    return next.handle(modifiedRequest).pipe(
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          // Add any response handling logic here
        }
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        // Handle errors
        if (error.status === 401) {
          this.authenticationService.logout();
          this.notificationService.error("Session Scaduta");
          this.router.navigate(['login']);
        } else if (error.status === 503) {
          window.location.reload();
        }

        const errorMessage = error.error?.message || error.statusText;
        this.notificationService.error(errorMessage);
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}
