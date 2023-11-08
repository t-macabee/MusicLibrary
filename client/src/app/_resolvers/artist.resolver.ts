import {Artist} from "../_models/artist";
import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {ArtistService} from "../_services/artist.service";

@Injectable({
  providedIn: 'root'
})
export class ArtistResolver implements Resolve<Artist> {

  constructor(private artistService: ArtistService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Artist> {
    const id = route.paramMap.get('id');
    if(id) {
      return this.artistService.getArtist(+id);
    }
  }
}
