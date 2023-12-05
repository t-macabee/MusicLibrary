import {Component, OnInit} from '@angular/core';
import {TrackService} from "../../_services/track.service";
import {Track} from "../../_models/track";

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css']
})
export class TrackListComponent implements OnInit {
  tracks: Track[] = [];

  constructor(private trackService: TrackService) {
  }

  ngOnInit() {
    this.getTracks();
  }

  getTracks() {
    this.trackService.getAllTracks().subscribe(response => {
      this.tracks = response;
    })
  }
}
