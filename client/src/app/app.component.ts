import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { PresenceService } from './_services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'My dating app';
  users: any;

  constructor(private accountService: AccountService, private presence: PresenceService){}

  ngOnInit(): void {
    // this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User =  JSON.parse(localStorage.getItem('user') + '');
    // console.log(user);
    if (user) {
      this.accountService.setCurrentUser(user);
      this.presence.createHubConnection(user);
    }
  }

  // getUsers() {
  //   this.http.get('https://localhost:5001/api/users').subscribe(response => {
  //     this.users = response
  //   }, error => {
  //     console.log(error);
  //   })
  // }
}
