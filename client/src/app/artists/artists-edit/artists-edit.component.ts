import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-artists-edit',
  templateUrl: './artists-edit.component.html',
  styleUrls: ['./artists-edit.component.css']
})
export class ArtistsEditComponent implements OnInit {
  @Input()
  editArtist: any;
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  saveChanges() {
    this.http.put(this.baseUrl + 'Artist/UpdateArtist/id?id=' + this.editArtist.id, this.editArtist).subscribe((returnValue : any) => {
      alert("Success!");
    });
  }
}
