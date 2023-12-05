import {Album} from "./album";
import {Genre} from "./genre";
import {Photo} from "./photo";

export interface Artist {
  id: number;
  artistName: string;
  artistDescription: string;
  photoUrl: string;
  genreId: number;
  genre: Genre;
  albums: Album[];
  photos: Photo[];
}
