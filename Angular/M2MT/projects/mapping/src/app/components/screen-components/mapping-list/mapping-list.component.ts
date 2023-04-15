import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Action, Table } from 'projects/shared/src/public-api';
import { MappingService } from '../../../service/mapping.service';
import { ModelService } from '../../../service/model.service';
import { Model } from 'projects/shared/src/lib/Data/models/model.model';
import { Mapping } from 'projects/shared/src/lib/Data/models/mapping.model';

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

  constructor(private router: Router, private route: ActivatedRoute, private mappingService: MappingService, private modelService: ModelService) { }

  ngOnInit() {
    this.mappingService.GetMappings().subscribe((data: Mapping[]) => {
      this.data.rows = [];
      var i = 0;

      data.forEach(mapping => {
        this.data.rows[i] =[ 
          mapping.id, 
          'loading...', 
          'loading...'
        ];
        this.updateRow(mapping, i)
        i++;
      });
    });
  }

  updateRow(mapping: Mapping, row: number) {
    this.modelService.GetModel(mapping.modelFrom).subscribe((data: Model) => {
      this.data.rows[row][1] = data.name;
    });
    this.modelService.GetModel(mapping.modelTo).subscribe((data: Model) => {
      this.data.rows[row][2] = data.name;
    });
  }

  rowSelectedToBeDeleted(index: number, that: MappingListComponent) {
    that.router.navigate([that.data.rows[index][0],"delete"], {relativeTo: that.route});
  }

  rowSelected(index: number, that: MappingListComponent) {
    that.router.navigate([that.data.rows[index][0]], {relativeTo: that.route});
  }
}
