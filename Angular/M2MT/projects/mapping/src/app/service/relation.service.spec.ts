import { TestBed } from '@angular/core/testing';

import { RelationService } from './relation.service';

describe('RelationServiceService', () => {
  let service: RelationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RelationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
