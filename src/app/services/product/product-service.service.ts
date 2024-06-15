import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductModelDto } from '../../models/ProductModelDto';

//construção de um novo header para a requisição
const httpOptions = {
  headers: new HttpHeaders({
    'Content-type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ProductServiceService {
  
  //url de acesso para a API!!!!!!!!
  url = 'https://localhost:7106/product-api/product' 
  //injeção de um objeto do tipo http client para o serviço
  constructor(private client: HttpClient) { }

  //metodos http
  //método responsável por criar uma requisição http que será enviada para a url, e irá retornar um array de productmodeldto    
  //Observable se trata de uma classe que observa valores que vão alterando. Como parâmetro deste método, enviamos a nossa url de acesso 
  getAllProducts(): Observable<ProductModelDto[]>{
    return this.client.get<ProductModelDto[]>(this.url);
  }

  getProductById(id: string): Observable<ProductModelDto>{
    //quando recebemos algum id, passamos eles através de uma concatenação de strings para ums nova constante, e passamos essa contante para o parâmetro do método get
    const apiUrl = `${this.url}/${id}`;
    return this.client.get<ProductModelDto>(apiUrl);
  }

  //quando não sabemos o tipo do objeto a ser retornado como parâmetro genérico do tipo Observable, podemos retornar um any
  createProduct(productmodeldto: ProductModelDto): Observable<ProductModelDto>{
    //método post para inserção de dados no banco. Aqui passamos nossa url, o objeto a ser inserido ou atualizado, e as opções do http, que vai representar o cabeçalho da requisição, application/json
    return this.client.post<ProductModelDto>(this.url, productmodeldto, httpOptions);
  }
  
  updateProduct(productmodeldto: ProductModelDto): Observable<any>{
    
    return this.client.put<any>(this.url, productmodeldto, httpOptions);
  }

  deleteProduct(id: string): Observable<any>{
    const apiUrl = `${this.url}/${id}`;
    return this.client.delete<ProductModelDto>(apiUrl, httpOptions);
  }
}
