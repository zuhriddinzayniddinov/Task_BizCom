import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-teacher-of-sciences',
  templateUrl: './teacher-of-sciences.component.html',
  styleUrls: ['./teacher-of-sciences.component.css']
})
export class TeacherOfSciencesComponent {
  teacherOfScience: any;

  constructor(private apiSvc: ApiService,private route:Router){}


  ngOnInit(){
     this.apiSvc.GetAllUserOsScience().subscribe(res=>{
      this.teacherOfScience = res;      
     });
  }

  GetIStudent(scienceId : number){
    this.route.navigate(["teacher/students/"+scienceId])
  }
}
