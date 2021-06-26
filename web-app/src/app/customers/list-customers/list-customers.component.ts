import { AfterViewInit, Component, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Customer } from 'src/_models';
import { CustomerService } from 'src/_services/customer.service';

@Component({
  selector: 'app-list-customers',
  templateUrl: './list-customers.component.html',
  styleUrls: ['./list-customers.component.scss']
})
export class ListCustomersComponent implements OnInit, AfterViewInit {
  
  displayedColumns: string[] = ['firstname', 'surname', 'email', 'cellphone', 'amountTotal', 'actions'];

  dataSource = new MatTableDataSource<Customer>();

  @Input() customers: Customer[] = [];
  @Output() refreshList: EventEmitter<Boolean> = new EventEmitter();
  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.dataSource = new MatTableDataSource<Customer>(this.customers);
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  constructor(
    @Inject(CustomerService) private customerService: CustomerService,
    private _snackBar: MatSnackBar,    
    private router: Router) { }
  navigate(url: string) {
    this.router.navigate([url]);
  }

  view(id: string){
    this.router.navigate([`edit-customer`, id]);
  }

  delete(id: number) {
    this.customerService.delete(id.toString()).subscribe(data => {
      if(data){
        this.customers = this.customers.filter(x => x.id !== id);
        setTimeout(() => {
          this.refreshList.emit(true);
          this.openSnackBar('Customer deleted successfully', 'Close');
        }, 1500);
      } else {
        this.openSnackBar('Something went wrong try again later.', 'Close');
      }
    });
   
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
