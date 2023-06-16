import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';
import { SearchAgeDto } from './SearchAgeDto';
import { SearchBirthdayDateDto } from './SearchBirthdayDateDto';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent {
  roles = [{val:'Student',valu:0},{val:'Teacher',valu:1},{val:'Admin',valu:2}]
  uof = [{val:'dan katta',valu:"form"},{val:'gacha',valu:"under"}]
  phoneCompony = 
  [{val:'Humans',valu:33},
  {val:'Ucell',valu:93},
  {val:'Uzmobile',valu:95},
  {val:'MobiUz',valu:88},
  {val:'Beeline',valu:90}]
  users: any;
  name  = '';
  phone = 0;
  searchAgeDto = new SearchAgeDto();
  searchBirthdayDateDto = new SearchBirthdayDateDto();

  constructor(private apiSvc: ApiService,private router :Router){}


  ngOnInit(){
    // this.getAllUsers();
  }

  getAllUsers(){
    this.apiSvc.getAllUsers().subscribe(result=>{
      this.users = result;
    });
  }
  NameSearch(){
    this.apiSvc.getByNameUsers(this.name).subscribe(result=>{
      this.users = result;
    });
  }

  PhoneSearch(){    
    this.apiSvc.getByPhoneNumberUsers(this.phone).subscribe(result=>{
      this.users = result;
    });
  }

  birthdaySearch(){
    console.log(this.searchBirthdayDateDto);
    
    this.apiSvc.getByBirthdayUsers(this.searchBirthdayDateDto).subscribe(result=>{
      this.users = result;
    });
  }

  AgeSearch(){    
    this.apiSvc.getByAgeUsers(this.searchAgeDto).subscribe(result=>{
      this.users = result;
    });
  }

  navigateToSelectedUser(userid:Number){
    this.router.navigate(['/admin/selected/'+userid]);
  }
}
