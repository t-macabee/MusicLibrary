import {Artist} from "./artist";
import {Genre} from "./genre";

export interface Track {
    id: number;
    trackName: string;
    length: string;
    genreId: number;
    genre: Genre;
    artist: Artist;
    about: string;
  }

