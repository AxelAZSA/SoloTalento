import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAuthentication } from 'src/app/Interfaces/IAuthentication';
import { ICliente } from 'src/app/Interfaces/ICliente';
import { ILogin } from 'src/app/Interfaces/ILogin';
import { ISesion } from 'src/app/Interfaces/ISesion';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class SesionService {

  url:string= environment.BaseApiUrl+'Sesion/';

constructor(private http: HttpClient) { }

getByCliente(idCliente:number): Observable<ISesion>{
  let GetUrl=this.url+idCliente;
  return this.http.get<ISesion>(GetUrl);
}

postLogin(login:ILogin): Observable<IAuthentication>{
  const headers= {
    'content-type':'application/json'
  }

const body= JSON.stringify(login);
console.log(body);
  let GetUrl=this.url+"login";
 return this.http.post<IAuthentication>(GetUrl,body,{'headers':headers});
}

putSesion(sesion:ISesion): Observable<String>{
  const headers= {
    'content-type':'application/json'
  }

const body= JSON.stringify(sesion);
console.log(body);
  let GetUrl=this.url+"edit/"+sesion.id;
 return this.http.put<String>(GetUrl,body,{'headers':headers});
}

postSesion(sesion:ISesion): Observable<IAuthentication>{
  const headers= {
    'content-type':'application/json'
  }

const body= JSON.stringify(sesion);
console.log(body);
  let GetUrl=this.url+"register";
 return this.http.post<IAuthentication>(GetUrl,body,{'headers':headers});
}

logut(): Observable<any>{
  let GetUrl=this.url+'logout';
  return this.http.delete<any>(GetUrl);
}
}
