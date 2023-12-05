import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule} from "@angular/forms";
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule} from "./_modules/shared.module";
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor} from "./_interceptor/error.interceptor";
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorsComponent } from './errors/server-errors/server-errors.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from "./_interceptor/jwt.interceptor";
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { ButtonsModule } from "ngx-bootstrap/buttons";
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { GenreComponent } from './genre/genre.component';
import { ArtistDetailComponent } from './artists/artist-detail/artist-detail.component';
import { ArtistEditComponent } from './artists/artist-edit/artist-edit.component';
import { ArtistListComponent } from './artists/artist-list/artist-list.component';
import { ArtistCardComponent } from './artists/artist-card/artist-card.component';
import { AlbumEditComponent } from './albums/album-edit/album-edit.component';
import { TabService } from "./_services/tab.service";
import { AlbumDetailComponent } from './albums/album-detail/album-detail.component';
import { TrackListComponent } from './tracks/track-list/track-list.component';
import { AlbumTrackComponent } from './tracks/album-track/album-track.component';
import { TrackEditComponent } from './tracks/track-edit/track-edit.component';
import { ArtistPhotoEditorComponent } from './artists/artist-photo-editor/artist-photo-editor.component';
import { BackgroundComponent } from './background/background.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    MessagesComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorsComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TextInputComponent,
    DateInputComponent,
    MemberMessagesComponent,
    GenreComponent,
    ArtistDetailComponent,
    ArtistEditComponent,
    ArtistListComponent,
    ArtistCardComponent,
    AlbumEditComponent,
    AlbumDetailComponent,
    TrackListComponent,
    AlbumTrackComponent,
    TrackEditComponent,
    ArtistPhotoEditorComponent,
    BackgroundComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        ButtonsModule
    ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    [TabService]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
