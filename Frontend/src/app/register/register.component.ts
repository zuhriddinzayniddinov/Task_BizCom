import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormBuilder, Validators } from '@angular/forms';
import { User } from '../user';
import { Router } from '@angular/router';
import { NotificationService } from '../notification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  chek = false;
  registerForm;
  user: any;
  roles = [{val:'Student',valu:0},{val:'Teacher',valu:1}]
  constructor(private fb: FormBuilder,private auth:AuthService,private router:Router,private noti:NotificationService) {
    this.registerForm = this.fb.group({
      firstName: ['',Validators.required],
      lastName: ['',Validators.required],
      phoneNumber: ['',Validators.required],
      email: ['',Validators.required],
      birthday: ['',Validators.required],
      password: ['',Validators.required],
      role: [0,Validators.required],
    });
  }

  register() {
    this.chek = true;
    this.auth.register(this.registerForm.value as User);
    this.auth.getNewUser().subscribe(us => {
      this.user = us;
      if (this.user) {
        this.noti.showNotification("Muvaffaqiyatli ro'yxatdan o'tildi.");
        this.router.navigate(['login']);
      }
    }); 
  }
}
