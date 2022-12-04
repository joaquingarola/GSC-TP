import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from "@angular/material/table";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { ListPersonComponent } from './components/person-crud/list-person/list-person.component';

import { httpInterceptorProviders } from './interceptors/http-interceptor.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddPersonComponent } from './components/person-crud/add-person/add-person.component';
import { EditPersonComponent } from './components/person-crud/edit-person/edit-person.component';
import { NavComponent } from './components/nav/nav.component';
import { AddCategoryComponent } from './components/category-crud/add-category/add-category.component';
import { EditCategoryComponent } from './components/category-crud/edit-category/edit-category.component';
import { ListCategoryComponent } from './components/category-crud/list-category/list-category.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ListPersonComponent,
    AddPersonComponent,
    EditPersonComponent,
    NavComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    ListCategoryComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
