import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { AuthService } from '../auth.service';
import { Science } from '../science/Science';

@Component({
  selector: 'app-sciences',
  templateUrl: './sciences.component.html',
  styleUrls: ['./sciences.component.css']
})
export class SciencesComponent {
  sciences: any;
  role: string | null = '';
  constructor(public apiSvc: ApiService, public auth:AuthService) {
    this.role = auth.role;
  }

  ngOnInit() {
    this.apiSvc.getSciences().subscribe(result => {
      this.sciences = result;
    });
    this.apiSvc.getNewScience().subscribe(newScience => {
      this.sciences.push(newScience);
    });
  }

  AddScience(science:Science){
    this.apiSvc.AddScience(science).subscribe(response =>{
      console.log(response);
    })
  }
}
