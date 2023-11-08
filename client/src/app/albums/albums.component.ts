import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {TabDirective, TabsetComponent} from "ngx-bootstrap/tabs";
import {NgxGalleryImage, NgxGalleryOptions} from "@kolkov/ngx-gallery";
import {ActivatedRoute, Router} from "@angular/router";
import {Artist} from "../_models/artist";
import {TabService} from "../_services/tab.service";
import {AlbumService} from "../_services/album.service";
import {ToastrService} from "ngx-toastr";
import {Album} from "../_models/album";

@Component({
  selector: 'app-albums',
  templateUrl: './albums.component.html',
  styleUrls: ['./albums.component.css']
})
export class AlbumsComponent implements OnInit {
  @Input() artist: Artist;
  albums: Album[] = [];
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];
  activeTab?: TabDirective;

  constructor(private route: ActivatedRoute,
              private tabService: TabService,
              private albumService: AlbumService,
              private toastr: ToastrService
              ) { }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.artist = data['artist'];
        this.route.queryParams.subscribe(params => {
          params['tab'] && this.selectTab(params['tab']);
        });
      },
    });

    this.getAlbumsByArtist();
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
  }

  selectTab(heading: string) {
    if (this.tabService.tabset) {
      const selectedTab = this.tabService.tabset.tabs.find(tab => tab.heading === heading);
      if (selectedTab) {
        selectedTab.active = true;
      }
    }
  }

  createAlbumForArtist() {
    const artistId = this.artist.id;
    const newAlbum = {
      albumName: 'New Album',
      year: '2023',
      totalLength: 0
    };

    this.albumService.createAlbumByArtist(artistId, newAlbum).subscribe(response => {
      this.toastr.success('Album created!');
      console.log('Album created: ', response);
    }, error => {
      this.toastr.error('Error creating an album!');
      console.log('Error creating an album: ', error);
    })
  }

  getAlbumsByArtist(){
    this.albumService.getAlbumsByArtist(this.artist.id).subscribe(response => {
      this.albums = response;
    }, error => {
      console.log('Error getting a list of albums by artist', error);
    })
  }

}
