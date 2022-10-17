import { TestBed } from '@angular/core/testing';

import { TaxonomyServiceService } from './taxonomy-service.service';

describe('TaxonomyServiceService', () => {
  let service: TaxonomyServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TaxonomyServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
