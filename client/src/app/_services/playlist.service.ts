import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAllPlaylists() {
    return this.http.get(this.baseUrl + 'Playlist');
  }

  getPlaylist(id: number) {
    return this.http.get(this.baseUrl + 'Playlist?userId=' + id);
  }

  getPlaylistByName(name: string) {
    return this.http.get(this.baseUrl + 'Playlist/playlistName?name=' + name);
  }
}
