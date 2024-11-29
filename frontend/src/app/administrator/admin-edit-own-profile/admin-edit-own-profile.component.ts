import {Component, inject, OnInit} from '@angular/core';
import {City} from '../../Models/City';
import {CityService} from '../../services/city.service';
import {AdminService} from '../../services/administrator.service';


@Component({
  selector: 'app-admin-edit-own-profile',
  templateUrl: './admin-edit-own-profile.component.html',
  styleUrl: './admin-edit-own-profile.component.css'
})
export class AdminEditOwnProfileComponent implements OnInit{
  cities:City[]=[];
  model:any={};
  cityService=inject(CityService);
  adminService=inject(AdminService);

    ngOnInit(): void {
      this.cityService.GetAllCities().subscribe(res=>{
        this.cities = res;
        console.log(this.cities);
        this.model = this.adminService.currentAdmin();
      })
    }

    Update() {
      console.log("Ovo prije update");
      console.log(this.model);
      this.adminService.Update(this.model).subscribe({
        next: result => {console.log("Ovo je poslije"); console.log(result); window.location.reload();},
        error: err => console.log(err)
      })
    }
}
