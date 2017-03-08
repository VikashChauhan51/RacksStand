import { TestBed, inject } from '@angular/core/testing';

import { MessageService } from './message.service';

describe('MessageServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MessageService]
    });
  });

  it('should ...', inject([MessageService], (service: MessageService) => {
    expect(service).toBeTruthy();
  }));
});
