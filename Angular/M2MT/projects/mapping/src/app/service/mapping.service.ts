import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { Mapping } from 'projects/shared/src/lib/Data/models/mapping.model';
import { MappingRule } from 'projects/shared/src/lib/Data/models/mapping-rule.model';
import { ModelSidesDTO } from 'projects/shared/src/lib/Data/dtos/model-sides.dto';
import { MappingRelation } from 'projects/shared/src/lib/Data/models/mapping-relation.model';
import { Attribute } from 'projects/shared/src/lib/Data/models/attribute.model';



interface ActionState {
  LeftSelected(id: string): void;
  RightSelected(id: string): void;
  RelationSelected(id: string): void;
  CancelAction(): void;
}

class DefaultState implements ActionState {
  constructor(private sharedClasses: RequiredClassForActionState) {
    sharedClasses.observers.sendAllOff();
  }

  LeftSelected(id: string): void { }
  RightSelected(id: string): void { }
  RelationSelected(id: string): void { }
  CancelAction(): void { }
}

class DeleteState implements ActionState {
  constructor(private sharedClasses: RequiredClassForActionState) {
    sharedClasses.observers.sendAllOff();
    sharedClasses.observers.arrowsHighlight.next(Observers.ON);
    sharedClasses.observers.cancelable.next(Observers.ON);
  }

  LeftSelected(id: string): void {  }
  RightSelected(id: string): void {  }
  RelationSelected(id: string): void {
    this.sharedClasses.mappingService.DeleteMappingRelation(id).subscribe((relation: MappingRelation) => { 
      this.sharedClasses.observers.updateRelations.next(relation);
    });
    this.sharedClasses.stateObserver.next(new DefaultState(this.sharedClasses));
  }
  CancelAction(): void { 
    this.sharedClasses.stateObserver.next(new DefaultState(this.sharedClasses));
  }
}

class CreateSelectLeftState implements ActionState {
  constructor(private sharedClasses: RequiredClassForActionState, private mappingRule: string) {
    sharedClasses.observers.sendAllOff();
    sharedClasses.observers.leftSideHighlight.next(Observers.ON);
    sharedClasses.observers.cancelable.next(Observers.ON);
  }

  LeftSelected(id: string): void { 
    this.sharedClasses.stateObserver.next(new CreateSelectRightState(this.sharedClasses, id, this.mappingRule));
  }
  RightSelected(id: string): void {  }
  RelationSelected(id: string): void { }
  CancelAction(): void { 
    this.sharedClasses.stateObserver.next(new DefaultState(this.sharedClasses));
  }
}

class CreateSelectRightState implements ActionState {
  constructor(private sharedClasses: RequiredClassForActionState, private leftID: string, private mappingRule: string) {
    sharedClasses.observers.sendAllOff();
    sharedClasses.observers.rightSideHighlight.next(Observers.ON);
    sharedClasses.observers.cancelable.next(Observers.ON);
  }

  LeftSelected(id: string): void { }
  RightSelected(id: string): void { 
    this.sharedClasses.mappingService.CreateMappingRelation({
      id: "00000000-0000-0000-0000-000000000000",
      mappingRule: this.mappingRule,
      attributeLeft: this.leftID,
      attributeRight: id,
    }).subscribe((relation: MappingRelation) => {
      this.sharedClasses.observers.updateRelations.next(relation);
    });
    this.sharedClasses.stateObserver.next(new DefaultState(this.sharedClasses));
   }
  RelationSelected(id: string): void { }
  CancelAction(): void { 
    this.sharedClasses.stateObserver.next(new DefaultState(this.sharedClasses));
  }
}

class Observers {
  public static readonly OFF = false;
  public static readonly ON = true;

  public readonly leftSideHighlight = new Subject<boolean>();
  public readonly rightSideHighlight = new Subject<boolean>();
  public readonly arrowsHighlight = new Subject<boolean>();
  public readonly cancelable = new Subject<boolean>();
  public readonly updateRelations = new Subject<MappingRelation>();

  public sendAllOff() {
    this.leftSideHighlight.next(Observers.OFF);
    this.arrowsHighlight.next(Observers.OFF);
    this.rightSideHighlight.next(Observers.OFF);
    this.cancelable.next(Observers.OFF);
  }
}

interface RequiredClassForActionState {
    observers: Observers;
    stateObserver: Subject<ActionState>;
    mappingService: MappingService;
}

@Injectable({
  providedIn: 'root'
})
export class MappingService implements OnDestroy {
  private readonly stateObserver = new Subject<ActionState>();
  private subscription?: Subscription;
  public readonly observers = new Observers();
  private readonly classesForActionState = {
    observers: this.observers, 
    stateObserver: this.stateObserver,
    mappingService: this,
  }
  private actionState: ActionState = new DefaultState(this.classesForActionState);
  
  constructor(private http: HttpClient) {
    this.subscription = this.stateObserver.subscribe((newState: ActionState) => {
      this.actionState = newState;
    });
  }
  
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
  
  delete() {
    if (this.actionState instanceof DeleteState) return;
    this.actionState = new DeleteState(this.classesForActionState);
  }
  
  create(mappingRuleId: string) {
    if (this.actionState instanceof CreateSelectLeftState || this.actionState instanceof CreateSelectRightState) return;
    this.actionState = new CreateSelectLeftState(this.classesForActionState, mappingRuleId);
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

  GetMappings() {
    return this.http.get<Mapping[]>('/api/mapping');
  }

  GetMapping(mappingID: string) {
    return this.http.get<Mapping>(`/api/mapping/${mappingID}`);
  }

  GetMappingRules(mappingID: string) {
    return this.http.get<MappingRule[]>(`/api/mapping/${mappingID}/mappingrules`);
  }

  GetMappingRuleForSides(mappingRuleID: string) {
    return this.http.get<ModelSidesDTO>(`/api/mappingrule/${mappingRuleID}/modelsides`);
  }

  getMappingRelations(mappingRuleID: string) {
    return this.http.get<MappingRelation[]>(`/api/mappingrule/${mappingRuleID}/relations`);
  }

  CreateMapping(mapping: Mapping) {
    return this.http.post<Mapping>('/api/mapping', mapping);
  }

  CreateMappingRule(mappingRule: MappingRule) {
    return this.http.post<MappingRule>('/api/mappingrule', mappingRule);
  }

  CreateMappingRelation(relation: MappingRelation) {
    return this.http.post<MappingRelation>(`/api/mappingrelation`, relation);
  }

  AddElementToMappingRule(mappingRuleID: string, elementID: string) {
    return this.http.post<MappingRule>('/api/mappingrule/element', {
      "mappingRule": {
        "id": mappingRuleID
      },
      "element": {
        "id": elementID
      }
    });
  }

  DeleteMapping(mappingId: string) {
    return this.http.delete<Mapping>(`/api/mapping/${mappingId}`);
  }

  DeleteMappingRule(mappingRuleId: string) {
    return this.http.delete<MappingRule>(`/api/mappingrule/${mappingRuleId}`);
  }

  DeleteMappingRelation(MappingRelationID: string) {
    return this.http.delete<MappingRelation>(`/api/mappingrelation/${MappingRelationID}`);
  }
}
