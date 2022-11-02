import { Component, Input } from '@angular/core';
import { TaxonomyElement } from 'projects/shared/src/lib/Data/dto/elements.dto';

@Component({
  selector: 'app-element',
  templateUrl: './element.component.html',
  styleUrls: ['./element.component.scss']
})
export class ElementComponent {
  @Input() element?: TaxonomyElement;
}
