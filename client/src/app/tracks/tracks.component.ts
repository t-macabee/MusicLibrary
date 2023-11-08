import {Component, OnInit} from '@angular/core';
import {Track} from "../_models/track";
import {TrackService} from "../_services/track.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-tracks',
  templateUrl: './tracks.component.html',
  styleUrls: ['./tracks.component.css']
})
export class TracksComponent implements OnInit {
  tracks: Track[];

  constructor(private trackService: TrackService, private toastr: ToastrService) {
  }

  ngOnInit() {
    this.getAllTracks();
  }

  getAllTracks() {
    return this.trackService.getAllTracks().subscribe(response => {
      this.tracks = response;
    }, error => {
      console.log(error);
    })
  }

  deleteTrack(id: number) {
    this.trackService.deleteTrack(id).subscribe(() => {
        this.toastr.success("Track deleted!");
        const index = this.tracks.findIndex((x) => x.id === id);
        if(index !== -1){
          this.tracks.splice(index, 1);
        }
      },
      (error) => {
        console.error('Error deleting track:', error);
      }
    )
    this.getAllTracks();
  }
}
