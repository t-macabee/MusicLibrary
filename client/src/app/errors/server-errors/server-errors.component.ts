import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-server-errors',
  templateUrl: './server-errors.component.html',
  styleUrls: ['./server-errors.component.css']
})
export class ServerErrorsComponent {
  error: any;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.error;
  }
}
