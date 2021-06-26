import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Customer } from 'src/_models';
import { CustomerService } from 'src/_services/customer.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit {

  customers: Customer[] = [];

  constructor(
    @Inject(CustomerService) private customerService: CustomerService) {
  }

  ngOnInit() {
   this.getCustomers();
  }
  getCustomers(){
    this.customerService.getAll().subscribe(data => {
      this.customers = data;      
    });
  }
}
