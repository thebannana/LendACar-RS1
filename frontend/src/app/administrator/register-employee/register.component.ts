import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {CityService} from '../../services/city.service';
import {EmployeeService} from '../../services/employee.service';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-register-employee',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {

  employee: any = {};
  cities: any;
  cityService=inject(CityService);
  employeeService=inject(EmployeeService);


  ngOnInit(): void {
     this.loadCities();
  }

  loadCities(){
    this.cityService.GetAllCities().subscribe(res=>{
      this.cities = res;
    })
  }

  generateRandomString(length:number):string{
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$%&';
    let result = '';
    const charactersLength = characters.length;

    for (let i = 0; i < length; i++) {
      const randomIndex = Math.floor(Math.random() * charactersLength);
      result += characters[randomIndex];
    }

    return result;
  }

  formatEmail(email:string):string{
    return email
      .replace(/š/g, 's')
      .replace(/đ/g, 'd')
      .replace(/ć/g, 'c')
      .replace(/č/g, 'c')
      .replace(/ž/g, 'z')
      .replace(/dž/g, 'dz');
  }

  generateEmail(): void {
    if (this.employee.firstName && this.employee.lastName) {
      const FirstName = this.employee.firstName.trim().toLowerCase();
      const LastName = this.employee.lastName.trim().toLowerCase();

      // Generate the email
      let email=`${FirstName}.${LastName}@lendacar.ba`;
      this.employee.emailAdress = this.formatEmail(email);
    }
  }

  compareDates(dateString:string):boolean{
    const currentDate = new Date();
    const parsedDate=new Date(dateString);
    console.log(parsedDate.getTime());
    console.log(currentDate.getTime())
    if(parsedDate.getTime() > currentDate.getTime()) return false;

    return true;
  }

  register(form: any) {
    if (form.valid && this.compareDates(this.employee.hireDate)) {
      this.employee.workingHourId=1;
      this.employee.password=this.generateRandomString(12);
      console.log('Employee Registered:', this.employee);
      this.employeeService.Register(this.employee).subscribe({
        next:result => {alert('Employee successfully registered!');}
      })
    } else {
      alert('Please fill in the form correctly.');
    }
  }
}
