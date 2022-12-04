import { Component, OnInit } from '@angular/core';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  isLoged = false;

  constructor(private storageService: StorageService,) { }

  ngOnInit(): void {
    this.isLoged = this.storageService.isLoggedIn();
  }

  logOut(){
    this.storageService.logOut();
    this.isLoged = false;
  }
}
