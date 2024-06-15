import { OrderModelDto } from "./OrderModelDto";

export class BasketModelDto {
    id: string = "";
    userId: string = "";
    order: OrderModelDto[] = [];
    userName: string = "";
    confirmed: boolean = false;


}