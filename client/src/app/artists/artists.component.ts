import {Component, OnInit} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {ToastrService} from "ngx-toastr";
import {Artist} from "../_models/artist";
import {Router} from "@angular/router";

@Component({
  selector: 'app-artists',
  templateUrl: './artists.component.html',
  styleUrls: ['./artists.component.css']
})
export class ArtistsComponent implements OnInit {
  baseUrl = environment.apiUrl;
  artists: Artist[] = [];

  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router) {
  }

  ngOnInit() {
    this.getAllArtists();
  }

  getAllArtists() {
    this.http.get<Artist[]>(this.baseUrl + 'Artist').subscribe(response => {
      this.artists = response;
    })
  }

  deleteArtist(id: number) {
    this.http.delete(this.baseUrl + 'Artist/' + id).subscribe({
      next: () => this.artists?.splice(this.artists.findIndex(m => m.id === id), 1)
    })
    this.toastr.success("Artist deleted!");
    this.getAllArtists();
  }

  createTemplateArtist() {
    let sending = {
      artistName: "Artist name template",
      artistDescription: "Artist description template"
    };
    this.http.post(this.baseUrl + 'Artist', sending).subscribe(response => {
      if(response) {
        this.toastr.success("New teplate artist added!");
        this.getAllArtists();
      }
    })
  }

  artistDetails(artist : any){
    this.router.navigate(["artist-detail", artist.id]);
  }
}
