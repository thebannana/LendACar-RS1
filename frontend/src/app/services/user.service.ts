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
    return this.http.post(this.baseUrl+`user/register/`,model);
  }

  Update(model:any){
    console.log(model);
    return this.http.put<UserDto>(this.baseUrl+`user/update/${model.id}`,model).pipe(
      map(user=>{
        if(user){
          localStorage.clear();
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user)
          console.log("Current user set:", this.currentUser());
        }
        return user;
      })
    );
  }

  Delete(model:any){
    return this.http.delete<string>(this.baseUrl+`user/remove/${model.id}`)
  }

  // Getter to access current user
  getCurrentUser() {
    return this.currentUser();
  }
}
