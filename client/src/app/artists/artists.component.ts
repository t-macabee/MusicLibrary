import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Artist} from "../_modules/artist";

@Component({
  selector: 'app-artists',
  templateUrl: './artists.component.html',
  styleUrls: ['./artists.component.css']
})
export class ArtistsComponent implements OnInit {

  baseUrl = environment.apiUrl;
  artistName: string = "";
  result: any;
  selectedArtist: any;
  newArtist: Artist = null;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.webApiInitiation();
  }

  webApiInitiation() {
    return this.http.get(this.baseUrl + 'Artist/GetArtists').subscribe(x => {
      this.result = x;
    });
  }

  getArtists() {
    if(this.result == null)
      return [];
    return this.result.filter((x: any) => x.artistName.length == 0 ||  (x.artistName).toLowerCase().startsWith(this.artistName.toLowerCase()));
  }

  details(artist: any) {
    this.selectedArtist = artist;
  }
  /*
    Artist/DeleteArtist/id?id=
  */

  delete(artist: any) {
    this.http.delete(this.baseUrl + "Artist/DeleteArtist/id?id=" + artist.id).subscribe( response => this.webApiInitiation());
  }

  addNew() {
    this.newArtist = {
      id:0,
      artistName: "",
      gender: "",
      about: "",
      dateOfBirth: new Date()
    }
  }

  saveChangesNewArtist() {
    this.http.post(this.baseUrl + "Artist/AddArtist", this.newArtist).subscribe((returnValue : any) => {
      this.newArtist = null;
      this.webApiInitiation();
    });
  }
}
