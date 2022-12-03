import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

const USER_KEY = 'user-key';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor(private router: Router) { }

  public saveToken(token: string): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, token);
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(USER_KEY);
  }

  public isLoggedIn(): boolean {
    const token = window.sessionStorage.getItem(USER_KEY);
    if (token) {
      return true;
    }
    return false;
  }

  public logOut(): void {
    window.sessionStorage.clear();
    this.router.navigate(['/login']);
}
}
