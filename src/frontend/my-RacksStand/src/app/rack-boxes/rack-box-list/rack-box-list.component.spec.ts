import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RackBoxListComponent } from './rack-box-list.component';

describe('RackBoxListComponent', () => {
  let component: RackBoxListComponent;
  let fixture: ComponentFixture<RackBoxListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RackBoxListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RackBoxListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
