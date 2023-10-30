import {Component, OnInit} from '@angular/core';
import {Genre} from "../_models/genre";
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.css']
})
export class GenreComponent implements OnInit {
  baseUrl = environment.apiUrl;
  genres: Genre[] = [];
  txtGenre: any;

  constructor(private http: HttpClient, private toastr: ToastrService) {
  }

  ngOnInit() {
    this.getAllGenres();
  }

  getAllGenres() {
    this.http.get<Genre[]>(this.baseUrl + 'Genre').subscribe(response => {
      this.genres = response;
    })
  }

  createNewGenre() {
    let sending = {
      genreName: this.txtGenre
    };
    this.http.post(this.baseUrl + 'Genre/new-genre', sending).subscribe(response => {
      if(response) {
        this.toastr.success("New genre added!");
        this.getAllGenres();
      }
    })
  }

  deleteGenre(id: number) {
    this.http.delete(this.baseUrl + 'Genre/?id=' + id).subscribe({
      next: () => this.genres?.splice(this.genres.findIndex(m => m.id === id), 1)
    })
    this.toastr.success("Genre deleted!");
    this.getAllGenres();
  }
}
