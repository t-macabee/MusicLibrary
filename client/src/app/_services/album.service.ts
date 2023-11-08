import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Album} from "../_models/album";

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllAlbums() {
    return this.http.get(this.baseUrl + 'Album');
  }

  getAlbumsByArtist(id: number) {
    return this.http.get<Album[]>(this.baseUrl + 'Album/albumsByArtist/' + id);
  }

  getAlbum(id: number)  {
    return this.http.get(this.baseUrl + 'Album/' + id);
  }

  getAlbumByName(name: string) {
    return this.http.get(this.baseUrl + 'Album/albumName?name=' + name);
  }

  createAlbumByArtist(id: number, album: any) {
    return this.http.post(this.baseUrl + 'Album/create/' + id, album);
  }

  updateAlbum(artistId: number, albumId: number) {

  }
}