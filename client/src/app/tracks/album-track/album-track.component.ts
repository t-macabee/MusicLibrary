import {ChangeDetectorRef, Component, Input} from '@angular/core';
import {Track} from "../../_models/track";
import {ActivatedRoute} from "@angular/router";
import {TrackService} from "../../_services/track.service";
import {ToastrService} from "ngx-toastr";
import {AlbumService} from "../../_services/album.service";

@Component({
  selector: 'app-album-track',
  templateUrl: './album-track.component.html',
  styleUrls: ['./album-track.component.css']
})
export class AlbumTrackComponent {
  tracks: Track[] = [];
  id: number;

  albumName: string;
  artistName: string;

  showEditForm: boolean = false;
  selectedTrack: Track;

  constructor(private route: ActivatedRoute,
              private trackService: TrackService,
              private toastr: ToastrService,
              private cdr: ChangeDetectorRef,
              private albumService: AlbumService) {
  }

  ngOnInit() {
    const albumId = this.route.snapshot.params['id'];
    if (albumId) {
      this.id = +albumId;
      this.loadAlbumDetails();
      this.loadAlbumTracks();
    }
  }

  loadAlbumDetails() {
    this.albumService.getAlbum(this.id).subscribe(
      (album: any) => {
        this.albumName = album.albumName;
        this.artistName = album.artistName;
      },
      (error) => {
        console.log('Error loading album details: ', error);
      }
    );
  }

  loadAlbumTracks() {
    this.trackService.getTracksByAlbum(this.id).subscribe(
      (response: Track[]) => {
        this.tracks = response;
        this.cdr.detectChanges();
      },
      (error) => {
        this.toastr.error('Error loading tracks. Please try again later.', 'Error');
      }
    );
  }

  crateTemplateTrack() {
    const newTrack: Track = {
      id: 0,
      trackName: 'New Track',
      trackLength: 0,
      albumName: this.albumName,
      artistName: this.artistName
    };

    this.trackService.createTrack(this.id, newTrack).subscribe(
      (response: Track) => {
        this.toastr.success('Track created!');
        this.loadAlbumTracks();
      },
      (error) => {
        console.log('Error creating a track: ', error);
      }
    );
  }

  deleteTrack(tracktoDelete: any) {
      this.trackService.deleteTrack(tracktoDelete.id).subscribe(
        () => {
          this.toastr.success('Track deleted!');
          this.tracks = this.tracks.filter(track => track.id !== tracktoDelete.id);
        },
        (error) => {
          console.error('Error deleting track:', error);
        }
      );
  }

  updateTrack(updatedTrack: Track) {
    this.trackService.updateTrack(updatedTrack.id, updatedTrack).subscribe(
      (response: Track) => {
        const index = this.tracks.findIndex(track => track.id === updatedTrack.id);
        if (index !== -1) {
          this.tracks[index] = response;
        }

        this.toastr.success('Track updated!');
      },
      (error) => {
        console.log('Error updating a track: ', error);
        this.toastr.error('Error updating track');
      }
    );
  }

  editTrack(track: Track) {
    this.selectedTrack = { ...track };
    this.showEditForm = true;
  }

  closeEditForm() {
    this.showEditForm = false;
  }
}
