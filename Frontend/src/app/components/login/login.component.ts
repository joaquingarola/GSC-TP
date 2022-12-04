import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from "@angular/common/http";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { StorageService } from '../../services/storage.service';
import { Router  } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login : FormGroup;
  error : string = "";

  constructor(
    private authService: AuthService, 
    private storageService: StorageService, 
    private router: Router,
    private builder : FormBuilder,) 
    { 
      this.login = builder.group({
        UserName:['',Validators.required],
        Password:['',Validators.required]
      })
    }

  ngOnInit() {
  }

  onSubmit(): void {
    const { UserName, Password } = this.login.getRawValue();

    this.authService.login(UserName, Password).subscribe(
       (data : any) => {
        this.storageService.saveToken(data);
        this.router.navigate(['/home']);
      },
      (response: HttpErrorResponse) => {
        this.error = response.error;
      }
    );
  }
}
