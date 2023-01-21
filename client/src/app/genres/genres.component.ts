import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styleUrls: ['./genres.component.css']
})
export class GenresComponent implements OnInit {

  baseUrl = environment.apiUrl;
  result : any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getGenres();
  }

  getGenres() {
    return this.http.get(this.baseUrl + 'Genre/GetGenres').subscribe(x => {
      this.result = x;
    });
  }
}
