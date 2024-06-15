import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddressModelDto } from '../../models/AdressModelDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-type':'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  url = "https://localhost:7106/user-api/address"
  constructor(private client: HttpClient) { }

  getAddressById(id: string): Observable<AddressModelDto> {
    let getUrl = `${this.url}/${id}`;
    return this.client.get<AddressModelDto>(getUrl);
  }

  getAddressByUserId(id:string): Observable<AddressModelDto> {
    let getUrl = `${this.url}/${id}`
    return this.client.get<AddressModelDto>(getUrl);
  }

  createAddress(address: AddressModelDto): Observable<AddressModelDto>{
    return this.client.post<any>(this.url, address, httpOptions);
  }

  deleteAddress(id: string): Observable<any>{
    let deleteUrl = `${this.url}/${id}`;
    return this.client.delete<any>(deleteUrl, httpOptions);
  }

  updateAddress(address: AddressService): Observable<AddressModelDto> {
    return this.client.put<AddressModelDto>(this.url, address, httpOptions);
  }
}
