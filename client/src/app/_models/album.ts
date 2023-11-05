import {Track} from "./track";

export interface Album {
  id: number;
  albumName: string;
  year: string;
  tracks: Track[];
}
