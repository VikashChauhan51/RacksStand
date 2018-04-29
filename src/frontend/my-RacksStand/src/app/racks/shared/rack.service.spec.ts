import { TestBed, inject } from '@angular/core/testing';

import { RackService } from './rack.service';

describe('RackService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RackService]
    });
  });

  it('should ...', inject([RackService], (service: RackService) => {
    expect(service).toBeTruthy();
  }));
});
