import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ArticuloTiendaService } from '../Service/ArticuloTienda/articulo-tienda.service';
import { Router } from '@angular/router';
import { ArticuloService } from '../Service/Articulo/articulo.service';
import { TiendaService } from '../Service/Tienda/tienda.service';
import { ITienda } from '../Interfaces/ITienda';
import { IArticulo } from '../Interfaces/IArticulo';

@Component({
  selector: 'app-inventario-edit',
  templateUrl: './inventario-edit.component.html',
  styleUrls: ['./inventario-edit.component.css']
})
export class InventarioEditComponent {

  tiendas!:ITienda[];
  articulos!:IArticulo[];

  sentStock:FormGroup=this.form.group({
    stock: new FormControl(),
    idArticulo: new FormControl(),
    idTienda: new FormControl()
  });

  constructor(private articuloTService:ArticuloTiendaService,private tiendaService:TiendaService,private articuloService:ArticuloService,private form:FormBuilder,private router:Router){}

  ngOnInit():void{
    this.articuloService.getAll().subscribe(data=>{
      this.articulos = data;
      this.tiendaService.getAll().subscribe(data=>{
        this.tiendas=data;
      })
    })

  }
  onSubmit(data:any){
    var idArticulo = Number(data.idArticulo)
    var stock = Number(data.stock)
    var idTienda = Number(data.idTienda)
    this.articuloTService.postStock(idArticulo,idTienda,stock).subscribe(data=>{
      this.router.navigate(['/inventario'])
    });
  }
}
