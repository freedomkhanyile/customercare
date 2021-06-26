import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import {
  AddCustomerComponent,
  CustomersComponent,
  ListCustomersComponent,
  UpdateCustomerComponent
} from './customers';


const routes: Routes = [
  { path: '', component: CustomersComponent },
  { path: 'add-customer', component: AddCustomerComponent },
  { path: 'edit-customer/:id', component: UpdateCustomerComponent },
];

export const declarations = [
  AppComponent,
  CustomersComponent,
  AddCustomerComponent,
  UpdateCustomerComponent,
  ListCustomersComponent
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
