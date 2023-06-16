import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private addUser = new Subject<User>();
  constructor(private http: HttpClient, private router: Router) { }

  login(credations: any) { 
      this.http.post<any>('https://localhost:44315/api/Account', credations)
      .subscribe(response => {
        localStorage.setItem('name', response.firstName);
        localStorage.setItem('role', response.role);
        localStorage.setItem('accessToken', response.accessToken);
        localStorage.setItem('expireDate', response.expireDate);
        if(response)
        this.router.navigate(['/']);
      });
  }

  get Name() {
    return localStorage.getItem('name');
  }

  getNewUser() {
    return this.addUser.asObservable();
  }

  addNewUser(user: User) {
    return this.addUser.next(user);
  }

  register(credations: User) {
    console.log(credations);
    
    this.http.post<any>('https://localhost:44315/api/Account/Register', credations)
      .subscribe(response => {
        this.addNewUser(response)
      });
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/']);
  }

  get role() {
    let userRole = localStorage.getItem('role');
    return userRole;
  }

  get isAuth() {
    return !!localStorage.getItem('accessToken');
  }
}
