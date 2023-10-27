import {Component, OnInit} from '@angular/core';
import {Message} from "../_models/message";
import {Pagination} from "../_models/pagination";
import {UserParams} from "../_models/userParams";
import {MessageService} from "../_services/message.service";
import {AccountService} from "../_services/account.service";
import {User} from "../_models/user";
import {take} from "rxjs";
import {Router} from "@angular/router";

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit{
  messages?: Message[] = [];
  pagination?: Pagination;
  containter = 'Unread';
  userParams: UserParams;
  user: User;
  loading = false;

  constructor(private messageService: MessageService, private accountService: AccountService, private router: Router) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
    })
  }

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages() {
    this.loading = true;
    this.messageService.getMessages(this.userParams, this.containter).subscribe({next: response => {
        this.messages = response.result;
        this.pagination = response.pagination;
        this.loading = false;
      }
   })
  }

  deleteMessage(id: number) {
    this.messageService.deleteMessage(id).subscribe({
      next: () => this.messages?.splice(this.messages.findIndex(m => m.id === id), 1)
    })
  }

  pageChanged(event: any) {
    if(this.userParams.pageNumber !== event.Page) {
      this.userParams.pageNumber = event.Page;
      this.loadMessages();
    }
  }
}
