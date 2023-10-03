import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IArticuloTienda } from 'src/app/Interfaces/IArticuloTienda';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class ArticuloTiendaService {

  url:string= environment.BaseApiUrl+"Inventario/";

  constructor(private http: HttpClient) { }

  getAll(): Observable<IArticuloTienda[]>{
    let GetUrl=this.url;
    return this.http.get<IArticuloTienda[]>(GetUrl);
  }
  getByTienda(idTienda:number): Observable<IArticuloTienda[]>{
    let GetUrl=this.url+idTienda;
    return this.http.get<IArticuloTienda[]>(GetUrl);
  }

  postStock(idArticulo:number,idTienda:number,stock:number): Observable<string>{
    const body= JSON.stringify({'idArticulo':idArticulo,'idTienda':idTienda,'stock':stock});
    const headers= {
      'content-type':'application/json'
     }
    alert(body)
    let GetUrl=this.url;
    return this.http.post<string>(GetUrl,body,{'headers':headers});
  }

}
