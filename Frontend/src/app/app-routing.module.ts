import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ListPersonComponent } from './components/person-crud/list-person/list-person.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from "./guards/auth.guard";
import { AddPersonComponent } from './components/person-crud/add-person/add-person.component';
import { EditPersonComponent } from './components/person-crud/edit-person/edit-person.component';
import { ListCategoryComponent } from './components/category-crud/list-category/list-category.component';
import { AddCategoryComponent } from './components/category-crud/add-category/add-category.component';
import { EditCategoryComponent } from './components/category-crud/edit-category/edit-category.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'people', component: ListPersonComponent, canActivate: [AuthGuard]},
  {path: 'people/add', component: AddPersonComponent, canActivate: [AuthGuard]},
  {path: 'people/edit/:id', component: EditPersonComponent, canActivate: [AuthGuard]},
  {path: 'categories', component: ListCategoryComponent, canActivate: [AuthGuard]},
  {path: 'categories/add', component: AddCategoryComponent, canActivate: [AuthGuard]},
  {path: 'categories/edit/:id', component: EditCategoryComponent, canActivate: [AuthGuard]},
  {path: '', redirectTo: 'people', pathMatch: 'full'},
  {path: '**', redirectTo: 'people'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
