import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {Playlist} from "../../_models/playlist";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {AccountService} from "../../_services/account.service";
import {PlaylistService} from "../../_services/playlist.service";
import {Track} from "../../_models/track";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-user-playlist-dialog',
  templateUrl: './user-playlist-dialog.component.html',
  styleUrls: ['./user-playlist-dialog.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class UserPlaylistDialogComponent implements OnInit{
  playlists: Playlist[] = [];
  memberId: number;
  track: any;

  constructor(public dialogRef: MatDialogRef<UserPlaylistDialogComponent>,
              private accountService: AccountService,
              private playlistService: PlaylistService,
              private toastr: ToastrService,
              @Inject(MAT_DIALOG_DATA) public data: { track: Track}) {
    this.track = data.track;
  }

  ngOnInit() {
    this.accountService.currentUser$.subscribe(user => {
      if (user) {
        this.memberId = user.id;
      }
    });
    this.GetPlaylists();
  }

  closeDialog() {
    this.dialogRef.close();
  }

  GetPlaylists() {
    this.playlistService.getPlaylistsByUser(this.memberId).subscribe(response => {
      if(response) {
        this.playlists = response;
      }
    }, error => {
      console.log(error);
    })
  }

  addToPlaylist(playlist: Playlist) {
    this.playlistService.addTrackToPlaylist(playlist.id, this.track.id).subscribe(response => {
      this.toastr.success('Track added to playlist');
    }, error => {
      console.log(error);
    })
  }
}
