import {Component, OnInit} from '@angular/core';
import {PlaylistService} from "../../_services/playlist.service";
import {Playlist} from "../../_models/playlist";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-member-playlist',
  templateUrl: './member-playlist.component.html',
  styleUrls: ['./member-playlist.component.css']
})
export class MemberPlaylistComponent implements OnInit{
  playlists: Playlist[] = [];
  memberId: number;
  selectedPlaylist: Playlist | null = null;

  constructor(private route: ActivatedRoute, private playlistSerivce: PlaylistService) {

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.memberId = params['id'];
      this.getUserPlaylists();
    });
  }

  getUserPlaylists() {
    this.playlistSerivce.getPlaylistsByUser(this.memberId).subscribe(response => {
      this.playlists = response;
    }, error => {
      console.log(error);
    })
  }

  selectPlaylist(playlist: Playlist) {
    this.selectedPlaylist = playlist;
  }
}
