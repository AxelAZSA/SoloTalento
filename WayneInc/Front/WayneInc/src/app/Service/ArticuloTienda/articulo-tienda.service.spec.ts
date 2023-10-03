import { TestBed } from '@angular/core/testing';

import { ArticuloTiendaService } from './articulo-tienda.service';

describe('ArticuloTiendaService', () => {
  let service: ArticuloTiendaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ArticuloTiendaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
