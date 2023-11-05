import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Artist } from '../_models/artist';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {
  private baseUrl = 'your-api-base-url';

  constructor(private http: HttpClient) {}

  // Get a single artist by ID
  getArtist(id: number): Observable<Artist> {
    return this.http.get<Artist>(`${this.baseUrl}/artists/${id}`);
  }

  // Update artist information
  updateArtist(id: number, artist: Artist): Observable<Artist> {
    return this.http.put<Artist>(`${this.baseUrl}/artists/${id}`, artist);
  }

  // Set the main photo for an artist
  setMainPhoto(photoId: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/artists/setMainPhoto/${photoId}`, null);
  }

  // Delete a photo for an artist
  deletePhoto(photoId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/artists/deletePhoto/${photoId}`);
  }

  // Add a new photo for an artist
  addPhoto(artistId: number, photo: Photo): Observable<Photo> {
    return this.http.post<Photo>(`${this.baseUrl}/artists/addPhoto/${artistId}`, photo);
  }

  // Additional methods for artist-related operations
}
