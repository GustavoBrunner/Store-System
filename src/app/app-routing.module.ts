import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponentComponent } from './components/product-component/product-component.component';
import { CategoryComponent } from './components/category/category.component';
import { BasketComponent } from './components/basket/basket/basket.component';
import { UsersComponent } from './components/users/users.component';

//definição das rotas da aplicação, aqui é onde definimos quais os endpoints que a aplicação terá.
const routes: Routes = [{
  //aqui definimos uma ligação entre a rota da requisição, e o componente que ela irá abrir. Componentes são conjunto de código fonte, css, html. se digitarmos ..localhost/product, o angular chamará o component ProductComponent
  path: 'product', component: ProductComponentComponent},
  {path: 'category', component: CategoryComponent},
  {path: 'basket', component: BasketComponent},
  {path: 'user', component: UsersComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
