import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Customer } from 'src/_models';
import { CustomerService } from 'src/_services/customer.service';
@Component({
  selector: 'app-add-customer',
  templateUrl: './add-customer.component.html',
  styleUrls: ['./add-customer.component.scss']
})
export class AddCustomerComponent implements OnInit {
  customers: Customer;
  formGroup: FormGroup;
  
  constructor(
    @Inject(CustomerService) private customerService: CustomerService,
    private router: Router,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar) 
    { }

  ngOnInit() {
    this.buildFormValidations();
  }

  buildFormValidations() {
    this.formGroup = this.formBuilder.group({
      firstName: [null,[Validators.required, Validators.minLength(3)]],
      lastName: [null, Validators.required],
      email: [null,[ Validators.required, Validators.email]],
      cellphone: [null, Validators.required],
      amountTotal: [null, Validators.required],
      customerType: [null, Validators.required],
    });
  }

  onSubmit(customer: any){
    this.customerService.create(customer).subscribe(data => {
      if(data.id){
        this.openSnackBar('Customer Created', 'Close');
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
