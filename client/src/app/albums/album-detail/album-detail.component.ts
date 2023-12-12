import {Component, Input, OnInit} from '@angular/core';
import {TabDirective} from "ngx-bootstrap/tabs";
import {ActivatedRoute, Router} from "@angular/router";
import {Artist} from "../../_models/artist";
import {TabService} from "../../_services/tab.service";
import {AlbumService} from "../../_services/album.service";
import {ToastrService} from "ngx-toastr";
import {Album} from "../../_models/album";

@Component({
  selector: 'app-albums',
  templateUrl: './album-detail.component.html',
  styleUrls: ['./album-detail.component.css']
})
export class AlbumDetailComponent implements OnInit {
  @Input() artist: Artist;
  albums: Album[] = [];
  activeTab?: TabDirective;

  showEditForm: boolean = false;
  selectedAlbum: Album;
  selectedArtistId: number;

  constructor(
    private route: ActivatedRoute,
    private tabService: TabService,
    private albumService: AlbumService,
    private toastr: ToastrService,
    private router: Router
  )  { }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.artist = data['artist'];
        this.route.queryParams.subscribe(params => {
          params['tab'] && this.selectTab(params['tab']);
        });
      },
    });
    if (this.artist && this.artist.albums?.length > 0) {
      this.loadAlbums();
    }
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
  }

  selectTab(heading: string) {
    if (this.tabService.tabset) {
      const selectedTab = this.tabService.tabset.tabs.find(
        (tab) => tab.heading === heading
      );
      if (selectedTab) {
        selectedTab.active = true;
      }
    }
  }

  createTemplateAlbum() {
    const artistId = this.artist.id;
    const newAlbum: Album = {
      id: 0,
      artistId: artistId,
      artistName: this.artist.artistName,
      albumName: 'New Album',
      year: '2023',
      totalLength: 0,
      tracks: []
    };

    this.albumService.createAlbumByArtist(artistId, newAlbum).subscribe(
      (response: Album) => {
        this.toastr.success("Album created!");
        this.albums.push(response);
      },
      (error) => {
        console.log('Error creating an album: ', error);
      }
    );
  }

  loadAlbums() {
    this.albumService.getAlbumsByArtist(this.artist.id).subscribe(
      (response) => {
        this.albums = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  deleteAlbum(artistId: number, albumId: number) {
    this.albumService.deleteAlbum(artistId, albumId).subscribe(() => {
      const index = this.albums.findIndex(album => album.id === albumId);
      if (index !== -1) {
        this.albums.splice(index, 1);
      }
      this.toastr.success("Album deleted!");
    }, (error) => {
      console.log(error);
    });
  }

  editAlbum(album: Album) {
    this.selectedAlbum = { ...album };
    this.selectedArtistId = this.artist.id;
    this.showEditForm = true;
  }

  closeEditForm() {
    this.showEditForm = false;
  }

  updateAlbum(updatedAlbum: Album) {
    this.albumService.updateAlbum(this.selectedArtistId, updatedAlbum.id, updatedAlbum).subscribe(
      (response: Album) => {
        const index = this.albums.findIndex(album => album.id === updatedAlbum.id);
        if (index !== -1) {
          this.albums[index] = response;
        }
        if (this.artist.id === response.artistId) {
          this.artist.artistName = response.artistName;
        }
        this.toastr.success("Album updated!");
        this.showEditForm = false;
      },
      (error) => {
        console.log('Error updating an album: ', error);
        this.toastr.error("Error updating album");
      }
    );
  }

  albumTracks(album: any) {
    this.router.navigate(["album-tracks", album.id]);
  }
}
