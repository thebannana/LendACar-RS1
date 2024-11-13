import {Component, inject, OnInit} from '@angular/core';
import {UserService} from './services/user.service';
import {UserDto} from './Models/UserDto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'frontend';
  private accountService=inject(UserService)
  ngOnInit() {
    this.setCurrentUSer();
  }

  setCurrentUSer(){
    const userString=localStorage.getItem('user');
    if(!userString)return;
    const user:UserDto=JSON.parse(userString);
    this.accountService.currentUser.set(user);
    console.log(this.accountService.currentUser());
  }
}
