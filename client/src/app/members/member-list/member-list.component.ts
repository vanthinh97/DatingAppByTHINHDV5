import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members$!: Observable<Member[]>;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    // if ((!localStorage.getItem("user") == false)) {this.loadMember();}

    this.members$ = this.memberService.getMembers();
    
  }

  // loadMember() {
    
  //   this.memberService.getMembers().subscribe(members => {
  //     this.members = members;
  //   })
  // }

}
