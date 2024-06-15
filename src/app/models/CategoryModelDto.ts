import { BaseModelDto } from "./BaseModelDto";
import { ProductModelDto } from "./ProductModelDto";

export class CategoryModelDto extends BaseModelDto{
    products: ProductModelDto[] = [];
    
}