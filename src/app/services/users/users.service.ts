import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModelDto } from '../../models/UserModelDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  url: string = "https://localhost:7106/user-api/user"

  constructor(private client: HttpClient) { }

  getUsers(): Observable<UserModelDto[]> {
    return this.client.get<UserModelDto[]>(this.url, httpOptions);
  }

  getUserById(id: string): Observable<UserModelDto> {
    const getUrl = `${this.url}/${id}` ;
    return this.client.get<UserModelDto>(getUrl, httpOptions);
  }

  createUser(user: UserModelDto): Observable<any> {
    return this.client.post(this.url, user, httpOptions);
  }

  updateUser(user: UserModelDto): Observable<any>{
    return this.client.put(this.url, user, httpOptions);
  }

  deleteUser(id:string): Observable<any>{
    const deleteUrl = `${this.url}/${id}`
    return this.client.delete(deleteUrl, httpOptions);
  }
}
