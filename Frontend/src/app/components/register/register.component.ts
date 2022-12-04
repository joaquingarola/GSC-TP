import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  error : string = "";
  register : FormGroup;

  constructor(
    private builder : FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { 
    this.register = builder.group({
      UserName:['',Validators.required],
      Password:['',Validators.required]
    })
  }

  ngOnInit(): void { }

  onSubmit(): void {
    const { UserName, Password } = this.register.getRawValue();
    this.authService.register(UserName , Password).subscribe(
      () => {
        this.router.navigate(['/home']);
      },
      (response: HttpErrorResponse) => {
        this.error = response.error;
      }
    );
  }
}
