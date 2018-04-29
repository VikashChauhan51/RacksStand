import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RackBoxComponent } from './rack-box.component';

describe('RackBoxComponent', () => {
  let component: RackBoxComponent;
  let fixture: ComponentFixture<RackBoxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RackBoxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RackBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
