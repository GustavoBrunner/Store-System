import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryModelDto } from '../../models/CategoryModelDto';

//criar o header para requisições do tipo post
const httpOptions = {
  headers: new HttpHeaders({
    'Content-type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  url: string = 'https://localhost:7106/product-api/category'

  //classe httpclient recebida por injeção de dependência
  constructor(private client: HttpClient) { }

  getCategoryById(id?: string): Observable<CategoryModelDto>{
    const getUrl = `${this.url}/${id}`;
    
    return this.client.get<CategoryModelDto>(getUrl, httpOptions);
  }
  getAllCategories(): Observable<CategoryModelDto[]>{
    return this.client.get<CategoryModelDto[]>(this.url);
  }

  createCategory(category: CategoryModelDto): Observable<CategoryModelDto>{
    return this.client.post<any>(this.url, category, httpOptions);
  }

  deleteCategory(id: string): Observable<boolean>{
    const deleteUrl = `${this.url}/${id}`;
    
    return this.client.delete<boolean>(deleteUrl, httpOptions);
  }

  updateCategory(category: CategoryModelDto): Observable<boolean> {
    return this.client.put<boolean>(this.url, category, httpOptions)
  }

}
