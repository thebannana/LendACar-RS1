import {Component, inject, OnInit} from '@angular/core';
import {EmployeeDto} from '../../Models/EmployeeDto';
import {EmployeeService} from '../../services/employee.service';

@Component({
  selector: 'app-admin-employee-overview',
  templateUrl: './admin-employee-overview.component.html',
  styleUrl: './admin-employee-overview.component.css'
})
export class AdminEmployeeOverviewComponent implements OnInit {
  ngOnInit(): void {
    this.loadEmployees();
  }
  employees: EmployeeDto[] = [];
  employeeService=inject(EmployeeService);

  loadEmployees(){
    this.employeeService.GetAllEmployees().subscribe({
      next: data => this.employees = data
    })
  }

  DeleteEmployee($event: any) {
    console.log($event.target.value);
  }

  EditEmployee($event: any) {
    console.log($event.target.value);
  }
}
