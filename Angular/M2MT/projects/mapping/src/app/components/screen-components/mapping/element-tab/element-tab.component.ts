import { Component, Input } from '@angular/core';
import { BasicElement } from 'projects/mapping/src/app/models/element.model';

@Component({
  selector: 'app-element-tab',
  templateUrl: './element-tab.component.html',
  styleUrls: ['./element-tab.component.scss']
})
export class ElementTabComponent {
  @Input() elements: BasicElement[] = []
}
