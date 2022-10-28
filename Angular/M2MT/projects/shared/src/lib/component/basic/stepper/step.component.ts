import { EventEmitter } from "@angular/core";

export interface StepComponent<T> {
  injectData(data: T): void;
  dataChange: EventEmitter<T>;
 }