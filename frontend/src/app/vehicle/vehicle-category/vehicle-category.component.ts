import { Component } from '@angular/core';

@Component({
  selector: 'app-vehicle-categories',
  templateUrl: './vehicle-category.component.html',
  styleUrls: ['./vehicle-category.component.css']
})
export class VehicleCategoriesComponent {
  vehicleCategories = [
    { name: 'Sedan', description: 'A comfortable sedan.', icon: 'assets/sedan-icon.png' },
    { name: 'Wagon', description: 'A spacious wagon.', icon: 'assets/wagon-icon.png' }
  ];

  newCategory = {
    name: '',
    description: '',
    icon: ''
  };

  addCategory() {
    if (this.newCategory.name && this.newCategory.description && this.newCategory.icon) {
      this.vehicleCategories.push({ ...this.newCategory });
      this.newCategory = { name: '', description: '', icon: '' };  // Reset the form
    }
  }

  editCategory(index: number) {
    const category = this.vehicleCategories[index];
    this.newCategory = { ...category };  // Populate the form with the existing category data
  }

  deleteCategory(index: number) {
    if (confirm('Are you sure you want to delete this category?')) {
      this.vehicleCategories.splice(index, 1);
    }
  }

  onIconChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.newCategory.icon = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }
}