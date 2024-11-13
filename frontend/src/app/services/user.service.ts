import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {UserDto} from '../Models/UserDto';
import {map} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }
  private http=inject(HttpClient);
  baseUrl:string="http://localhost:7000/api/";
  currentUser=signal<UserDto | null>(null)

  Login(model:any){
   return this.http.post<UserDto>(this.baseUrl+'user/login',model).pipe(
     map(user=>{
       if(user){
         localStorage.setItem('user',JSON.stringify(user));
         this.currentUser.set(user)
         console.log("Current user set:", this.currentUser());
         console.log("User service 1");
       }
       return user;
     })
   );
  }

  Register(model:any){
    return this.http.post(this.baseUrl+'user/register',model);
  }
}
