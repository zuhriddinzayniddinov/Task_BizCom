import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentOfSciencesComponent } from './student-of-sciences.component';

describe('StudentOfSciencesComponent', () => {
  let component: StudentOfSciencesComponent;
  let fixture: ComponentFixture<StudentOfSciencesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StudentOfSciencesComponent]
    });
    fixture = TestBed.createComponent(StudentOfSciencesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
