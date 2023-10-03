import {Component, OnInit} from '@angular/core';
import {Member} from "../../_models/member";
import {MembersService} from "../../_services/members.service";
import {ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member;

  constructor(private memberService: MembersService, private router: ActivatedRoute) {

  }

  ngOnInit() {
    this.loadMember();
  }

  loadMember() {
    this.memberService.getMember(this.router.snapshot.paramMap.get('username'))
      .subscribe(x => {
        this.member = x;
      })
  }


}
