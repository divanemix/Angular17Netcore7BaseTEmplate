import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, BehaviorSubject, Subject } from 'rxjs';
import { catchError, tap, map, takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { User } from '../../models';




@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private _destroyed$ = new Subject();
  apiUrl = "";
  private currentUserSubject: BehaviorSubject<User>;
  private currentUser: Observable<User>;

  private currentChangePswSubject: BehaviorSubject<boolean>;
  public currentChangePsw: Observable<boolean>;

  private userRolesPswSubject: BehaviorSubject<any[]>;
  public userRoles: Observable<any[]>;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();

    this.currentChangePswSubject = new BehaviorSubject<boolean>(JSON.parse(localStorage.getItem('passwordValidity')));
    this.currentChangePsw = this.currentChangePswSubject.asObservable();
    this.apiUrl = ` ${baseUrl}api/auth/`;

  }
  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }
  public get passwordValidityValue(): boolean {
    return this.currentChangePswSubject.value;
  }

  public get userRolesValue(): any[] {
    if (this.currentUserSubject.value)
      return this.currentUserSubject.value.activeRoles;
    return [];
  }

  isActiveRole(role): boolean {
    let roles = [];
    if (this.currentUserSubject.value)
      roles = this.currentUserSubject.value.activeRoles;

    if (!role) return false;
    var isActive = roles.some(r => r.toUpperCase() === role.toUpperCase());
    return isActive;
  }

  login(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'login', data)
      .pipe(map(user => {
        let pswValid = true;
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
         
          localStorage.setItem('passwordValidity', JSON.stringify(pswValid));
          this.currentUserSubject.next(user);
          this.currentChangePswSubject.next(pswValid);

          return user;
        }
      }),
        tap(_ => this.log('login')),
        takeUntil(this._destroyed$),
        catchError(this.handleError('login', []))
      );
  }
  changePassword(data: any): Observable<any> {

    return this.http.post<any>(this.apiUrl + 'change-password', data)
      .pipe(map(user => {

        localStorage.setItem('passwordValidity', JSON.stringify(true));
        this.currentChangePswSubject.next(true);
      })
      );
  }
  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.currentChangePswSubject.next(null);

  }

  register(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'register', data)
      .pipe(
        tap(_ => this.log('login')),
        catchError(this.handleError('login', [])),
        takeUntil(this._destroyed$)
      );
  }
  

  //#region


  //endregion
  //************************* */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    console.log(message);
  }
  ngOnDestroy() {
    this._destroyed$.next(0);
    this._destroyed$.complete();
  };
}
