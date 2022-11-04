import { Injectable, OnDestroy } from '@angular/core';
import { MappingRelation } from 'projects/shared/src/lib/Data/models/mapping-relation.model';
import { Subject, Subscription } from 'rxjs';
import { Attribute } from '../../../../shared/src/lib/Data/models/attribute.model';



interface ActionState {
  LeftSelected(id: string): void;
  RightSelected(id: string): void;
  RelationSelected(id: string): void;
  CancelAction(): void;
}

class DefaultState implements ActionState {
  constructor(private observers: Observers, private stateObserver: Subject<ActionState>) {
    observers.sendAllOff();
  }

  LeftSelected(id: string): void { }
  RightSelected(id: string): void { }
  RelationSelected(id: string): void { }
  CancelAction(): void { }
}

class DeleteState implements ActionState {
  constructor(private observers: Observers, private stateObserver: Subject<ActionState>) {
    observers.sendAllOff();
    observers.arrowsHighlight.next(Observers.ON);
    observers.cancelable.next(Observers.ON);
  }

  LeftSelected(id: string): void {  }
  RightSelected(id: string): void {  }
  RelationSelected(id: string): void {
    this.stateObserver.next(new DefaultState(this.observers, this.stateObserver));
  }
  CancelAction(): void { 
    this.stateObserver.next(new DefaultState(this.observers, this.stateObserver));
  }
}

class CreateSelectLeftState implements ActionState {
  constructor(private observers: Observers, private stateObserver: Subject<ActionState>) {
    observers.sendAllOff();
    observers.leftSideHighlight.next(Observers.ON);
    observers.cancelable.next(Observers.ON);
  }

  LeftSelected(id: string): void { 
    this.stateObserver.next(new CreateSelectRightState(this.observers, this.stateObserver));
  }
  RightSelected(id: string): void {  }
  RelationSelected(id: string): void { }
  CancelAction(): void { 
    this.stateObserver.next(new DefaultState(this.observers, this.stateObserver));
  }
}

class CreateSelectRightState implements ActionState {
  constructor(private observers: Observers, private stateObserver: Subject<ActionState>) {
    observers.sendAllOff();
    observers.rightSideHighlight.next(Observers.ON);
    observers.cancelable.next(Observers.ON);
  }

  LeftSelected(id: string): void { }
  RightSelected(id: string): void { 
    this.stateObserver.next(new DefaultState(this.observers, this.stateObserver));
   }
  RelationSelected(id: string): void { }
  CancelAction(): void { 
    this.stateObserver.next(new DefaultState(this.observers, this.stateObserver));
  }
}

class Observers {
  public static readonly OFF = false;
  public static readonly ON = true;

  public readonly leftSideHighlight = new Subject<boolean>();
  public readonly rightSideHighlight = new Subject<boolean>();
  public readonly arrowsHighlight = new Subject<boolean>();
  public readonly cancelable = new Subject<boolean>();

  public sendAllOff() {
    this.leftSideHighlight.next(Observers.OFF);
    this.arrowsHighlight.next(Observers.OFF);
    this.rightSideHighlight.next(Observers.OFF);
    this.cancelable.next(Observers.OFF);
  }
}

@Injectable({
  providedIn: 'root'
})
export class MappingService implements OnDestroy {
  private readonly stateObserver = new Subject<ActionState>();
  private subscription?: Subscription;
  public readonly observers = new Observers();
  private actionState: ActionState = new DefaultState(this.observers, this.stateObserver);
  
  constructor() {
    this.subscription = this.stateObserver.subscribe((newState: ActionState) => {
      this.actionState = newState;
    });
  }
  
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
  
  delete() {
    if (this.actionState instanceof DeleteState) return;
    this.actionState = new DeleteState(this.observers, this.stateObserver);
  }
  
  create() {
    if (this.actionState instanceof CreateSelectLeftState || this.actionState instanceof CreateSelectRightState) return;
    this.actionState = new CreateSelectLeftState(this.observers, this.stateObserver);
  }

  cancel() {
    this.actionState.CancelAction();
  }
  
  LeftSelected(attribute: Attribute): void {
    this.actionState.LeftSelected(attribute.id);
  }
  
  RightSelected(attribute: Attribute): void { 
    this.actionState.RightSelected(attribute.id);
  }

  RelationSelected(relation: MappingRelation): void {
    this.actionState.RelationSelected(relation.id);
  }
}
