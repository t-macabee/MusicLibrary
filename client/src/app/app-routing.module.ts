import {NgModule} from "@angular/core";
import {Routes, RouterModule} from "@angular/router";
import {HomeComponent} from "./home/home.component";
import {MessagesComponent} from "./messages/messages.component";
import {ArtistsComponent} from "./artists/artists.component";
import {GenresComponent} from "./genres/genres.component";
import {AlbumsComponent} from "./albums/albums.component";
import {PlaylistsComponent} from "./playlists/playlists.component";
import {AuthGuard} from "./_guards/auth.guard";
import {TestErrorsComponent} from "./errors/test-errors/test-errors.component";
import {NotFoundComponent} from "./errors/not-found/not-found.component";
import {ServerErrorComponent} from "./errors/server-error/server-error.component";
import {MemberListComponent} from "./members/member-list/member-list.component";
import {MemberDetailComponent} from "./members/member-detail/member-detail.component";
import {MemberEditComponent} from "./members/member-edit/member-edit.component";
import {PreventUnsavedChangesGuard} from "./_guards/prevent-unsaved-changes.guard";
import {TracksComponent} from "./tracks/tracks.component";
import {TrackDetailComponent} from "./tracks/track-detail/track-detail.component";

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path:'',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children:[
      {path: 'tracks', component: TracksComponent},
      {path: 'tracks/:id', component: TrackDetailComponent},
      {path: 'artists', component: ArtistsComponent},
      {path: 'genres', component: GenresComponent},
      {path: 'albums', component: AlbumsComponent},
      {path: 'playlists', component: PlaylistsComponent},
      {path: 'messages', component: MessagesComponent},
      {path: 'members', component: MemberListComponent},
      {path: 'members/:username', component: MemberDetailComponent},
      {path: 'member/edit', component: MemberEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    ]
  },
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule{ }
