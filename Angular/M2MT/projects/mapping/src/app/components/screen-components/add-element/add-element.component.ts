import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Element } from 'projects/shared/src/lib/Data/models/element.model';
import { MappingService } from '../../../service/mapping.service';
import { MappingRule } from 'projects/shared/src/lib/Data/models/mapping-rule.model';
import { Base } from 'projects/shared/src/lib/Data/models/base.model';

@Component({
  selector: 'app-add-element',
  templateUrl: './add-element.component.html',
  styleUrls: ['./add-element.component.scss']
})
export class AddElementComponent implements OnInit {
  addable = false;
  modelRef?: string = undefined;
  selectedElement?: Base;
  mappingRuleID: string = "";

  constructor(private router: Router, private route: ActivatedRoute, private mappingService: MappingService) {  }

  ngOnInit(): void {
    var modelRefOrNull = this.route.snapshot.paramMap.get('modelID');
    if (modelRefOrNull !== null) this.modelRef = modelRefOrNull;

    this.route.parent?.params.subscribe(params => {
      var mappingRuleID = params['mappingRuleID']
      if (mappingRuleID !== undefined) this.mappingRuleID = mappingRuleID;
    });
  }

  selected(element: Base) {
    this.selectedElement = element;
    this.addable = (this.selectedElement !== undefined && this.selectedElement.id !== "");
  }

  add() {
    if (this.selectedElement === undefined) return;
    this.mappingService.AddElementToMappingRule(this.mappingRuleID, this.selectedElement.id).subscribe((data: MappingRule) => {
      this.router.navigate(['../..'], {relativeTo: this.route});
    });
  }

  cancel() {
    this.router.navigate(['../..'], {relativeTo: this.route});
  }
}
