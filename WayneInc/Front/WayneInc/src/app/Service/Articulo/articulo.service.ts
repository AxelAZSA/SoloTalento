import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IArticulo } from 'src/app/Interfaces/IArticulo';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class ArticuloService {

  url:string= environment.BaseApiUrl+"Articulo";

  constructor(private http: HttpClient) { }

  getArticuloById(id:number): Observable<IArticulo>{
    let GetUrl=this.url+id;
    return this.http.get<IArticulo>(GetUrl);
  }

  getAll(): Observable<IArticulo[]>{
    const headers= {
      'content-type':'application/json'
    }
    let GetUrl=this.url;
    return this.http.get<IArticulo[]>(GetUrl,{'headers':headers} );
  }

  post(articulo:IArticulo): Observable<number>{
    const headers= {
      'content-type':'application/json'
    }

  const body= JSON.stringify(articulo);
  console.log(body);
    let GetUrl=this.url;
   return this.http.post<number>(GetUrl,body,{'headers':headers});
  }

  put(articulo:IArticulo): Observable<IArticulo>{
    const headers= {
      'content-type':'application/json'
     }
    const body=JSON.stringify({'articulo':articulo});
     console.log(body);
     let GetUrl=this.url+articulo.idArticulo;
    return this.http.put<IArticulo>(GetUrl,articulo,{'headers':headers});
  }

  delete(id:number): Observable<IArticulo>{
    const headers= {
      'content-type':'application/json'
     }

     let GetUrl=this.url+id;
    return this.http.delete<IArticulo>(GetUrl,{'headers':headers});
  }
}
