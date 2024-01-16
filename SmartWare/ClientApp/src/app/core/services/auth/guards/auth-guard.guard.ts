import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateChildFn, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { map } from 'rxjs';
import { AuthenticationService } from '../authentication.service';

@Injectable({ providedIn: 'root' })
class AdminGuard {
  canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
    const login = "/auth/signin";
    const authService = inject(AuthenticationService);
    const router = inject(Router);
    const currentUser = authService.currentUserValue;
    if (currentUser) return true;
    router.navigate([login], { queryParams: { returnUrl: state.url } });
    return false
  }
}
export const IsAdminGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
): boolean => {

  return inject(AdminGuard).canActivate(route, state)
};
export const canActivateChild: CanActivateChildFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
  return inject(AdminGuard).canActivate(route, state)
};

