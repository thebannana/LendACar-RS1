import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Define the interface for the Employee data
interface Employee {
  userId: number;
  companyAdminEmail: string;
  companyPositionId: number;
  workingHourId: number;
  companyId: number;
}

@Injectable({
  providedIn: 'root'
})
export class CompanyEmployeeService {
  constructor(private http: HttpClient) {}

  // POST method to add an employee
  addEmployee(employee: Employee): Observable<any> {
    return this.http.post<any>(`http://localhost:7000/api/CompanyEmployee/create`, employee);
  }
}
