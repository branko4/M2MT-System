import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SharedComponent } from './shared.component';
import { TableComponent } from './component/basic/table/table.component';
import { CommonModule } from '@angular/common';
import { PopUpComponent } from './component/basic/pop-up/pop-up.component';
import { StepperComponent } from './component/basic/stepper/stepper.component';
import { StepDirective } from './component/basic/stepper/step.directive';
import { ConformationPopUpComponent } from './component/basic/conformation-pop-up/conformation-pop-up.component';



@NgModule({
  declarations: [
    SharedComponent,
    TableComponent,
    PopUpComponent,
    StepperComponent,
    StepDirective,
    ConformationPopUpComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
  ],
  exports: [
    SharedComponent,
    TableComponent,
    PopUpComponent,
    StepperComponent,
    ConformationPopUpComponent,
  ]
})
export class SharedModule { }
