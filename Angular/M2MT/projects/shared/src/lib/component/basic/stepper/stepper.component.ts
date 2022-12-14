import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, Type, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { StepComponent } from './step.component';
import { StepDirective } from './step.directive';

export interface Step<T> {
  component: Type<StepComponent<T>>, 
  data: T,
  name?: string,
}

@Component({
  selector: 'lib-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss']
})
export class StepperComponent implements OnInit, OnDestroy {
  @Input() steps: Step<any>[] = [];
  @Output() stepsChange = new EventEmitter<Step<any>[]>();
  private subscription?: Subscription;

  @ViewChild(StepDirective, {static: true}) stepHost!: StepDirective;
  private currentIndex = 0;
  isLowest = false;
  isHighest = false;

  ngOnInit(): void {
    this.currentIndex = 0;
    this.setActive();
  }

  previous(): void {
    this.currentIndex--;
    this.setActive()
  }

  next(): void {
    this.currentIndex++;
    this.setActive();
  }

  stepClicked(index: number): void {
    this.currentIndex = index;
    this.setActive();
  }

  setActive(): void {
    // next line lowers the change that when two inputs are pressed at or near the same time (++ ++ or -- --) the value can not be upgraded during the function, and therefore passing the outerbounds
    const workingIndex = this.currentIndex; 
    if (this.lowBoundCheck(workingIndex)) return this.setMax();
    if (this.upBoundCheck(workingIndex)) return this.setMin();
    const viewContainerRef = this.stepHost.viewContainerRef;
    viewContainerRef.clear();

    this.updateControl()
    const componentRef = viewContainerRef.createComponent<StepComponent<any>>(this.steps[workingIndex].component);
    componentRef.instance.injectData(this.steps[workingIndex].data);
    this.subscription = componentRef.instance.dataChange.subscribe((data: any) => {
      this.steps[workingIndex].data = data;
      this.next();
      this.stepsChange.emit(this.steps)
    })
  }

  
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  setMax() {
    this.currentIndex = this.steps.length - 1;
  }

  setMin() {
    this.currentIndex = 0;
  }

  updateControl() {
    // since the method checks if value is over it needs one to be added and/or removed
    this.isLowest = this.lowBoundCheck(this.currentIndex - 1);
    this.isHighest = this.upBoundCheck(this.currentIndex + 1);
  }

  lowBoundCheck(value: number) {
    return value < 0;
  }

  upBoundCheck(value: number) {
    return value >= this.steps.length;
  }

}
