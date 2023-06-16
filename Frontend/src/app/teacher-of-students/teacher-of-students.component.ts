import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-teacher-of-students',
  templateUrl: './teacher-of-students.component.html',
  styleUrls: ['./teacher-of-students.component.css']
})
export class TeacherOfStudentsComponent {
  students :any;

  constructor(private apiSvc: ApiService,private route:ActivatedRoute){}


  ngOnInit(){
   let scienceId = Number(this.route.snapshot.paramMap.get("scienceId"));
     this.apiSvc.GetStudentsByScienceId(scienceId).subscribe(res=>{
      this.students = res;      
     });
  }

  UpdateBall(sscience:any){
    this.apiSvc.UpdateStudentOfScience(sscience).subscribe(res=>{
      console.log(res);      
    });
  }
}
