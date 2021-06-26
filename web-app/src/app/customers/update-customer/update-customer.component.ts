import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from 'src/_models';
import { CustomerService } from 'src/_services/customer.service';

@Component({
  selector: 'app-update-customer',
  templateUrl: './update-customer.component.html',
  styleUrls: ['./update-customer.component.scss']
})
export class UpdateCustomerComponent implements OnInit {
  customerId: number;
  customer: Customer;
  formGroup: FormGroup;
  constructor(
    @Inject(CustomerService) private customerService: CustomerService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private router: Router,
    private _snackBar: MatSnackBar
  ) { 
    this.activatedRoute.params.subscribe(r => {
      this.customerId = r.id;
    });
  }

  ngOnInit() {
    this.customerService.get(this.customerId).subscribe(data => {
      this.customer = data;
      this.buildFormValidations();
    });
  }

  
  buildFormValidations() {
    this.formGroup = this.formBuilder.group({
      id:[this.customer.id],
      firstName: [this.customer.firstName,[Validators.required, Validators.minLength(3)]],
      lastName: [this.customer.lastName, Validators.required],
      email: [this.customer.email,[ Validators.required, Validators.email]],
      cellphone: [this.customer.cellphone, Validators.required],
      amountTotal: [this.customer.amountTotal, Validators.required],
      customerType: [this.customer.customerType, Validators.required]
     });
  }

  onSubmit(customer: any) {
    this.customerService.update(customer).subscribe(data => {
      if(data.id) {
        this.openSnackBar('Customer Updated', 'Close');
        setTimeout(() => {
          this.router.navigate(['']);
        }, 1500);
      }
    });
   }

   openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
