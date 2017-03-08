import { TestBed, async, inject } from '@angular/core/testing';

import { CanDeactivateGuardService } from './can-deactivate-guard.service';

describe('CanDeactivateGuardServiceGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CanDeactivateGuardService]
    });
  });

  it('should ...', inject([CanDeactivateGuardService], (guard: CanDeactivateGuardService) => {
    expect(guard).toBeTruthy();
  }));
});
