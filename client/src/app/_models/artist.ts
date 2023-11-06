import {Album} from "./album";

export interface Artist {
  id: number;
  artistName: string;
  artistDescription: string;
  photoUrl: string;
  albums: Album[];
}
