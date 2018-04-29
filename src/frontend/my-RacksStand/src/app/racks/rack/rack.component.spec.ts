import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RackComponent } from './rack.component';

describe('RackComponent', () => {
  let component: RackComponent;
  let fixture: ComponentFixture<RackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
