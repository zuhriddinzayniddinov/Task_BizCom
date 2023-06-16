import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { User } from './user';
import { SearchAgeDto } from './admin-users/SearchAgeDto';
import { SearchBirthdayDateDto } from './admin-users/SearchBirthdayDateDto';
import { Science } from './science/Science';
import { Subject } from 'rxjs';

@Injectable()
export class ApiService {
    private selectedScience = new Subject<Science>();
    private addScience = new Subject<Science>();
    constructor(private http: HttpClient) { }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - -*/ 

    AddScience(science: Science){
        return this.http.post('https://localhost:44315/api/UserOfSciences', science.id);
    }

    GetAllUserOsScience(){
        return this.http.get('https://localhost:44315/api/UserOfSciences');
    }

    GetStudentsByScienceId(scienceId:number){
        return this.http.get('https://localhost:44315/api/UserOfSciences/'+scienceId);
    }

    UpdateStudentOfScience(studentOfScience:any){
        return this.http.put('https://localhost:44315/api/UserOfSciences',studentOfScience);
    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - -*/ 

    selectScience(science: Science) {
        this.selectedScience.next(science);
    }

    getSciences() {
        return this.http.get('https://localhost:44315/api/Science');
    }

    putScience(science: Science) {
        this.http.put('https://localhost:44315/api/Science/' + science.id, science)
            .subscribe(response => {
                console.log(response);
            });
    }
    
    postScience(science: Science) {
        this.http.post('https://localhost:44315/api/Science', science)
            .subscribe(response => {
                this.addNewScience(response as Science);
                console.log(response);
            });
    }

    getSelectedScience() {
        return this.selectedScience.asObservable();
    }

    getNewScience() {
        return this.addScience.asObservable();
    }

    addNewScience(science: Science) {
        return this.addScience.next(science);
    }

  /* - - - - - - - - - - - - - - - - - - - - - - - - - - - -*/  
    getAllUsers() {
        return this.http.get('https://localhost:44315/api/Admin/users');
    }

    GetScienceByMaxBall(search:any){
        return this.http.get('https://localhost:44315/api/Admin/science/'+search.userId+'/search?minBall='+search.minBall+'&minCount='+search.minCount);
    }

    GetScienceByNormalBall() {
        return this.http.get('https://localhost:44315/api/Admin/science/normal');
    }

    GetScienceByTemStudentsMaxBall(userId:number){
        return this.http.get('https://localhost:44315/api/Admin/science/'+userId);
    }

    getByNameUsers(name : string){
        return this.http.get('https://localhost:44315/api/Admin/users/name?name='+name);
    }

    getByPhoneNumberUsers(phone : Number){        
        return this.http.get('https://localhost:44315/api/Admin/users/phone?phoneCompony='+phone);
    }

    getByAgeUsers(searchAgeDto : SearchAgeDto){
        return this.http.get('https://localhost:44315/api/Admin/users/age?age='+searchAgeDto.age+"&role="+searchAgeDto.role+"&underOrForm="+searchAgeDto.underOrForm);
    }

    getByBirthdayUsers(searchBirthdayDateDto : SearchBirthdayDateDto){
        return this.http.get('https://localhost:44315/api/Admin/users/birthday?startDate='+ searchBirthdayDateDto.startDate +'&endDate='+searchBirthdayDateDto.endDate);
    }

    putUser(userMorify: User) {
        this.http.put('https://localhost:44315/api/Admin', userMorify)
            .subscribe(response => {
                console.log(response);
            });
    }

    getByIdUser(userId: Number) {
        return this.http.get('https://localhost:44315/api/Admin/' + userId);
    }

}