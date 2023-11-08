import { Injectable } from '@angular/core';
import {TabsetComponent} from "ngx-bootstrap/tabs";

@Injectable({
  providedIn: 'root'
})
export class TabService {
  tabset?: TabsetComponent;

  constructor() { }
}
