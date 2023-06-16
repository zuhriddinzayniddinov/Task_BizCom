import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  bott = false;
  role : string | null = '';
  constructor(public auth:AuthService)
  {
    this.role = auth.role;
  }

  ngOnInit() {
    this.role = this.auth.role;
  }


  logout() {
    this.auth.logout();
    this.role = this.auth.role;
  }

  bottActive(){
    this.bott = !this.bott;
  }
}
