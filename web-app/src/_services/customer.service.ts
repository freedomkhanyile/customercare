import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Customer } from 'src/_models';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  api: string;
  constructor(
    private http: HttpClient,
  ) {
    this.api = environment.API_URL;
  }

  getAll() : Observable<Customer[]>{
    return this.http.get<Customer[]>(`${this.api}/api/customers`);
  }

  get(id: number) : Observable<Customer>{
    return this.http.get<Customer>(`${this.api}/api/customers/${id}`);
  }

  create(model: Customer): Observable<Customer> {
    return this.http.post<Customer>(`${this.api}/api/customers`, model);
  }

  update(model: Customer): Observable<Customer> {
    return this.http.put<Customer>(`${this.api}/api/customers/${model.id}`, model);
  }

  delete(customerId: string): Observable<boolean> {
   return this.http.delete<boolean>(`${this.api}/api/customers/${customerId}`);
  }
  
}
