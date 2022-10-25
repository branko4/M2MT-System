import { Component, Type } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StepComponent } from 'projects/shared/src/public-api';
import { SelectElementComponent } from '../../element-components/select-element/select-element.component';
import { ModelsComponent } from '../models/models.component';

@Component({
  selector: 'app-create-mapping',
  templateUrl: './create-mapping.component.html',
  styleUrls: ['./create-mapping.component.scss']
})
export class CreateMappingComponent {
  steps: Type<StepComponent>[] = [ ModelsComponent, ModelsComponent, SelectElementComponent ];

  constructor(private router: Router, private route: ActivatedRoute) { }

  test(event: boolean) {
    if (event) this.cancel();
  }

  cancel() {
    this.router.navigate(['..'], {relativeTo: this.route});
  }

  create() {
    this.router.navigate(['..','new'], {relativeTo: this.route});
  }
}
