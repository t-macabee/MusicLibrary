import { Injectable } from '@angular/core';
import {Artist} from "../_models/artist";
import {Photo} from "../_models/photo";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ArtistService {
  private baseUrl = environment.apiUrl;
  artist: Artist[] = [];

  constructor(private http: HttpClient) {}

  getArtists() {
    return this.http.get<Artist[]>(this.baseUrl + 'Artist');
  }

  getArtist(id: number) {
    return this.http.get<Artist>(this.baseUrl + "Artist/id?id=" + id);
  }

  createTemplateArtist() {
    let sending = {
      artistName: "Artist name template",
      artistDescription: "Artist description template"
    };
    return this.http.post(this.baseUrl + 'Artist', sending);
  }

  deleteArtist(id: number) {
    return this.http.delete(this.baseUrl + 'Artist/' + id);
  }

  updateArtist(id: number, artist: Artist) {
    return this.http.put(this.baseUrl + "Artist/" + id, artist);
  }

}
