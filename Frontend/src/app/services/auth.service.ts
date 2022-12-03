import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const AUTH_API = 'https://localhost:7026/api/auth/';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(private http: HttpClient) { }

  login(UserName: string, Password: string): Observable<any> {
    return this.http.post(
      AUTH_API + 'login',
      {
        UserName,
        Password,
      },
      {responseType: 'text'}
    );
  }

  register(UserName: string, Password: string): Observable<any> {
    return this.http.post(
      AUTH_API + 'register',
      {
        UserName,
        Password,
      }
    );
  }
}