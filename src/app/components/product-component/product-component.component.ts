import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ProductModelDto } from '../../models/ProductModelDto';
import { ProductServiceService } from '../../services/product/product-service.service';
import { CategoryService } from '../../services/category/category.service';
import { CategoryModelDto } from '../../models/CategoryModelDto';

@Component({
  selector: 'app-product-component',
  templateUrl: './product-component.component.html',
  styleUrl: './product-component.component.scss'
})
export class ProductComponentComponent implements OnInit {
  //uma variável para definir o formulário.
  formulario: any;

  formTitulo?: string;

  categories: CategoryModelDto[] = [];
  
  //injetamos serviços necessários através do construtor, igual é no .net
  constructor(private productService: ProductServiceService, private categoryService: CategoryService){}
  
  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe(result => {
      this.categories = result;
    });
    this.formTitulo = "New Product:"
    //aqui temos algo similar à criação de formulário no cshtml, onde iremos colocar todos os inputs necessários do formulário através de variáveis, o ForGroup é um grupo de FormControl.
    this.formulario = new FormGroup({
      name: new FormControl(null),
      price: new FormControl(null),
      image: new FormControl(null),
      stock: new FormControl(null),
      productDescription: new FormControl(null),
      categoryId: new FormControl(null),
    })
  }
  SendProductForm(): void{
    //guardamos os dados que vieram do formulário em um objeto do tipo desejado
    const product: ProductModelDto = this.formulario.value;
    this.productService.createProduct(product).subscribe(resultado => {
      alert(`Product ${resultado.name} created successfully`)
    });

  }

}
