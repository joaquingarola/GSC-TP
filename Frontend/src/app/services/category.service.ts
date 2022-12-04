import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category/category';
import { CategoryCreationDto } from '../models/category/category-creation-dto';

const API_URL = 'https://localhost:7026/api/category/';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Category[]> {
    return this.http.get<Category[]>(API_URL);
  }

  getById(id: string): Observable<Category> {
    return this.http.get<Category>(`${API_URL}${id}`);
  }

  Create(newCategory: CategoryCreationDto){
    return this.http.post(API_URL, newCategory);
  }

  DeleteCategory(id: string){
    return this.http.delete<{ msg: string }>(`${API_URL}${id}`);
  }

  EditCategory(cat: Category){
    return this.http.put(`${API_URL}${cat.id}`, cat);
  }
}
