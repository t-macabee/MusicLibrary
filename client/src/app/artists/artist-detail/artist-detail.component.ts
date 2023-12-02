import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {environment} from "../../../environments/environment";
import {Artist} from "../../_models/artist";
import {TabDirective} from "ngx-bootstrap/tabs";
import {TabService} from "../../_services/tab.service";

@Component({
  selector: 'app-artist-detail',
  templateUrl: './artist-detail.component.html',
  styleUrls: ['./artist-detail.component.css']
})
export class ArtistDetailComponent implements OnInit {
  @Input() artist: Artist;
  albums: any;
  id: number;
  sub: any;
  baseUrl = environment.apiUrl;
  activeTab?: TabDirective;

  constructor(private http: HttpClient,
              private router: ActivatedRoute,
              private route: Router,
              private tabsService: TabService
              ) {}

  ngOnInit() {
    this.sub = this.router.params.subscribe((res:any) => {
      this.id = +res["id"];
      this.loadArtist();
    })
  }

  loadArtist() {
    this.http.get<Artist>(this.baseUrl + 'Artist/' + this.id).subscribe(results => {
      this.artist = results;
    }, error => {
      console.log(error);
    })
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    this.tabsService.tabset = data!.tabset;
  }

  selectTab(heading: string) {
    if(this.tabsService.tabset) {
      this.tabsService.tabset.tabs.find(x => x.heading === heading)!.active = true;
    }
  }

  editArtist(artist: any) {
    this.route.navigate(["artist-edit", artist.id]);
  }
}
