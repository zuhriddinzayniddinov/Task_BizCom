import { Component } from '@angular/core';
import { Science } from './Science';
import { Subscription } from 'rxjs';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-science',
  templateUrl: './science.component.html',
  styleUrls: ['./science.component.css']
})
export class ScienceComponent {
  science = new Science();
  subscription = new Subscription();
  constructor(public apiSvc: ApiService,private router: Router) {

  }

  post() {
    if (this.science.id)
      this.apiSvc.putScience(this.science);
    else
      this.apiSvc.postScience(this.science);
    this.resetScience();
  }

  resetScience() {
    this.science = new Science();
  }

  ngOnInit(){
    this.subscription = this.apiSvc.getSelectedScience().subscribe( q => {
        this.science = q;
    });
}

ngOnDestroy() {
    this.subscription.unsubscribe();
}
}
