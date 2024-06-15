import { BaseModelDto } from "./BaseModelDto";
import { CategoryModelDto } from "./CategoryModelDto";

export class ProductModelDto extends BaseModelDto{
    price: number = 0;
    stock: number = 0;
    productDescription: string = "";
    categoryId: string = "";
    // Category?: CategoryModelDto;

}