import {Component, OnInit} from '@angular/core';
import {AccountService} from "../_services/account.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {Observable} from "rxjs";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  user$: Observable<any>;

  constructor(public accountService: AccountService, private router: Router) {}

  ngOnInit() {
    this.user$ = this.accountService.currentUser$;
  }

  getProfilePicture(user: any) {
    return user?.photoUrl || './assets/user.png';
  }

  login() {
   this.accountService.login(this.model).subscribe({
     next: _ => {
       this.router.navigateByUrl('/members');
       this.model = {};
     }
   })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
