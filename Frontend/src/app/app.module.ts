import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ApiService } from './api.service';
import { MatListModule } from '@angular/material/list';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import {MatSelectModule} from '@angular/material/select'
import { AuthService } from './auth.service';
import { AuthInterceptorService } from './auth-interceptor.service';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatChipsModule} from '@angular/material/chips';
import {MatRadioModule} from '@angular/material/radio';
import {MatDialogModule} from '@angular/material/dialog';
import { NotFoundComponent } from './not-found/not-found.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { AdminUsersComponent } from './admin-users/admin-users.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { AdminSelectUserComponent } from './admin-select-user/admin-select-user.component';
import { SciencesComponent } from './sciences/sciences.component';
import { ScienceComponent } from './science/science.component';
import { StudentOfSciencesComponent } from './student-of-sciences/student-of-sciences.component';
import { TeacherOfSciencesComponent } from './teacher-of-sciences/teacher-of-sciences.component';
import { TeacherOfStudentsComponent } from './teacher-of-students/teacher-of-students.component';
let routes = [
  {path: 'admin/users',component : AdminUsersComponent},
  {path: 'admin/science',component : ScienceComponent},
  {path: 'sciences',component : SciencesComponent},
  {path: 'student',component : StudentOfSciencesComponent},
  {path: 'teacher',component : TeacherOfSciencesComponent},
  {path: 'teacher/students/:scienceId',component : TeacherOfStudentsComponent},
  {path: 'admin/selected/:userid',component : AdminSelectUserComponent},
  {path: 'admin/balls',component : AdminSelectUserComponent},
  {path: 'admin',component : AdminPanelComponent},
  {path: 'register', component :RegisterComponent},
  {path: 'login', component :LoginComponent},
  {path: '',component:HomeComponent},
  {path: '**',component:NotFoundComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    RegisterComponent,
    LoginComponent,
    NotFoundComponent,
    AdminPanelComponent,
    AdminUsersComponent,
    SpinnerComponent,
    AdminSelectUserComponent,
    SciencesComponent,
    ScienceComponent,
    StudentOfSciencesComponent,
    TeacherOfSciencesComponent,
    TeacherOfStudentsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatSlideToggleModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    MatListModule,
    FormsModule,
    RouterModule.forRoot(routes),
    MatToolbarModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatExpansionModule,
    MatChipsModule,
    MatRadioModule,
    MatDialogModule
  ],
  providers: [ApiService,AuthService,{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent],
})
export class AppModule { }
