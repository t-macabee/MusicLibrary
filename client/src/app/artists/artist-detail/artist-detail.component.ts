import {Component, OnInit, ViewChild} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {environment} from "../../../environments/environment";
import {Artist} from "../../_models/artist";
import {NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions} from "@kolkov/ngx-gallery";
import {TabDirective, TabsetComponent} from "ngx-bootstrap/tabs";


@Component({
  selector: 'app-artist-detail',
  templateUrl: './artist-detail.component.html',
  styleUrls: ['./artist-detail.component.css']
})
export class ArtistDetailComponent implements OnInit {
  @ViewChild('artistTabs', {static: true}) artistTabs?: TabsetComponent;
  id: number;
  sub: any;
  baseUrl = environment.apiUrl;
  artist: Artist;
  activeTab?: TabDirective;

  constructor(private http: HttpClient, private router: ActivatedRoute, private route: Router) {
  }

  ngOnInit() {
    this.sub = this.router.params.subscribe((res:any) => {
      this.id = +res["id"];
      this.loadArtist();
    })
  }

  loadArtist() {
    this.http.get<Artist>(this.baseUrl + 'Artist/id?id=' + this.id).subscribe(results => {
      this.artist = results;
    }, error => {
      console.log(error);
    })
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
  }

  selectTab(heading: string) {
    if(this.artistTabs) {
      this.artistTabs.tabs.find(x => x.heading === heading)!.active = true;
    }
  }

  editArtist(artist: any) {
      this.route.navigate(["artist-edit", artist.id]);
  }
}
