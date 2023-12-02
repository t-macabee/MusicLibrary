import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Track} from "../../_models/track";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-track-edit',
  templateUrl: './track-edit.component.html',
  styleUrls: ['./track-edit.component.css']
})
export class TrackEditComponent {
  @Input() track: Track;
  @Output() trackUpdated: EventEmitter<Track> = new EventEmitter<Track>();
  @Output() closeEditForm: EventEmitter<void> = new EventEmitter<void>();

  editForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.editForm = this.formBuilder.group({
      trackName: [this.track.trackName, Validators.required],
      trackLength: [this.track.trackLength, Validators.required]
    });
  }

  updateTrack() {
    if (this.editForm.valid) {
      const updatedTrack: Track = {
        ...this.track,
        trackName: this.editForm.get('trackName').value,
        trackLength: this.editForm.get('trackLength').value,
        albumName: this.track.albumName,
        artistName: this.track.artistName
      };
      this.trackUpdated.emit(updatedTrack);
    }
  }

  closeForm() {
    this.closeEditForm.emit();
  }
}

