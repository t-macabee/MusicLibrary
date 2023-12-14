import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Playlist} from "../../_models/playlist";

@Component({
  selector: 'app-user-playlist-edit',
  templateUrl: './user-playlist-edit.component.html',
  styleUrls: ['./user-playlist-edit.component.css']
})
export class UserPlaylistEditComponent implements OnInit{
  @Input() playlist: Playlist;
  @Output() playlistUpdated: EventEmitter<Playlist> = new EventEmitter<Playlist>();
  @Output() closeEditForm: EventEmitter<void> = new EventEmitter<void>();

  editForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.editForm = this.formBuilder.group({
      playlistName: [this.playlist.playlistName, Validators.required],
      playlistDescription: [this.playlist.playlistDescription, Validators.required]
    });
  }

  updatePlaylist() {
    if (this.editForm.valid) {
      const updatedPlaylist: Playlist = {
        ...this.playlist,
        playlistName: this.editForm.get('playlistName').value,
        playlistDescription: this.editForm.get('playlistDescription').value,
        id: this.playlist.id,
        tracks: this.playlist.tracks
      };
      this.playlistUpdated.emit(updatedPlaylist);
    }
  }

  closeForm() {
    this.closeEditForm.emit();
  }
}
