import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AdministratorDto} from '../Models/AdministratorDto';
import {map} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  constructor() { }

  private http=inject(HttpClient);
  baseUrl:string="http://localhost:7000/api/";
  currentAdmin=signal<AdministratorDto | null>(null)

  Login(model:any){
    return this.http.post<AdministratorDto>(this.baseUrl+'admin/admin-login',model).pipe(
      map(administrator=>{
        if(administrator){
          localStorage.setItem('administrator',JSON.stringify(administrator));
          this.currentAdmin.set(administrator);
        }
        return administrator;
      })
    )
  }
}