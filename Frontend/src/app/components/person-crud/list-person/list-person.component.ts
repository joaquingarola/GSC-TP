import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import Person from '../../../models/person';
import { StorageService } from '../../../services/storage.service';
import { UserService } from '../../../services/user.service';
import { first } from "rxjs";


@Component({
  selector: 'app-home',
  templateUrl: './list-person.component.html',
  styleUrls: ['./list-person.component.css']
})
export class ListPersonComponent implements OnInit {
  displayedColumns : string[] = ["id", "name", "phoneNumber", "email", "options"];
	people: Person[] = [];
  error : string = "";

  constructor(
    private storageService: StorageService,
    private userService: UserService) 
    { }

  ngOnInit(): void {
    this.listPeople();
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
