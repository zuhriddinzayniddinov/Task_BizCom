import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSelectUserComponent } from './admin-select-user.component';

describe('AdminSelectUserComponent', () => {
  let component: AdminSelectUserComponent;
  let fixture: ComponentFixture<AdminSelectUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminSelectUserComponent]
    });
    fixture = TestBed.createComponent(AdminSelectUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
