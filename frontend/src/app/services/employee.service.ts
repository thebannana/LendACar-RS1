import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EmployeeDto} from '../Models/EmployeeDto';
import {map} from 'rxjs';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  constructor() { }

  private http=inject(HttpClient);
  baseUrl:string="http://localhost:7000/api/";
  currentEmployee=signal<EmployeeDto | null>(null);
  private router=inject(Router);

  Login(model:any){
    return this.http.post<EmployeeDto>(this.baseUrl+'employee/login',model).pipe(
      map(employee=>{
        if(employee){
          localStorage.setItem('employee',JSON.stringify(employee));
          this.currentEmployee.set(employee);
        }
        return employee;
      })
    )
  }

  Update(model:any){
    return this.http.put<EmployeeDto>(this.baseUrl+`employee/update/${model.id}`,model).pipe(
      map(employee=>{
        if(employee){
          localStorage.setItem('employee',JSON.stringify(employee));
          this.currentEmployee.set(employee);
        }
        return employee;
      })
    )
  }

  ChangePassword(model:any){
    return this.http.put<any>(this.baseUrl+'resetpassword/change',model).subscribe({
      next:()=>{
        alert("Password change successful");
        localStorage.removeItem('employee');
        void this.router.navigateByUrl("/employee/login");
        this.currentEmployee.set(null);
      },
      error:error=>{
        alert(error.error);
        console.log(error)
      }
      }
    )
  }


}
