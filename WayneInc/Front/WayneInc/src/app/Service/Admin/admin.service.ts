import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAdmin } from 'src/app/Interfaces/IAdmin';
import { IAuthentication } from 'src/app/Interfaces/IAuthentication';
import { ILogin } from 'src/app/Interfaces/ILogin';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
url:string= environment.BaseApiUrl+'admin/';

constructor(private http: HttpClient) { }

postLogin(login:ILogin): Observable<IAuthentication>{
  const headers= {
    'content-type':'application/json'
  }

const body= JSON.stringify(login);
console.log(body);
  let GetUrl=this.url+"login";
 return this.http.post<IAuthentication>(GetUrl,body,{'headers':headers});
}

postAdmin(admin:IAdmin): Observable<IAuthentication>{
  const headers= {
    'content-type':'application/json'
  }

const body= JSON.stringify(admin);
console.log(body);
  let GetUrl=this.url+"register";
 return this.http.post<IAuthentication>(GetUrl,body,{'headers':headers});
}

logut(): Observable<any>{
  let GetUrl=this.url+'logout';
  return this.http.delete<any>(GetUrl);
}
}
