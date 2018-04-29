import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RackListComponent } from './rack-list.component';

describe('RackListComponent', () => {
  let component: RackListComponent;
  let fixture: ComponentFixture<RackListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RackListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RackListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
