import { TestBed } from '@angular/core/testing';

import { RelationServiceService } from './relation-service.service';

describe('RelationServiceService', () => {
  let service: RelationServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RelationServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
