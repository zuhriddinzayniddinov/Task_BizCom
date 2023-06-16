import { Component } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-student-of-sciences',
  templateUrl: './student-of-sciences.component.html',
  styleUrls: ['./student-of-sciences.component.css']
})
export class StudentOfSciencesComponent {
  studentOfScience: any;

  constructor(private apiSvc: ApiService){}


  ngOnInit(){
     this.apiSvc.GetAllUserOsScience().subscribe(res=>{
      this.studentOfScience = res;
     });
  }

}
