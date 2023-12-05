import { Injectable } from '@angular/core';
import {Artist} from "../_models/artist";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {Genre} from "../_models/genre";

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

  getArtist(id: number): Observable<Artist>  {
    return this.http.get<Artist>(this.baseUrl + "Artist/" + id);
  }

  getGenres() {
    return this.http.get<Genre[]>(this.baseUrl + "Genre");
  }

  createTemplateArtist(artist: Artist) {
    return this.http.post(this.baseUrl + 'Artist', artist);
  }

  deleteArtist(id: number) {
    return this.http.delete(this.baseUrl + 'Artist/' + id);
  }

  updateArtist(id: number, artist: Artist) {
    return this.http.put(this.baseUrl + "Artist/" + id, artist);
  }

  setMainPhoto(artistId: number, photoId: number) {
    return this.http.put(this.baseUrl + 'Artist/' + artistId + '/photos/set-main/' + photoId, {});
  }

  deletePhoto(artistId: number, photoId: number) {
    return this.http.delete(this.baseUrl + 'Artist/' + artistId + '/photos/' + photoId, {});
  }
}
