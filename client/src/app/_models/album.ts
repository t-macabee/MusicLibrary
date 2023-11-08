import {Track} from "./track";

export interface Album {
  id: number;
  artistId: number;
  artistName: string;
  albumName: string;
  year: string;
  totalLength: number;
  tracks: Track[];
}
