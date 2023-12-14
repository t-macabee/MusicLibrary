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

  getAlbumsByArtist(id: number) {
    return this.http.get<Album[]>(this.baseUrl + 'Album/albumsByArtist/' + id);
  }

  getAlbum(id: number)  {
    return this.http.get(this.baseUrl + 'Album/' + id);
  }

  createAlbumByArtist(id: number, album: any) {
    return this.http.post(this.baseUrl + 'Album/create/' + id, album);
  }

  deleteAlbum(artistId: number, albumId: number) {
    return this.http.delete(this.baseUrl + 'Album/' + artistId + '/' + albumId);
  }

  updateAlbum(artistId: number, albumId: number, update: Album) {
    return this.http.put(this.baseUrl + 'Album/' + artistId + '/' + albumId, update);
  }

}
