import {Component, inject} from '@angular/core';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent {

  accountService=inject(UserService);
}
