import {Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {NgForm} from "@angular/forms";
import {Member} from "../../_models/member";
import {User} from "../../_models/user";
import {AccountService} from "../../_services/account.service";
import {MembersService} from "../../_services/members.service";
import {ToastrService} from "ngx-toastr";
import {map, of, take} from "rxjs";
import {Artist} from "../../_models/artist";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {ActivatedRoute} from "@angular/router";
import {ArtistService} from "../../_services/artist.service";


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

  constructor(private http: HttpClient, private router: ActivatedRoute, private toastr: ToastrService, private artistService: ArtistService) {

  }

  ngOnInit() {
    this.sub = this.router.params.subscribe((res:any) => {
      this.id = +res["id"];
    })
    this.loadArtist();
  }

  loadArtist() {
    this.artistService.getArtist(this.id).subscribe(response => {
      this.artist = response;
    })
  }

  updateArtist(artistId: number, artist: Artist) {
    this.artistService.updateArtist(artistId, artist).subscribe(
      () => {
        this.toastr.success("Profile updated successfully");
        this.editForm.reset(this.artist);
      },
      error => {
        console.error("Error updating artist:", error);
        this.toastr.error('Error updating artist');
      }
    );
  }
}
