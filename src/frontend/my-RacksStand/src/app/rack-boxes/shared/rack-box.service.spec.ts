import { TestBed, inject } from '@angular/core/testing';

import { RackBoxService } from './rack-box.service';

describe('RackBoxService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RackBoxService]
    });
  });

  it('should ...', inject([RackBoxService], (service: RackBoxService) => {
    expect(service).toBeTruthy();
  }));
});
