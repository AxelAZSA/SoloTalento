import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICliente } from 'src/app/Interfaces/ICliente';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  url:string= environment.BaseApiUrl+"cliente/";

  constructor(private http: HttpClient) { }

  getClienteById(id:number): Observable<ICliente>{
    let GetUrl=this.url+id;
    return this.http.get<ICliente>(GetUrl);
  }

  getCliente(): Observable<ICliente>{
    let GetUrl=this.url;
    return this.http.get<ICliente>(GetUrl);
  }

  postCliente(clienteP:ICliente): Observable<number>{
    const headers= {
      'content-type':'application/json'
    }

  const body= JSON.stringify(clienteP);
  console.log(body);
    let GetUrl=this.url;
   return this.http.post<number>(GetUrl,body,{'headers':headers});
  }

  putCliente(clienteP:ICliente): Observable<ICliente>{
    const headers= {
      'content-type':'application/json'
     }
    const body=JSON.stringify({'cliente':clienteP});
     console.log(body);
     let GetUrl=this.url+clienteP.id;
    return this.http.put<ICliente>(GetUrl,clienteP,{'headers':headers});
  }

  deleteCliente(id:number): Observable<ICliente>{
    const headers= {
      'content-type':'application/json'
     }

     let GetUrl=this.url+id;
    return this.http.delete<ICliente>(GetUrl,{'headers':headers});
  }
}
