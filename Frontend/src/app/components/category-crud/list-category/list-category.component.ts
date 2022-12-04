import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import { Category } from 'src/app/models/category/category';
import { StorageService } from '../../../services/storage.service';
import { CategoryService } from '../../../services/category.service';
import { first } from "rxjs";

@Component({
  selector: 'app-list-category',
  templateUrl: './list-category.component.html',
  styleUrls: ['./list-category.component.css']
})
export class ListCategoryComponent implements OnInit {
  displayedColumns : string[] = ["id", "description", "options"];
	categories: Category[] = [];
  error : string = "";

  constructor(
    private storageService: StorageService,
    private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.listCategories();
  }

  listCategories()
  {
    this.categoryService.getAll()
    .pipe(first())
    .subscribe(
      (data) => {
        this.categories = data as Category[];
      });
  }

  delete(id:number)
  {
    this.categoryService.DeleteCategory(id.toString()).subscribe(
      (data : any) => {
        this.listCategories();
      },
      (response: HttpErrorResponse) => {
        this.error = response.error;
      }
    );
  }
}
