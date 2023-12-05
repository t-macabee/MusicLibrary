import {Component, Input, OnInit} from '@angular/core';
import {ArtistService} from "../../_services/artist.service";
import {Photo} from "../../_models/photo";
import {FileUploader} from "ng2-file-upload";
import {Artist} from "../../_models/artist";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-artist-photo-editor',
  templateUrl: './artist-photo-editor.component.html',
  styleUrls: ['./artist-photo-editor.component.css']
})
export class ArtistPhotoEditorComponent implements OnInit{
  @Input() artist: Artist;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;

  constructor(private artistService: ArtistService) {
  }

  ngOnInit() {
    if (this.artist) {
      this.initializeUploader();
    }
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photo: Photo) {
    this.artistService.setMainPhoto(this.artist.id, photo.id).subscribe(() => {
      this.artist.photoUrl = photo.url;
      this.artist.photos.forEach(p => p.isMain = p.id === photo.id);
    });
  }

  deletePhoto(photoId: number) {
    this.artistService.deletePhoto(this.artist.id, photoId).subscribe(() => {
      this.artist.photos = this.artist.photos.filter(x => x.id !== photoId);
    })
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + `Artist/${this.artist.id}/photos`,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (_, response) => {
      if (response) {
        const photo = JSON.parse(response);
        this.artist.photos.push(photo);
      }
    }
  }
}
