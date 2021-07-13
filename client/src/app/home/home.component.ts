import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  // user: User;

  constructor(private accountService: AccountService) { 
    // this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
    //   if (user) {
    //     this.user = user
    //     console.log(user);
    //   }
    // })
  }

  ngOnInit(): void {
    
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  // getUsers() {
  //   this.http.get('https://localhost:5001/api/users').subscribe(users => this.users = users);
  // }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
