import { NgModule } from '@angular/core';
import { SharedComponent } from './shared.component';
import { TableComponent } from './component/basic/table/table.component';
import { CommonModule } from '@angular/common';
import { PopUpComponent } from './component/basic/pop-up/pop-up.component';
import { StepperComponent } from './component/basic/stepper/stepper.component';
import { StepDirective } from './component/basic/stepper/step.directive';



@NgModule({
  declarations: [
    SharedComponent,
    TableComponent,
    PopUpComponent,
    StepperComponent,
    StepDirective,
  ],
  imports: [
    CommonModule,
  ],
  exports: [
    SharedComponent,
    TableComponent,
    PopUpComponent,
    StepperComponent,
  ]
})
export class SharedModule { }
