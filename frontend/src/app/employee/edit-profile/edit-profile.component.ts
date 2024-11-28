import {Component, inject, OnInit} from '@angular/core';
import {City} from '../../Models/City';
import {CityService} from '../../services/city.service';
import {EmployeeService} from '../../services/employee.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  cities:City[]=[];
  model:any={};
  cityService=inject(CityService);
  employeeService=inject(EmployeeService);

    ngOnInit(): void {
      this.cityService.GetAllCities().subscribe(res=>{
        this.cities = res;
        console.log(this.cities);
        this.model = this.employeeService.currentEmployee();
      })
    }

    Update(){
      this.employeeService.Update(this.model).subscribe({
        next:result => {console.log(result); window.location.reload();},
        error:err=>console.log(err)
      })
    }
}
