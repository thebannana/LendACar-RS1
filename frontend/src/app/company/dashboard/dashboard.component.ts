// dashboard.component.ts
import { Component, inject, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import {CompanyService} from '../../services/company.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  isFormVisible = false;
  companyName = '';
  address = '';
  companyPhone = '';
  companyEmail = '';
  companyAddress = '';
  avatarPreview: string | ArrayBuffer | null = null;
  activeLink: string = 'home';
  companyDescription: string = '';
  accountService = inject(UserService); // Inject UserService
  username: string = '';
  isLoggedIn: boolean = false;
  hasCompany: boolean = false;  // Flag to check if the user has a company
  userId: number = 0;
  updateFormVisible = false; // Visibility toggle for the update form
  selectedFileName: any;

  employees = [
    {
      employeeId: 1,
      lastName: 'Davolio',
      firstName: 'Nancy',
      title: 'Sales Representative',
      titleOfCourtesy: 'Ms.',
      birthDate: '1948-12-08',
      hireDate: '1992-05-01',
      address: '507 - 20th'
    },
    {
      employeeId: 2,
      lastName: 'Fuller',
      firstName: 'Andrew',
      title: 'Vice President, Sales',
      titleOfCourtesy: 'Dr.',
      birthDate: '1952-02-19',
      hireDate: '1992-08-14',
      address: '908 W. 1st'
    },
    {
      employeeId: 3,
      lastName: 'Leverling',
      firstName: 'Janet',
      title: 'Sales Representative',
      titleOfCourtesy: 'Ms.',
      birthDate: '1963-08-30',
      hireDate: '1992-04-01',
      address: '678 - 12th'
    },
    {
      employeeId: 4,
      lastName: 'Peacock',
      firstName: 'Margaret',
      title: 'Sales Representative',
      titleOfCourtesy: 'Mrs.',
      birthDate: '1950-09-19',
      hireDate: '1993-05-22',
      address: '234 - 7th'
    },
    {
      employeeId: 5,
      lastName: 'Buchanan',
      firstName: 'Steven',
      title: 'Sales Manager',
      titleOfCourtesy: 'Mr.',
      birthDate: '1965-03-13',
      hireDate: '1993-10-17',
      address: '1122 - 5th'
    },
    {
      employeeId: 6,
      lastName: 'Suyama',
      firstName: 'Michael',
      title: 'Sales Representative',
      titleOfCourtesy: 'Mr.',
      birthDate: '1967-07-02',
      hireDate: '1993-07-06',
      address: '764 - 8th'
    }
  ];


  constructor(private userService: UserService, private companyService: CompanyService) {
  }

  ngOnInit(): void {
    const currentUser = this.userService.getCurrentUser();  // Make sure this correctly returns a user object if logged in
    this.isLoggedIn = !!currentUser;  // Ensures it's true if a user is found, false otherwise
    if (currentUser) {
      this.username = currentUser.username;
      this.userId = currentUser.id;
      // Fetch company data when the component is initialized
      this.companyService.getCompanyByUserId(this.userId).subscribe((company) => {
        // Populate form with fetched company data
        this.companyName = company.companyName;
        this.companyPhone = company.companyPhone;
        this.companyEmail = company.companyEmail;
        this.companyDescription = company.companyDescription;
        this.address = company.companyAddress;
      });

      console.log('isLoggedIn:', this.isLoggedIn);
      console.log('hasCompany:', this.hasCompany);

      // Call the service to check if the user has a company
      this.companyService.checkIfCompanyExists(this.userId).subscribe(
        (exists: boolean) => {
          this.hasCompany = exists;  // Set the flag based on response
        },
        (error: any) => {
          console.error('Error checking if company exists:', error);
          this.hasCompany = false;  // Default to false if there's an error
        }
      );
    } else {
      console.log('No user logged in');  // Log if there's no user
    }
  }


  // Method to handle update logic
  updateEmployee(employee: any) {
    console.log('Update employee:', employee);
  }

  // Method to handle delete logic
  deleteEmployee(employeeId: number) {
    this.employees = this.employees.filter(emp => emp.employeeId !== employeeId);
    console.log('Deleted employee with ID:', employeeId);
  }

  // Method to handle add logic
  addEmployee() {
  }

  // Show the form
  showForm() {
    this.isFormVisible = true;
  }

  // Submit the company creation form
  submitForm() {
    // Log each field
    console.log("Company Name:", this.companyName);
    console.log("Address:", this.address);
    console.log("Phone:", this.companyPhone);
    console.log("Email:", this.companyEmail);
    console.log("Description:", this.companyDescription);
    console.log("Avatar Preview:", this.avatarPreview);

    const companyData = {
      companyName: this.companyName,
      address: this.address,
      companyPhone: this.companyPhone,
      companyEmail: this.companyEmail,
      companyDescription: this.companyDescription,
      avatar: this.avatarPreview,
      userId: this.userId,
    };

    this.companyService.createCompany(companyData).subscribe(
      (response) => {
        console.log('Company created successfully:', response);
        this.isFormVisible = false;
        this.resetForm();
        window.location.reload(); // Refresh the page after success
      },
      (error) => {
        console.error('Error creating company:', error);
      }
    );
  }

  // Reset the form fields after submission and log each reset action
  private resetForm() {
    console.log("Resetting Company Name");
    this.companyName = '';
    console.log("Resetting Address");
    this.address = '';
    console.log("Resetting Phone");
    this.companyPhone = '';
    console.log("Resetting Email");
    this.companyEmail = '';
    console.log("Resetting Description");
    this.companyDescription = '';
    console.log("Resetting Avatar Preview");
    this.avatarPreview = null;
  }

  // Cancel the form without saving
  cancelForm() {
    this.isFormVisible = false;
    this.resetForm();
  }

  // Handle file selection for avatar
  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files[0]) {
      const file = fileInput.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.avatarPreview = reader.result;
      };
      reader.readAsDataURL(file);
    }
  }

  // Handle PDF selection
  onPdfSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files[0]) {
      const file = fileInput.files[0];
      console.log('Selected PDF:', file);
    }
  }

  // Set active link (navigation)
  setActive(link: string) {
    this.activeLink = link;
  }

  deleteCompany(): void {
    const isConfirmed = window.confirm('Are you sure you want to delete the company?');

    if(isConfirmed){
    if (!this.userId) {
      console.error('User not logged in.');
      return;
    }

    // Call the service to delete the company
    this.companyService.deleteCompanyByUserId(this.userId).subscribe(
      () => {
        console.log('Company deleted successfully.');
        this.hasCompany = false; // Update the UI state to reflect the absence of a company
        window.location.reload(); // Refresh the page
      },
      (error) => {
        console.error('Error deleting company:', error);
      }
    );
    }
  }

  editCompany(): void {
    if (!this.userId) {
      console.error('User not logged in.');
      return;
    }

    // Fetch the company details to pre-fill the form
    this.companyService.getCompanyByUserId(this.userId).subscribe(
      (company) => {
        this.companyName = company.companyName;
        this.companyPhone = company.companyPhone;
        this.companyEmail = company.companyEmail;
        this.companyDescription = company.companyDescription;
        this.address = company.companyAddress;
        this.avatarPreview = company.companyAvatar ? `data:image/png;base64,${company.companyAvatar}` : null;
        this.updateFormVisible = true; // Show the update form
      },
      (error) => {
        console.error('Error fetching company details:', error);
      }
    );
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFileName = file;
    }
  }

  updateCompany(): void {
    const isConfirmed = window.confirm('Are you sure you want to update the company?');
    if(isConfirmed) {
      const updatedCompanyData = {
        companyName: this.companyName,
        companyPhone: this.companyPhone,
        companyEmail: this.companyEmail,
        companyDescription: this.companyDescription,
        companyAddress: this.address,
        userId: this.userId, // Use UserId to identify the company
        companyAvatar: this.selectedFileName // Attach the selected file here
      };

      this.companyService.updateCompanyByUserId(this.userId, updatedCompanyData).subscribe(
        () => {
          console.log('Company updated successfully.');
          this.updateFormVisible = false; // Hide the update form
          window.location.reload(); // Refresh the page
        },
        (error) => {
          console.error('Error updating company:', error);
          if (error.status === 400 && error.error && error.error.errors) {
            console.log('Validation errors:', error.error.errors);
            Object.entries(error.error.errors).forEach(([field, messages]) => {
              window.alert(`All fields are required. The field ${field} is empty`);
              console.error(`Validation error on ${field}:`, messages);
            });
          }
        }
      );
    }
  }


  showUpdateForm() {
    this.updateFormVisible = true;
  }
}
