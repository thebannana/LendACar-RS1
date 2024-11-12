import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  isFormVisible = false;
  companyName = '';
  address = '';
  companyPhone = '';
  companyEmail = '';
  companyAddress = '';
  avatarPreview: string | ArrayBuffer | null = null;

  showForm() {
    this.isFormVisible = true;
  }

  submitForm() {
    // Handle form submission here, e.g., save data or call an API
    console.log("Company Name:", this.companyName);
    console.log("Address:", this.address);

    // Hide form and reset fields after submission
    this.isFormVisible = false;
    this.resetForm();
  }

  private resetForm() {
    this.companyName = '';
    this.address = '';
  }

  cancelForm() {
    // Hide form without submitting
    this.isFormVisible = false;
    this.resetForm();
  }

  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files[0]) {
      const file = fileInput.files[0];
      const reader = new FileReader();

      reader.onload = () => {
        this.avatarPreview = reader.result; // Set the preview image
      };

      reader.readAsDataURL(file); // Read the file as a data URL
    }
  }

  onPdfSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files[0]) {
      const file = fileInput.files[0];
      console.log('Selected PDF:', file);
      // Handle the selected PDF (e.g., upload)
    }
  }
}
