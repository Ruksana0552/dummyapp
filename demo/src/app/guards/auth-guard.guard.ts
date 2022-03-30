import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardGuard implements CanActivate {
  constructor(private router: Router) {

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const token = localStorage.getItem("jwt");
    if (token )

      return true;
     // localStorage.clear
    if (!token ) {
      alert('Please login first')
      this.router.navigate(['/login'])
      localStorage.removeItem("jwt")
      return false;

    }
    return true;
  }
}


