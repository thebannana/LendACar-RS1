import {Component, inject} from '@angular/core';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  accountService=inject(UserService);

  logout() {
    this.accountService.currentUser.set(null);
  }
}


