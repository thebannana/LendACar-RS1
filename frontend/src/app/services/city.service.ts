import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {City} from '../Models/City';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor() { }
  private http=inject(HttpClient);
  baseUrl:string="http://localhost:7000/api/";

  GetAllCities(){
    return this.http.get<City[]>(this.baseUrl + 'city/get')
  }
}
