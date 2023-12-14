import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Playlist} from "../_models/playlist";

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getPlaylistsByUser(memberId: number) {
    return this.http.get<Playlist[]>(this.baseUrl + 'Playlist/' + memberId + '/playlists');
  }

  createPlaylist(memberId: number, playlist: Playlist) {
    return this.http.post(this.baseUrl + 'Playlist?userId=' + memberId, playlist);
  }

  updatePlaylist(playlistId: number, playlist: Playlist) {
    return this.http.put(this.baseUrl + 'Playlist/' + playlistId, playlist);
  }

  deletePlaylist(memberId: number, playlistId: number) {
    return this.http.delete(this.baseUrl + 'Playlist/' + memberId + '/playlists/' + playlistId);
  }

  addTrackToPlaylist(playlistId: number, trackId: number) {
    return this.http.post(this.baseUrl + 'Playlist/' + playlistId + '/tracks/' + trackId, {});
  }

  removeTrackFromPlaylist(playlistId: number, trackId: number) {
    return this.http.delete(this.baseUrl + 'Playlist/' + playlistId + '/tracks/' + trackId);
  }
}
