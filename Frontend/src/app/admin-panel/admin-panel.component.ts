import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent {

  constructor(private route:Router){}

  navigateUsers(){
    this.route.navigate(["/admin/users"])
  }

  navigateBalls(){
    this.route.navigate(["admin/balls"])
  }
}
