import {Component, inject, OnInit} from '@angular/core';
import {UserService} from '../../services/user.service';
import {CityService} from '../../services/city.service';
import {City} from '../../Models/City';
import {UserDto} from '../../Models/UserDto';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  cities:City[]=[];
  model:any={};
      ngOnInit(): void {
        this.cityService.GetAllCities().subscribe(res=>{
          this.cities = res;
          console.log(this.cities);
          this.model = this.accountService.currentUser();
        })
      }
      accountService=inject(UserService);
      cityService=inject(CityService);

  Update() {
    console.log("Ovo prije update");
    console.log(this.model);
    this.accountService.Update(this.model).subscribe({
      next: result => {console.log("Ovo je poslije"); console.log(result)},
      error: err => console.log(err)
    })
  }

  Delete() {
    this.accountService.Delete(this.model).subscribe({
      next: result => {
        console.log(result);
        this.accountService.currentUser.set(null);
        localStorage.removeItem('user');
      },
      error: err => {
        console.log(err);
        this.accountService.currentUser.set(null);
        localStorage.removeItem('user');
      }
    })
  }
}
