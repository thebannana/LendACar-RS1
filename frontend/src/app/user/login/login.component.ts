import {Component, inject} from '@angular/core';
import {UserService} from '../../services/user.service';
import {LoginUserDto} from '../../Models/LoginUserDto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
accountService=inject(UserService);
  model:any={};

  login() {
    this.accountService.Login(this.model).subscribe({
      next:res=>{
        console.log(res);
      },
      error:err=>console.log(err)
    })
    console.log(this.accountService.currentUser());
    console.log("Login 2");
  }
}
