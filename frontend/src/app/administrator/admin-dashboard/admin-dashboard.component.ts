import { Component ,inject, OnInit } from '@angular/core';
import {AdminService} from '../../services/administrator.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent {
  adminService=inject(AdminService);
  router=inject(Router);

  logout() {
    this.adminService.currentAdmin.set(null);
    localStorage.removeItem('admin');
    void this.router.navigateByUrl('/admin/login');
  }
}
