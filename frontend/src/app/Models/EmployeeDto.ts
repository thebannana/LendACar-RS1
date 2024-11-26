import {City} from './City';
import {WorkingHour} from './WorkingHour';

export interface EmployeeDto {
  id:number;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  birthDate: string;
  cityId: number;
  city: City;
  emailAddress: string;
  username: string;
  workingHour:WorkingHour
  workingHourId:number
  jobTitle: string;
}
