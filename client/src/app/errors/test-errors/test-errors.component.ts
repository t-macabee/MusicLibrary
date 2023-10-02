import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent {
  baseUrl = "https://localhost:44360/api/";
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {
  }

  get404Error() {
    this.http.get(this.baseUrl + 'Buggy/not-found').subscribe(x => {
      console.log(x);
    }, error => {
      console.log(error);
      this.validationErrors = error;
    })
  }

  get400Error() {
    this.http.get(this.baseUrl + 'Buggy/bad-request').subscribe(x => {
      console.log(x);
    }, error => {
      console.log(error);
      this.validationErrors = error;
    })
  }

  get500Error() {
    this.http.get(this.baseUrl + 'Buggy/server-error').subscribe(x => {
      console.log(x);
    }, error => {
      console.log(error);
      this.validationErrors = error;
    })
  }

  get401Error() {
    this.http.get(this.baseUrl + 'Buggy/auth').subscribe(x => {
      console.log(x);
    }, error => {
      console.log(error);
      this.validationErrors = error;
    })
  }

  get400ValidationError() {
    this.http.get(this.baseUrl + 'Account/register', {}).subscribe(x => {
      console.log(x);
    }, error => {
      console.log(error);
      this.validationErrors = error;
    })
  }
}
