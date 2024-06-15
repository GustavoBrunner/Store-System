import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderItemsModelDto } from '../../models/OrderItemsModelDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-type': 'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})
export class ItemOrderService {
  url = 'https://localhost:7106/basket-api/item'
  
  constructor(private client: HttpClient) { }

  getItemById(id: string): Observable<OrderItemsModelDto> {
    let getUrl = `${this.url}/${id}`
    return this.client.get<OrderItemsModelDto>(getUrl);
  }

  getItemByOrderId(id: string): Observable<OrderItemsModelDto[]>{
    let getUrl = `${this.url}/${id}`;
    return this.client.get<OrderItemsModelDto[]>(getUrl);
  }

  createItemOrder(item: OrderItemsModelDto): Observable<any>{
    return this.client.post<any>(this.url, this.url, httpOptions);
  } 

  deleteItemOrder(id: string): Observable<any>{
    let deleteUrl = `${this.url}/${id}`;
    return this.client.delete<any>(deleteUrl, httpOptions);
  }

  updateItemOrder(item: OrderItemsModelDto): Observable<any>{
    return this.client.put<any>(this.url, item, httpOptions);
  }
}
