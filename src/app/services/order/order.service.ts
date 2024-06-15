import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderModelDto } from '../../models/OrderModelDto';


const httpOptions = {
  headers: new HttpHeaders({
    'Context-type':'application/json' 
  })
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  url = "https://localhost:7106/basket-api/order"
  constructor(private client: HttpClient) { }

  getOrderById(id: string): Observable<OrderModelDto>{
    let getUrl = `${this.url}/${id}`
    return this.client.get<OrderModelDto>(getUrl);
  }

  getOrderByUserId(id: string): Observable<OrderModelDto[]> {
    let getUrl = `${this.url}/user/${id}`;
    return this.client.get<OrderModelDto[]>(getUrl);
  }

  createOrder(order: OrderModelDto): Observable<any>{
    return this.client.post<any>(this.url, order, httpOptions);
  }

  deleteOrder(id: string): Observable<any>{
    let deleteUrl = `${this.url}/${id}`
    return this.client.delete<any>(deleteUrl, httpOptions);
  }

  updateOrder(order: OrderModelDto): Observable<any>{
    return this.client.put<any>(this.url, order);
  }


}
