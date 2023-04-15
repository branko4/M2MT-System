import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MappingRule } from 'projects/shared/src/lib/Data/models/mapping-rule.model';
import { Mapping } from 'projects/shared/src/lib/Data/models/mapping.model';
import { Step } from 'projects/shared/src/public-api';
import { MappingService } from '../../../service/mapping.service';
import { SelectElementComponent } from '../../element-components/select-element/select-element.component';
import { MappingRuleMetaDataComponent } from './mapping-rule-meta-data/mapping-rule-meta-data.component';

@Component({
  selector: 'app-create-mapping-rule',
  templateUrl: './create-mapping-rule.component.html',
  styleUrls: ['./create-mapping-rule.component.scss']
})
export class CreateMappingRuleComponent implements OnInit {
  steps?: Step<any>[] = undefined
  creatable = this.stateIsInvalid();
  mappingID?: string;

  constructor(private router: Router, private route: ActivatedRoute, private mappingService: MappingService) {}

  ngOnInit(): void {
    this.route.parent?.params.subscribe(params => {
      var mappingID = params['mappingID']
      this.mappingID = mappingID;
      this.mappingService.GetMapping(mappingID).subscribe((data: Mapping) => {
        this.steps = [ 
          SelectElementComponent.GetReference({
            input: {
              modelRef: data.modelFrom
            }, 
            output: {},
          }),
          SelectElementComponent.GetReference({
            input: {
              modelRef: data.modelTo,
            }, 
            output: {},
          }),
          MappingRuleMetaDataComponent.GetReference({
            output: {},
          }),
         ];
      });
    });
  }

  onFormDataChanged() {
    this.creatable = this.stateIsInvalid();
  }

  stateIsInvalid(): boolean {
    return !(
        this.steps === undefined
    ||  this.steps[0].data.output.selectedElement         !== undefined
    &&  this.steps[1].data.output.selectedElement         !== undefined
    &&  this.steps[2].data.output.selectedMappingRuleName !== undefined
    &&  this.mappingID                                    !== undefined
    );
  }

  cancel() {
    this.router.navigate(['..'], {relativeTo: this.route});
  }

  create() {
    // TODO FIXME add create statement
    if (this.stateIsInvalid()) return;
    if (this.steps === undefined || this.mappingID === undefined) return;
    this.mappingService.CreateMappingRule( 
      {
        id: "00000000-0000-0000-0000-000000000000", 
        name: this.steps[2].data.output.selectedMappingRuleName, 
        mapping: this.mappingID, 
        elements: [
          { ID: this.steps[0].data.output.selectedElement.id, },
          { ID: this.steps[1].data.output.selectedElement.id, },
        ],
      } ).subscribe((data: MappingRule) => {
        this.router.navigate(['..',data.id], {relativeTo: this.route});
    });
  }
}
