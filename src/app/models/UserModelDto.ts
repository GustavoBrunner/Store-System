import { AddressModelDto } from "./AdressModelDto";
import { BaseModelDto } from "./BaseModelDto";
import { BasketModelDto } from "./BasketModelDto";

export class UserModelDto extends BaseModelDto {
    surname: string = "";
    
    money: number = 0;

    public get fullName() : string {
        return `${this.name} ${this.surname}`
    }
    
    basketId: string = "";
    basket : BasketModelDto = new BasketModelDto();
    addresses : AddressModelDto[] = [];
}
