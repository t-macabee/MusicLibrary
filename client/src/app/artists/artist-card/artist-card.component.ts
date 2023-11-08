import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Artist} from "../../_models/artist";
import {ArtistService} from "../../_services/artist.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-artist-card',
  templateUrl: './artist-card.component.html',
  styleUrls: ['./artist-card.component.css']
})
export class ArtistCardComponent {
  @Input() artist: any;
  @Output() artistDeleted: EventEmitter<number> = new EventEmitter<number>();


  constructor(private toastr: ToastrService, private artistService: ArtistService) {
  }

  deleteArtist(id: number) {
    this.artistService.deleteArtist(id).subscribe(() => {
        this.artistDeleted.emit(id);
      },
      (error) => {
        console.error('Error deleting artist:', error);
      }
    )
  }
}
