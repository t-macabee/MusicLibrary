import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Track} from "../_models/track";
import {catchError, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TrackService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAllTracks() {
    return this.http.get<Track[]>(this.baseUrl + 'Track');
  }

  deleteTrack(id: number) {
    return this.http.delete(this.baseUrl + 'Track/tracks/' + id).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error deleting track:', error );
        return throwError('Something went wrong while deleting the track.');
      })
    )
  }
}
