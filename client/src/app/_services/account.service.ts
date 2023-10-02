import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {map, ReplaySubject} from "rxjs";
import {User} from "../_models/user";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:44360/api/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'Account/login', model).pipe(
      map((x: User) => {
        const user = x;
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'Account/register', model).pipe(
      map((user: User) => {
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
