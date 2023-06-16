import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherOfStudentsComponent } from './teacher-of-students.component';

describe('TeacherOfStudentsComponent', () => {
  let component: TeacherOfStudentsComponent;
  let fixture: ComponentFixture<TeacherOfStudentsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TeacherOfStudentsComponent]
    });
    fixture = TestBed.createComponent(TeacherOfStudentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
