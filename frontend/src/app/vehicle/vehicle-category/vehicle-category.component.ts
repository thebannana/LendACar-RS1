import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-vehicle-category',
  templateUrl: './vehicle-category.component.html',
  styleUrl: './vehicle-category.component.css'
})
export class CategoryFormComponent {
  categoryForm: FormGroup;
  selectedFile: File | null = null;

  constructor(private fb: FormBuilder) {
    this.categoryForm = this.fb.group({
      categoryName: ['', Validators.required],
      categoryDescription: [''],
      categoryIcon: [null, Validators.required]
    });
  }

  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files[0]) {
      this.selectedFile = fileInput.files[0];
    }
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const formData = new FormData();
      formData.append('categoryName', this.categoryForm.get('categoryName')?.value);
      formData.append('categoryDescription', this.categoryForm.get('categoryDescription')?.value);
      if (this.selectedFile) {
        formData.append('categoryIcon', this.selectedFile, this.selectedFile.name);
      }

      // Here you would send the formData to your backend service
      console.log('Form submitted!', formData);
      // e.g., this.yourService.uploadCategory(formData).subscribe(...);
    }
  }
}