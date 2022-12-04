import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/models/category/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {
  editForm : FormGroup;
  categoryToUpdate! : Category;
  error : string = "";

  constructor(
    private builder : FormBuilder, 
    private categoryService : CategoryService, 
    private router : Router, 
    private activateRouter : ActivatedRoute) 
    { 
      this.editForm = builder.group({
        description:['', [Validators.required]],
      })
    }

  ngOnInit(): void {
    const id = this.activateRouter.snapshot.paramMap.get("id")!;
    this.categoryService.getById(id).subscribe(
      res=>{this.categoryToUpdate=res;
            this.editForm.patchValue(this.categoryToUpdate);}
    );
  }

  update()
  {
    const updatedCategory : Category = { id: this.categoryToUpdate.id, ...this.editForm.getRawValue() } ;
    this.categoryService.EditCategory(updatedCategory).subscribe(
      () => {
        this.router.navigate(['/categories']);
      },
      (response: HttpErrorResponse) => {
        this.error = response.error;
      }
    );
  }
}
