import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {PlaylistService} from "../../_services/playlist.service";
import {Playlist} from "../../_models/playlist";
import {AccountService} from "../../_services/account.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-user-playlist',
  templateUrl: './user-playlist.component.html',
  styleUrls: ['./user-playlist.component.css']
})
export class UserPlaylistComponent implements OnInit{
  playlists: Playlist[] = [];
  selectedPlaylist: Playlist | null;
  memberId: number;

  constructor(private playlistService: PlaylistService,
              private accountService: AccountService,
              private toastr: ToastrService,
              ) {
  }

  ngOnInit() {
    this.accountService.currentUser$.subscribe(user => {
      if (user) {
        this.memberId = user.id;
      }
    });
    this.GetPlaylists();
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

  selectPlaylist(playlist: Playlist) {
    this.selectedPlaylist = playlist;
  }

  CreateNewPlaylist() {
    let sending: any = {
      playlistName: 'Default playlist',
      playlistDescription: 'Default description'
    }
    this.playlistService.createPlaylist(this.memberId, sending).subscribe(response => {
      if(response) {
        this.toastr.success('Playlist created!');
        this.GetPlaylists();
      }
    })
  }

  updatePlaylist(updatedPlaylist: Playlist) {
    this.playlistService.updatePlaylist(updatedPlaylist.id, updatedPlaylist).subscribe(
      (response: Playlist) => {
        const updatedPlaylists = this.playlists.map(playlist => {
          return playlist.id === updatedPlaylist.id ? { ...playlist, ...response } : playlist;
        });

        this.playlists = [...updatedPlaylists];
        this.toastr.success('Playlist updated!');
      },
      (error) => {
        this.toastr.error('Failed to update the playlist');
      }
    );
  }

  deletePlaylist(selectedPlaylist: Playlist) {
    this.playlistService.deletePlaylist(this.memberId, selectedPlaylist.id).subscribe(
      () => {
        this.toastr.success('Playlist deleted!');
        this.playlists = this.playlists.filter(playlist => playlist.id !== selectedPlaylist.id);
        this.selectedPlaylist = null;
      },
      (error) => {
        console.error('Error deleting playlist:', error);
      }
    );
  }
}
