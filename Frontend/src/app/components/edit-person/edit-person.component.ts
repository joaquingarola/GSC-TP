import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Person from 'src/app/models/person';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-person',
  templateUrl: './edit-person.component.html',
  styleUrls: ['./edit-person.component.css']
})
export class EditPersonComponent implements OnInit {
  editForm : FormGroup;
  personToUpdate! : Person;
  error : string = "";

  constructor(
    private builder : FormBuilder, 
    private userService : UserService, 
    private router : Router, 
    private activateRouter : ActivatedRoute) 
    { 
      this.editForm = builder.group({
        name:['', [Validators.required]],
        phoneNumber:['', [Validators.pattern("[0-9]{6,12}")]],
        email:['', [Validators.email]],
      })
    }

  ngOnInit(): void {
    const id = this.activateRouter.snapshot.paramMap.get("id")!;
    this.userService.getById(id).subscribe(
      res=>{this.personToUpdate=res;
            this.editForm.patchValue(this.personToUpdate);}
    );
  }

  update()
  {
    const updatedPerson : Person = { id: this.personToUpdate.id, ...this.editForm.getRawValue() } ;
    this.userService.EditPerson(updatedPerson).subscribe(
      () => {
        this.router.navigate(['/home']);
      },
      (response: HttpErrorResponse) => {
        this.error = response.error;
      }
    );
  }
}
