import {Component, inject, model} from '@angular/core';
import {UserService} from '../../services/user.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
   model:any = {};
  accountService=inject(UserService);

  register() {
    this.accountService.Register(this.model).subscribe({
      next: result => console.log(result),
      error: err => console.log(err)
    })
  }
}
