import {Album} from "./album";
import {ArtistPhoto} from "./artistPhoto";

export interface Artist {
  id: number;
  artistName: string;
  artistDescription: string;
  photoUrl: string;
  albums: Album[];
  photos: ArtistPhoto[];
}
