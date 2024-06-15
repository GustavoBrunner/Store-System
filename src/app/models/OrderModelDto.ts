import { AddressModelDto } from "./AdressModelDto";
import { OrderItemsModelDto } from "./OrderItemsModelDto";

export class OrderModelDto {
    id: string = "";
    products: OrderItemsModelDto = new OrderItemsModelDto();
    adress: AddressModelDto = new AddressModelDto();
    finished: boolean = false;
    delievered: boolean = false;
}