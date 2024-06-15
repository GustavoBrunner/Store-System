import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ProductServiceService } from './services/product/product-service.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductComponentComponent } from './components/product-component/product-component.component';
import { CategoryComponent } from './components/category/category.component';
import { BasketComponent } from './components/basket/basket/basket.component';
import { UsersComponent } from './components/users/users.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductComponentComponent,
    CategoryComponent,
    BasketComponent,
    UsersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    // registrar o modal para toda a aplicação
    ModalModule.forRoot(),
    CommonModule,
  ],
  providers: [HttpClientModule, ProductServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
