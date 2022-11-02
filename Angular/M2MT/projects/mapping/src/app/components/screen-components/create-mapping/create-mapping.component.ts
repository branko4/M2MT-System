import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateMapping } from '../../../../../../shared/src/lib/Data/dto/mappings.dto';

@Component({
  selector: 'app-create-mapping',
  templateUrl: './create-mapping.component.html',
  styleUrls: ['./create-mapping.component.scss']
})
export class CreateMappingComponent {
  mapping: CreateMapping = {};
  creatable = this.stateIsInvalid();

  constructor(private router: Router, private route: ActivatedRoute) { }

  onFormDataChanged() {
    this.creatable = this.stateIsInvalid();
  }

  stateIsInvalid(): boolean {
    // since the hash represents the entire object and both objects should contain the same data, it should be enhoug to verify the hash
    return !(
          this.mapping.modelFrom?.hash  !== this.mapping.modelTo?.hash 
    &&    this.mapping.modelFrom  !== undefined 
    &&    this.mapping.modelTo    !== undefined 
    &&    this.mapping.name       !== undefined
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
