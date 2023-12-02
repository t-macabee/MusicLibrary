import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Album} from "../_models/album";
import {BehaviorSubject, catchError, tap} from "rxjs";
import {AlbumUpdate} from "../_models/albumUpdate";

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

  deleteAlbum(artistId: number, albumId: number) {
    return this.http.delete(this.baseUrl + 'Album/' + artistId + '/' + albumId);
  }

  updateAlbum(artistId: number, albumId: number, update: AlbumUpdate) {
    return this.http.put(this.baseUrl + 'Album/' + artistId + '/' + albumId, update);
  }

}
