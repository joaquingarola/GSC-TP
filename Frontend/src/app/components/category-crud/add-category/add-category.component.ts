import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryCreationDto } from 'src/app/models/category/category-creation-dto'; 
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {
  register : FormGroup;
  error : string = "";

  constructor(
    private builder : FormBuilder, 
    private categoryService: CategoryService, 
    private router : Router
  ) { 
    this.register = builder.group({
      description: ['', [Validators.required]],
    })
  }

  ngOnInit(): void {
  }

  create()
  {
    const newCategory : CategoryCreationDto = this.register.getRawValue();
    this.categoryService.Create(newCategory).subscribe(
      () => {
        this.router.navigate(['/categories']);
      },
      (response: HttpErrorResponse) => {
        this.error = response.error;
      }
    );
  }
}
