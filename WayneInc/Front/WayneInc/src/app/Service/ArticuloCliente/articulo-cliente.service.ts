import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IArticuloCliente } from 'src/app/Interfaces/IArticuloCliente';
import { ICarrito } from 'src/app/Interfaces/ICarrito';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class ArticuloClienteService {

  url:string= environment.BaseApiUrl+"Compras/";

  constructor(private http: HttpClient) { }

  getArticuloById(id:number): Observable<IArticuloCliente>{
    let GetUrl=this.url+id;
    return this.http.get<IArticuloCliente>(GetUrl);
  }

  getAll(): Observable<IArticuloCliente[]>{
    let GetUrl=this.url;
    return this.http.get<IArticuloCliente[]>(GetUrl);
  }

  getCarrito(): Observable<ICarrito>{
    let GetUrl=this.url+'Carrito';
    return this.http.get<ICarrito>(GetUrl);
  }

  agregarItem(idArticulo:number): Observable<string>{
    const headers= {
      'content-type':'application/json'
    }
  const body= JSON.stringify({'idArticulo':idArticulo});
  console.log(body);
    let GetUrl=this.url+'Carrito/agregar/'+idArticulo;
   return this.http.post<string>(GetUrl,{'headers':headers});
  }

  postComprar(idCarrito:number): Observable<string>{
    const headers= {
      'content-type':'application/json'
     }
     let GetUrl=this.url+"Comprar/"+idCarrito;
    return this.http.post<string>(GetUrl,{'headers':headers});
  }

    removeItem(idCarritoItem:number,idCarrito:number): Observable<string>{
    const headers= {
      'content-type':'application/json'
     }
     const body= JSON.stringify({'idCarritoItem':idCarritoItem,'idCarrito':idCarrito});
     let GetUrl=this.url+"Carrito/remover";
    return this.http.post<string>(GetUrl,body,{'headers':headers});
  }
}
