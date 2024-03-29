import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {MemberListComponent} from "./members/member-list/member-list.component";
import {MemberDetailComponent} from "./members/member-detail/member-detail.component";
import {MessagesComponent} from "./messages/messages.component";
import {AuthGuard} from "./_guards/auth.guard";
import {NotFoundComponent} from "./errors/not-found/not-found.component";
import {ServerErrorsComponent} from "./errors/server-errors/server-errors.component";
import {MemberEditComponent} from "./members/member-edit/member-edit.component";
import {PreventUnsavedChangesGuard} from "./_guards/prevent-unsaved-changes.guard";
import {MemberDetailedResolver} from "./_resolvers/member-detailed.resolver";
import {ArtistDetailComponent} from "./artists/artist-detail/artist-detail.component";
import {ArtistEditComponent} from "./artists/artist-edit/artist-edit.component";
import {ArtistListComponent} from "./artists/artist-list/artist-list.component";
import {ArtistResolver} from "./_resolvers/artist.resolver";
import {AlbumDetailComponent} from "./albums/album-detail/album-detail.component";
import {AlbumTrackComponent} from "./tracks/album-track/album-track.component";
import {TrackListComponent} from "./tracks/track-list/track-list.component";
import {MemberPlaylistComponent} from "./members/member-playlist/member-playlist.component";
import {UserPlaylistComponent} from "./playlists/user-playlist/user-playlist.component";

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'members', component: MemberListComponent},
      {path: 'members/:username', component: MemberDetailComponent, resolve: {member: MemberDetailedResolver}},
      {path: 'member/edit', component: MemberEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      {path: 'messages', component: MessagesComponent},
      {path: 'artists', component: ArtistListComponent},
      {path: 'artist-detail/:id', component: ArtistDetailComponent, resolve: {artist: ArtistResolver}},
      {path: 'artist-edit/:id', component: ArtistEditComponent},
      {path: 'albums', component: AlbumDetailComponent},
      {path: 'album-tracks/:id', component: AlbumTrackComponent},
      {path: 'track-list', component: TrackListComponent},
      {path: 'member-playlist/:id', component: MemberPlaylistComponent},
      {path: 'user-playlist', component: UserPlaylistComponent}
    ]
  },
  {path: 'not-found', component: NotFoundComponent },
  {path: 'server-error', component: ServerErrorsComponent},
  {path: '**', component: NotFoundComponent, pathMatch: "full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
