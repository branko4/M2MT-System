import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Action, Table } from 'projects/shared/src/public-api';

@Component({
  selector: 'app-mapping-rule-list',
  templateUrl: './mapping-rule-list.component.html',
  styleUrls: ['./mapping-rule-list.component.scss']
})
export class MappingRuleListComponent {
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

  constructor(private router: Router, private route: ActivatedRoute) { }

  delete(index: number, that: MappingRuleListComponent) {
    console.log(that);
    console.log(`The following row is called to be deleted: ${that.data.rows[index]}`);
  }

  rowSelected(index: number, that: MappingRuleListComponent) {
    console.log(that);
    console.log(`The following row is called to be deleted: ${that.data.rows[index][0]}`);
    that.router.navigate([that.data.rows[index][0]], {relativeTo: that.route});
  }
}
