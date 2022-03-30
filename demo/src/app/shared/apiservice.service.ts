import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { LoginComponent } from '../login/login.component';
@Injectable({
  providedIn: 'root'
})
export class ApiserviceService {

  constructor(private http: HttpClient) { }

  post(data: any): Observable<any> {
    return this.http.post<any>("https://localhost:44362/api/Cosmos/createdocument", data)
      .pipe(map((res: any) => {
        return res;
      }))
}

PlaceOrder(data: any): Observable<any> {
  return this.http.post<any>("https://localhost:44362/api/Cosmos/placeorder", data)
    .pipe(map((res: any) => {
      return res;
    }))
}
createdb() {
  return this.http.get<any>("https://localhost:44362/api/Cosmos/createdb")
   // pipe(map((res: any) => { return res; }))
}
createcollection() {
  return this.http.get<any>("https://localhost:44362/api/Cosmos/createcollection").
    pipe(map((res: any) => { return res; }))
}

getData() {
  
 const token= localStorage.getItem("jwt")

  return this.http.get("https://localhost:44362/api/Cosmos/get",
  {
    headers:new HttpHeaders(
  {
    'Authorization':`Bearer ${token}`,
    'Content-Type': 'application/json'
  }
)
 }).
 pipe(map((res: any) => { return res; }))

}
 

 

/*updateDb(data: any, id: number) {
  return this.http.put<any>("http://localhost:47973/api/Cosmos/updateemployee" + id, data)
    .pipe(map((res: any) => { return res; }))
}*/


deleteuser(id:string) {
  return this.http.delete<any>("https://localhost:44362/api/Cosmos/delete/"+id)
    
}
}
