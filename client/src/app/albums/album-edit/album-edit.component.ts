import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Album} from "../../_models/album";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-album-edit',
  templateUrl: './album-edit.component.html',
  styleUrls: ['./album-edit.component.css']
})
export class AlbumEditComponent implements OnInit {
  @Input() album: Album;
  @Output() albumUpdated: EventEmitter<Album> = new EventEmitter<Album>();
  @Output() closeEditForm: EventEmitter<void> = new EventEmitter<void>();

  editForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.editForm = this.formBuilder.group({
      albumName: [this.album.albumName, Validators.required],
      year: [this.album.year, Validators.required],
      totalLength: [this.album.totalLength, Validators.required]
    });
  }

  updateAlbum() {
    if(this.editForm.valid) {
      const updatedAlbum: Album = {
        ...this.album,
        albumName: this.editForm.get('albumName').value,
        year: this.editForm.get('year').value,
        totalLength: this.editForm.get('totalLength').value
      };
      this.albumUpdated.emit(this.album);
    }
  }

  closeForm() {
    this.closeEditForm.emit();
  }
}
