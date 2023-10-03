import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ArticuloClienteService } from '../Service/ArticuloCliente/articulo-cliente.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-pago',
  templateUrl: './pago.component.html',
  styleUrls: ['./pago.component.css']
})
export class PagoComponent {

idCarrito!:number;
  constructor(private articuloCService:ArticuloClienteService,private form:FormBuilder, private router:Router, private actRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.actRoute.paramMap.subscribe(params => {
      this.idCarrito= Number(params.get('idCarrito'));
    });}

    onSubmit(idCarrito:number){
      this.articuloCService.postComprar(idCarrito).subscribe(data=>{
        this.router.navigate(['/cliente']);
      });
    }
}
