import { Component, Input } from '@angular/core';
import { MappingService } from 'projects/mapping/src/app/service/mapping.service';
import { Attribute } from 'projects/shared/src/lib/Data/models/attribute.model';
import { Element } from 'projects/shared/src/lib/Data/models/element.model';
import { ParentRelationElement } from '../ParentRelationElement.class';

export interface ModelSide {
  onSelect(attribute: Attribute, mappingService: MappingService): void;
}

export class RightModelSide implements ModelSide {
  onSelect(attribute: Attribute, mappingService: MappingService): void {
    mappingService.RightSelected(attribute);
  }
}

export class LeftModelSide implements ModelSide {
  onSelect(attribute: Attribute, mappingService: MappingService): void {
    mappingService.LeftSelected(attribute);
  }
}

@Component({
  selector: 'app-element',
  templateUrl: './element.component.html',
  styleUrls: ['./element.component.scss']
})
export class ElementComponent {
  @Input() element?: ParentRelationElement;
  @Input() modelSide: ModelSide = new LeftModelSide();

  elementWithValues?: Element;

  constructor(private mappingService: MappingService) {}

  onSelected(attribute: Attribute) {
    this.modelSide.onSelect(attribute, this.mappingService);
  }
  
  formatAttributeID(attribute: Attribute) {
    return `attribute-${attribute.id}`;
  }
}
