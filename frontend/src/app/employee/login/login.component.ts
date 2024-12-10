import {Component, inject, OnInit} from '@angular/core';
import {EmployeeService} from '../../services/employee.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{

  ngOnInit(): void {
      let employee=localStorage.getItem('employee');
      if(employee){
        this.router.navigateByUrl('/employee/dashboard');
      }
  }
  passwordShow:boolean = false;

  employeeService=inject(EmployeeService);
  model:any={};
  router=inject(Router);

  login(){
    this.employeeService.Login(this.model).subscribe({
      next:res=>{
        alert("User login successfully");
        console.log(res);
        void this.router.navigateByUrl('/employee/dashboard');
      },
      error:err=>{
        alert("Invalid login credentials");
        console.log(err);
      }
    })
  }



}
