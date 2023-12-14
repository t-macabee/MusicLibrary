import {Track} from "./track";

export interface Playlist {
  id: number;
  playlistName: string;
  playlistDescription: string;
  tracks: Track[];
}
