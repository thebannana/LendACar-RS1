import {Component, Inject, inject} from '@angular/core';
import {EmployeeService} from '../../services/employee.service';
import {WorkingHour}  from '../../Models/WorkingHour';

@Component({
  selector: 'app-profile-overview',
  templateUrl: './profile-overview.component.html',
  styleUrl: './profile-overview.component.css'
})
export class ProfileOverviewComponent {
  employeeService=inject(EmployeeService);

  }
