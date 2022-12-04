import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PersonCreationDTO } from 'src/app/models/person-creation-dto';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add-person',
  templateUrl: './add-person.component.html',
  styleUrls: ['./add-person.component.css']
})
export class AddPersonComponent implements OnInit {
  register : FormGroup;
  error : string = "";

  constructor(
    private builder : FormBuilder, 
    private userService: UserService, 
    private router : Router) 
    { 
      this.register = builder.group({
        name: ['', [Validators.required]],
        phoneNumber: ['', [Validators.pattern("[0-9]{6,12}")]],
        email: ['', [Validators.email]],
      })
    }

  ngOnInit(): void {
  }

  create()
  {
    const newPerson : PersonCreationDTO = this.register.getRawValue();
    this.userService.Create(newPerson).subscribe(
      () => {
        this.router.navigate(['/home']);
      },
      (response: HttpErrorResponse) => {
        this.error = response.error;
      }
    );
  }
}
