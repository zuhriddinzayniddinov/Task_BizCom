import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherOfSciencesComponent } from './teacher-of-sciences.component';

describe('TeacherOfSciencesComponent', () => {
  let component: TeacherOfSciencesComponent;
  let fixture: ComponentFixture<TeacherOfSciencesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TeacherOfSciencesComponent]
    });
    fixture = TestBed.createComponent(TeacherOfSciencesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
