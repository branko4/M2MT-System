import { Component, Input } from '@angular/core';
import { BasicElement } from 'projects/shared/src/lib/Data/models/element.model';

@Component({
  selector: 'app-element-tab',
  templateUrl: './element-tab.component.html',
  styleUrls: ['./element-tab.component.scss']
})
export class ElementTabComponent {
  @Input() elements: BasicElement[] = [];
}
