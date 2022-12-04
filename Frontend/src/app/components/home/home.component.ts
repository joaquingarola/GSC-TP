import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import Person from '../../models/person';
import { StorageService } from '../../services/storage.service';
import { UserService } from '../../services/user.service';
import { first } from "rxjs";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isLoged = false;
  displayedColumns : string[] = ["id", "name", "phoneNumber", "email", "options"];
	people: Person[] = [];
  error : string = "";

  constructor(
    private storageService: StorageService,
    private userService: UserService
    ) 
    { }

  ngOnInit(): void {
    this.isLoged = this.storageService.isLoggedIn();
    this.listPeople();
  }

  logOut(){
    this.storageService.logOut();
    this.isLoged = false;
  }

  listPeople()
  {
    this.userService.getAll()
    .pipe(first())
    .subscribe(
      (data) => {
        this.people = data as Person[];
      });
  }

  delete(id:number)
  {
    this.userService.DeletePerson(id.toString()).subscribe(
      () => {
        this.listPeople();
      },
      (response: HttpErrorResponse) => {
        this.error = response.error;
      }
    );
  }
}
