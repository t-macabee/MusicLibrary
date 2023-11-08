import {Album} from "./album";
import {Genre} from "./genre";

export interface Artist {
  id: number;
  artistName: string;
  artistDescription: string;
  genre: Genre;
  albums: Album[];
}
