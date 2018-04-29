import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierAddressComponent } from './supplier-address.component';

describe('SupplierAddressComponent', () => {
  let component: SupplierAddressComponent;
  let fixture: ComponentFixture<SupplierAddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierAddressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
