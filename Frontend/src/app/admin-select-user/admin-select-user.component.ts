import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { ActivatedRoute } from '@angular/router';
import { Search } from './Search';

@Component({
  selector: 'app-admin-select-user',
  templateUrl: './admin-select-user.component.html',
  styleUrls: ['./admin-select-user.component.css']
})
export class AdminSelectUserComponent {
  roles = [{val:'Student',valu:0},{val:'Teacher',valu:1},{val:'Admin',valu:2}];
  user: any;
  science:any;
  ball = false;
  search = new Search();
  
  constructor(private apiSvc: ApiService,private router:ActivatedRoute){
    
  }
  ngOnInit(){
    let userId = Number(this.router.snapshot.paramMap.get("userid"));
    if(userId){
      this.search.userId = userId;
      this.apiSvc.getByIdUser(userId).subscribe(res =>{
        this.user = res;
      })
    }
    else
    this.ball = true;
  }

  GetScienceByMaxBall(){
    this.apiSvc.GetScienceByMaxBall(this.search).subscribe(res=>{
      this.science=res;
    })
  }
  GetScienceByTemStudentsMaxBall(userId:number){
    this.apiSvc.GetScienceByTemStudentsMaxBall(userId).subscribe(x=>{
      this.science = x;
    })
  }
  GetScienceByNormalBall(){
    this.apiSvc.GetScienceByNormalBall().subscribe(res =>{
      this.science = res;
      console.log(res);
      
    })
  }
}
