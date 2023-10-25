import {Component, Input} from '@angular/core';
import {Member} from "../../_models/member";
import {PresenceService} from "../../_services/presence.service";

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent {
  @Input() member: Member;

  constructor(public presence: PresenceService) {
  }
}
