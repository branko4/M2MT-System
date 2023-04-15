import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Element } from 'projects/shared/src/lib/Data/models/element.model';

@Component({
  selector: 'app-element-tab',
  templateUrl: './element-tab.component.html',
  styleUrls: ['./element-tab.component.scss']
})
export class ElementTabComponent {
  @Input() elements: Element[] = [];
  @Input() activeElement?: Element;
  @Input() modelID: string = "";
  @Output() activeElementChange = new EventEmitter<Element>()

  changeActiveElement(element: Element) {
    this.activeElementChange.emit(element);
  }
}
