import { NgModule } from '@angular/core';
import { SharedComponent } from './shared.component';
import { TableComponent } from './component/basic/table/table.component';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [
    SharedComponent,
    TableComponent,
  ],
  imports: [
    CommonModule,
  ],
  exports: [
    SharedComponent,
    TableComponent,
  ]
})
export class SharedModule { }
