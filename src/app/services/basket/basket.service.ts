import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketModelDto } from '../../models/BasketModelDto';

const httpOptions= {
  headers: new HttpHeaders({
    'Content-type':'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  url = "https://localhost:7106/basket-api/basket"
  
  constructor(private client: HttpClient) { }

  getBasketById(id: string): Observable<BasketModelDto> {
    let getUrl = `${this.url}/${id}`;
    return this.client.get<BasketModelDto>(getUrl);
  }

  getBasketByUserId(id: string): Observable<BasketModelDto>{
    let getUrl = `${this.url}/user/${id}`;
    return this.client.get<BasketModelDto>(getUrl);
  }

  createBasket(basket: BasketModelDto): Observable<BasketModelDto>{
    return this.client.post<any>(this.url, basket, httpOptions);
  }

  deleteBasket(id: string): Observable<any>{
    let deleteUrl = `${this.url}/${id}`;
    return this.client.delete<any>(deleteUrl,httpOptions);
  }

  updateBasket(basket: BasketModelDto): Observable<any>{
    return this.client.put<any>(this.url, basket, httpOptions);
  }
}