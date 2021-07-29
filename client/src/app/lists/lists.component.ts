import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members!: Partial<Member[]>;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination!: Pagination;
  memberUrl!: string;
                                                                                        
  constructor(private memberService: MembersService, private router: Router) { }

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.memberService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe(response => {
      // console.log(response);
        this.members = response.result;
        this.pagination = response.pagination;
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadLikes();
  }

  sendUrlToFaceService(urlImageToAzure: string) {
    this.router.navigate(['/azureservice'], { queryParams: { urlImage: urlImageToAzure}});
  }
}
