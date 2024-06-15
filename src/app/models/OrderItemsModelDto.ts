import { BaseModelDto } from "./BaseModelDto";
import { ProductModelDto } from "./ProductModelDto";

export class OrderItemsModelDto extends BaseModelDto {
    products: ProductModelDto[] = [];
    
}