import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MappingRule } from 'projects/shared/src/lib/Data/models/mapping-rule.model';
import { Action, Table } from 'projects/shared/src/public-api';
import { MappingService } from '../../../service/mapping.service';

@Component({
  selector: 'app-mapping-rule-list',
  templateUrl: './mapping-rule-list.component.html',
  styleUrls: ['./mapping-rule-list.component.scss']
})
export class MappingRuleListComponent implements OnInit {
  public data: Table = {
    headers: ["Identity", "Name"],
    rows: [
      [1, "Bufferstop"],
      [2, "LS"],
      [3, "LC"],
      [4, "Topology"],
    ],
    actions: [new Action("Delete", this.delete, this)],
    onSelected: new Action("Selected", this.rowSelected, this),
  }

  constructor(private router: Router, private route: ActivatedRoute, private mappingService: MappingService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.mappingService.GetMappingRules(params['mappingID']).subscribe((data: MappingRule[]) => {
        this.data.rows = [];
        var i = 0;
        data.forEach(mappingRule => {
          console.log(mappingRule);
          this.data.rows[i] = [ 
            mappingRule.id, 
            mappingRule.name, 
          ];
          i++;
        });
      });
    });
  }

  delete(index: number, that: MappingRuleListComponent) {
    that.router.navigate([that.data.rows[index][0],"delete"], {relativeTo: that.route});
  }

  rowSelected(index: number, that: MappingRuleListComponent) {
    that.router.navigate([that.data.rows[index][0]], {relativeTo: that.route});
  }
}
