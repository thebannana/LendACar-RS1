import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http: HttpClient) {}

  // Check if a company exists for the user
  checkIfCompanyExists(userId: number): Observable<boolean> {
    return this.http.get<boolean>(`http://localhost:7000/api/company/check/${userId}`);
  }

  // Get company details if needed
  getCompanyByUserId(userId: number): Observable<any> {
    return this.http.get<any>(`http://localhost:7000/api/company/get/${userId}`);
  }

// API call to create a company
  createCompany(companyData: any): Observable<any> {
    const formData = new FormData();

    // Append text fields
    formData.append('companyName', companyData.companyName);
    formData.append('companyPhone', companyData.companyPhone);
    formData.append('companyDescription', companyData.companyDescription);
    formData.append('companyEmail', companyData.companyEmail);
    formData.append('companyAddress', companyData.companyAddress);
    formData.append('userId', companyData.userId.toString());

    // Append the avatar image if it's provided
    if (companyData.companyAvatar) {
      formData.append('companyAvatar', companyData.companyAvatar, companyData.companyAvatar.name);
    }

    return this.http.post<any>(`http://localhost:7000/api/company/create`, formData);
  }

  deleteCompanyByUserId(userId: number): Observable<void> {
    return this.http.delete<void>(`http://localhost:7000/api/company/deleteByUser/${userId}`);
  }

  updateCompanyByUserId(userId: number, companyData: any): Observable<void> {
    const formData = new FormData();

    // Append text fields
    formData.append('companyName', companyData.companyName);
    formData.append('companyPhone', companyData.companyPhone);
    formData.append('companyDescription', companyData.companyDescription);
    formData.append('companyEmail', companyData.companyEmail);
    formData.append('companyAddress', companyData.companyAddress);
    formData.append('userId', companyData.userId.toString());

    // Append the avatar image if it's provided
    if (companyData.companyAvatar) {
      formData.append('companyAvatar', companyData.companyAvatar, companyData.companyAvatar.name);
    }

    return this.http.put<void>(`http://localhost:7000/api/company/updateByUser/${userId}`, formData);
  }
}
