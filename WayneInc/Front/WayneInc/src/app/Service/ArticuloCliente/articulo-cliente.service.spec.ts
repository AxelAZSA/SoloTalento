import { TestBed } from '@angular/core/testing';

import { ArticuloClienteService } from './articulo-cliente.service';

describe('ArticuloClienteService', () => {
  let service: ArticuloClienteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ArticuloClienteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
