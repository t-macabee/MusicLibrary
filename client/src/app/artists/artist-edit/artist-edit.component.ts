import {ChangeDetectorRef, Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {NgForm} from "@angular/forms";
import {ToastrService} from "ngx-toastr";
import {Artist} from "../../_models/artist";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {ActivatedRoute} from "@angular/router";
import {ArtistService} from "../../_services/artist.service";
import {Genre} from "../../_models/genre";

@Component({
  selector: 'app-artist-edit',
  templateUrl: './artist-edit.component.html',
  styleUrls: ['./artist-edit.component.css']
})
export class ArtistEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if(this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  baseUrl = environment.apiUrl;
  artist: Artist;
  id: number;
  sub: any;

  genres: any;
  selectedGenreId: number;

  constructor(private http: HttpClient,
              private router: ActivatedRoute,
              private toastr: ToastrService,
              private artistService: ArtistService,
              private cdr: ChangeDetectorRef) {

  }

  ngOnInit() {
    this.sub = this.router.params.subscribe((res: any) => {
      this.id = +res["id"];
    });

    this.loadArtist();
  }

  loadArtist() {
    this.artistService.getGenres().subscribe(genres => {
      this.genres = genres;
    });

    this.artistService.getArtist(this.id).subscribe(
      response => {
        this.artist = response;
        this.selectedGenreId = this.artist.genre?.id;
      },
      error => {
        console.error("Error loading artist:", error);
      }
    );
  }

  updateArtist(artistId: number, artist: Artist) {
    artist.artistName = this.artist.artistName;
    artist.artistDescription = this.artist.artistDescription;
    artist.genreId = this.selectedGenreId;

    this.artistService.updateArtist(artistId, artist).subscribe(
      () => {
        this.toastr.success('Profile updated successfully');
        this.loadArtist();
        this.editForm.resetForm();
      },
      error => {
        console.error('Error updating artist:', error);
        this.toastr.error('Error updating artist');
      }
    );
  }
}
