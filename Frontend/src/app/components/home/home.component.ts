import { Component, OnInit, SimpleChanges, OnChanges } from '@angular/core';
import Person from '../../models/person';
import { StorageService } from '../../services/storage.service';
import { UserService } from '../../services/user.service';
import { first } from "rxjs";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnChanges {
  isLoged = false;
  displayedColumns : string[] = ["id", "name", "phoneNumber", "email", "options"];
	people: Person[] = [];

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

  ngOnChanges(changes: SimpleChanges): void {
    console.log("Changes in ", changes);
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

  ReloadList(){
    this.listPeople();
  }

  delete(id:number)
  {
    this.userService.DeletePerson(id.toString())
    this.people = this.people.filter(person => person.id != id);
  }
}
