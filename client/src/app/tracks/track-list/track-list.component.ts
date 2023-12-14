import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {TrackService} from "../../_services/track.service";
import {Track} from "../../_models/track";
import {MatDialog} from "@angular/material/dialog";
import {UserPlaylistDialogComponent} from "../../playlists/user-playlist-dialog/user-playlist-dialog.component";


@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css']
})
export class TrackListComponent implements OnInit {
  tracks: Track[] = [];
  searchTerm = '';

  constructor(private trackService: TrackService,
              private dialog: MatDialog,
              private cdr: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.getTracks();
  }

  getTracks() {
    this.trackService.getAllTracks().subscribe(response => {
      this.tracks = response;
    })
  }

  openPlaylistDialog(track: Track) {
    const dialogRef = this.dialog.open(UserPlaylistDialogComponent, {
      height: '350px',
      width: '350px',
      disableClose: true,
      data: { track: track }
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.cdr.detectChanges();
    });
  }

  get filteredTracks() {
    return this.tracks.filter((track) =>
      track.trackName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}
