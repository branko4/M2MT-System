import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Action, Table } from 'projects/shared/src/public-api';

@Component({
  selector: 'app-mapping-list',
  templateUrl: './mapping-list.component.html',
  styleUrls: ['./mapping-list.component.scss']
})
export class MappingListComponent implements OnInit {
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

  ngOnInit(): void {
  }

  rowSelectedToBeDeleted(index: number, that: MappingListComponent) {
    console.log(that);
    console.log(`The following row is called to be deleted: ${that.data.rows[index]}`);
  }

  rowSelected(index: number, that: MappingListComponent) {
    console.log(that);
    console.log(`The following row is called to be deleted: ${that.data.rows[index][0]}`);
    that.router.navigate([that.data.rows[index][0]], {relativeTo: that.route});
  }
}
