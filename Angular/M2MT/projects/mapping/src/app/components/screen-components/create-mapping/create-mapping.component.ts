import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Mapping } from 'projects/shared/src/lib/Data/models/mapping.model';
import { MappingService } from '../../../service/mapping.service';

@Component({
  selector: 'app-create-mapping',
  templateUrl: './create-mapping.component.html',
  styleUrls: ['./create-mapping.component.scss']
})
export class CreateMappingComponent implements OnInit {
  mapping: Mapping = { 
    id: "00000000-0000-0000-0000-000000000000", // placeholder for uuid, since backend should generate new uuid, however property is required
    modelFrom: "",
    modelTo: "",
    name: ""
  };
  creatable = this.stateIsInvalid();

  constructor(private router: Router, private route: ActivatedRoute, private mappingService: MappingService) { }

  ngOnInit(): void {
  }

  onFormDataChanged() {
    this.creatable = this.stateIsInvalid();
  }

  stateIsInvalid(): boolean {
    return !(
          this.mapping.modelFrom  !== undefined 
    &&    this.mapping.modelTo    !== undefined
    &&    this.mapping.name       !== undefined
    );
  }

  cancel() {
    this.router.navigate(['..'], {relativeTo: this.route});
  }

  create() {
    if (this.stateIsInvalid()) return;
    this.mappingService.CreateMapping(this.mapping).subscribe((data: Mapping) => {
      this.router.navigate(['..',data.id], {relativeTo: this.route});
    });
  }
}
