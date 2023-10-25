import {CanDeactivate} from '@angular/router';
import {Injectable} from "@angular/core";
import {MemberEditComponent} from "../members/member-edit/member-edit.component";


@Injectable({
  providedIn: "root"
})

export class PreventUnsavedChangesGuard implements CanDeactivate<unknown>{
  constructor() { }

  canDeactivate(component: MemberEditComponent): boolean {
    if(component.editForm?.dirty) {
      return confirm("Any unsaved changes will be lost!");
    }
    return true;
  }
}
