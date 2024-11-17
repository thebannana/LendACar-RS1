import {Component, inject, model, OnInit} from '@angular/core';
import {UserService} from '../../services/user.service';
import {CityService} from '../../services/city.service';
import {City} from '../../Models/City';
import {Router} from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
    ngOnInit(): void {
      this.cityService.GetAllCities().subscribe(res=>{
        this.cities = res;

      })
    }

   model:any = {};
    cities:City[]=[];
  accountService=inject(UserService);
  cityService=inject(CityService);
  router=inject(Router);

  register() {
    console.log(this.model);
    this.accountService.Register(this.model).subscribe({
      next: result =>{
        alert("User created successfully");
        console.log(result)
        void this.router.navigateByUrl('/user/login');
      },
      error: err => console.log(err)
    })
  }
}
