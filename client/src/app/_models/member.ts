import {Photo} from "./photo";
import {Playlist} from "./playlist";

export interface Member {
  id: number;
  username: string;
  photoUrl: string;
  age: number;
  knownAs: string;
  created: Date;
  lastActive: Date;
  interests: string;
  country: string;
  photos: Photo[];
  playlist: Playlist[];
}


