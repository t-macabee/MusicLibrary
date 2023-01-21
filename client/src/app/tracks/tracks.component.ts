import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Track} from "../_modules/track";

@Component({
  selector: 'app-tracks',
  templateUrl: './tracks.component.html',
  styleUrls: ['./tracks.component.css']
})
export class TracksComponent implements OnInit {

  baseUrl = environment.apiUrl;
  result: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getTracks();
  }

  getTracks() {
    return this.http.get(this.baseUrl + "Track/GetTracks").subscribe(x => {
      this.result = x;
    });
  }

}
