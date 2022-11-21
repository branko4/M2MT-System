import { Component, Type } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Step } from 'projects/shared/src/public-api';
import { SelectElementComponent } from '../../element-components/select-element/select-element.component';
import { MappingRuleMetaDataComponent } from './mapping-rule-meta-data/mapping-rule-meta-data.component';

@Component({
  selector: 'app-create-mapping-rule',
  templateUrl: './create-mapping-rule.component.html',
  styleUrls: ['./create-mapping-rule.component.scss']
})
export class CreateMappingRuleComponent {
  steps: Step<any>[] = [ 
    SelectElementComponent.GetReference({
      input: {
        modelRef: {
          id: "dfgb",
          id2: "dfgb"
        },
      }, 
      output: {},
    }),
    SelectElementComponent.GetReference({
      input: {
        modelRef: {
          id: "EULYNX",
          id2: "EULYNX"
        },
      }, 
      output: {},
    }),
    MappingRuleMetaDataComponent.GetReference({
      output: {},
    }),
   ];
  creatable = this.stateIsInvalid();

  constructor(private router: Router, private route: ActivatedRoute) {}

  onFormDataChanged() {
    this.creatable = this.stateIsInvalid();
  }

  stateIsInvalid(): boolean {
    
    return !(
        this.steps[0].data.output.selectedElement         !== undefined
    &&  this.steps[1].data.output.selectedElement         !== undefined
    &&  this.steps[2].data.output.selectedMappingRuleName !== undefined
    );
  }

  cancel() {
    this.router.navigate(['..'], {relativeTo: this.route});
  }

  create() {
    if (this.stateIsInvalid()) return;
    this.router.navigate(['..','new'], {relativeTo: this.route});
  }
}
