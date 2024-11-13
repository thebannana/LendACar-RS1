import {Component, inject} from '@angular/core';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent {
      accountService=inject(UserService);
}
