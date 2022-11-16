import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ServiceService {
url= 'https://localhost:7020';
  constructor(private http: HttpClient) { }
  saveuserdata(user: any){
    console.log(user);
    return this.http.post(this.url + '/api/Home', user);
    
  }
}
