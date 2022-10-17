import { TestBed } from '@angular/core/testing';

import { MappingServiceService } from './mapping-service.service';

describe('MappingServiceService', () => {
  let service: MappingServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MappingServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
