import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Action, Table } from 'projects/shared/src/public-api';

@Component({
  selector: 'app-mapping-list',
  templateUrl: './mapping-list.component.html',
  styleUrls: ['./mapping-list.component.scss']
})
export class MappingListComponent {
  public data: Table = {
    headers: ["Identity", "Model A", "Model B"],
    rows: [
      [1, "IMSpoor", "EULYNX"],
      [2, "PlanPro", "EULYNX"],
      [3, "SDEF", "EULYNX"],
      [4, "PlanPro", "IMSpoor"],
    ],
    actions: [new Action("Delete", this.rowSelectedToBeDeleted, this)],
    onSelected: new Action("Selected", this.rowSelected, this),
  }

  constructor(private router: Router, private route: ActivatedRoute) { }

  rowSelectedToBeDeleted(index: number, that: MappingListComponent) {
    that.router.navigate([that.data.rows[index][0],"delete"], {relativeTo: that.route});
  }

  rowSelected(index: number, that: MappingListComponent) {
    that.router.navigate([that.data.rows[index][0]], {relativeTo: that.route});
  }
}
