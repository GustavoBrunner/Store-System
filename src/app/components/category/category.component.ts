import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category/category.service';
import { FormControl, FormGroup } from '@angular/forms';
import { CategoryModelDto } from '../../models/CategoryModelDto';
import { ProductModelDto } from '../../models/ProductModelDto';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss'
})
export class CategoryComponent implements OnInit {


  form: any;
  categories: CategoryModelDto[] = [];

  formTitle: string = "";

  isShowForm: any;
  isShowTable: any;
  isShowProducts: any;
  isEditingCategory: any;


  category: CategoryModelDto = new CategoryModelDto;

  constructor(private service: CategoryService){}

  ngOnInit(): void {
    this.showTable();
  }
  showForm(category?: CategoryModelDto): void{
    this.isShowProducts = false;
    this.isShowTable = false;
    this.isShowForm = true;
    if(category != null){
      this.isEditingCategory = true;
      this.formTitle = "Update Category";
        this.form = new FormGroup({
        id: new FormControl(category.id),
        name: new FormControl(category.name),
        image: new FormControl(category.image),
      })
      return;
    }
    this.isEditingCategory = false;
    this.formTitle = "New Category";

    this.form = new FormGroup({
      name: new FormControl(null),
      image: new FormControl(null),
    })
  }
  showTable(): void{
    this.isShowProducts = false;
    this.isShowForm = false;
    this.isShowTable = true;

    //pega todas as categorias armazenadas no banco de dados e insere no array criado. O subscribe é o que executa uma função em type script.
    this.service.getAllCategories().subscribe(result => {
      this.categories = result;
    });
  }


  createCategory(isEditing: boolean): void{
    const category: CategoryModelDto = this.form.value; 
    alert(isEditing);
    if(isEditing){
      this.service.updateCategory(category).subscribe();
      this.showTable();
      return;
    }
    this.service.createCategory(category).subscribe();
    this.showTable();
  }
  showProducts(id?: string): void {
    this.service.getCategoryById(id).subscribe(result => {
      this.category.id = result.id;
      this.category.image = result.image;
      this.category.name = result.name;
      this.category.products = result.products;
    });
    this.isShowForm = false;
    this.isShowTable = false;
    this.isShowProducts = true;
    
  }

  deleteCategory(id: string) : void {
    this.service.deleteCategory(id).subscribe(result =>{
      this.showTable();
      if(result){
        alert("Category deleted");
        }
        else{ alert("Not possible to delete category!"); }
      })
  }
  editCategory(id: string) : void {
    let categoryModel: CategoryModelDto;
    this.service.getCategoryById(id).subscribe(result => {
      categoryModel = result;
      this.showForm(categoryModel);
    });

  }


  back(): void {
    throw new Error('Method not implemented.');
  }

  BuyProduct(product: ProductModelDto): void {
    
  }
}
