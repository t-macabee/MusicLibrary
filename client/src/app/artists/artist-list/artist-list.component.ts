import { Component } from '@angular/core';
import {Artist} from "../../_models/artist";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {ArtistService} from "../../_services/artist.service";
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.css']
})
export class ArtistListComponent {
  baseUrl = environment.apiUrl;
  artists: Artist[] = [];
  searchTerm = '';

  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router, private artistService: ArtistService) {
  }

  ngOnInit() {
    this.getAllArtists();
  }

  getAllArtists() {
    this.artistService.getArtists().subscribe(response => {
      this.artists = response;
    }, error => {
      console.log(error);
    });
  }

  deleteArtist(id: number) {
    this.artistService.deleteArtist(id).subscribe(() => {
        this.toastr.success("Artist deleted!");
        const index = this.artists.findIndex((x) => x.id === id);
        if(index !== -1){
          this.artists.splice(index, 1);
        }
      },
      (error) => {
        console.error('Error deleting artist:', error);
      }
    )
    this.getAllArtists();
  }

  createTemplateArtist() {
    let sending: any = {
      artistName: "Default name",
      artistDescription: "Default description",
      genreId: 1
    };
    this.artistService.createTemplateArtist(sending).subscribe(response => {
      if(response) {
        this.toastr.success("New teplate artist added!");
        this.getAllArtists();
      }
    },(error) => {
      console.error('Error adding template artist:', error);
    })
  }

  artistDetails(artist : any){
    this.router.navigate(["artist-detail", artist.id]);
  }

  handleArtistDeleted(id: number) {
    this.artists = this.artists.filter((artist) => artist.id !== id);
    this.toastr.success('Artist deleted!');
  }

  get filteredArtists() {
    return this.artists.filter((artist) =>
      artist.artistName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}
