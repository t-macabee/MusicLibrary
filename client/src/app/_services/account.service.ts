import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {map, ReplaySubject} from "rxjs";
import {User} from "../_models/user";
import {environment} from "../../environments/environment";
import {PresenceService} from "./presence.service";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private presence: PresenceService) { }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    this.presence.createHubConnection(user);
  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'Account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if(user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'Account/register', model).pipe(
      map((user: User) => {
        if(user) {
          this.setCurrentUser(user);
          this.presence.createHubConnection(user);
        }
        return user;
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.presence.stopHubConnection();
  }
}
