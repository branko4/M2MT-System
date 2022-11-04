import { Component, Input, OnInit } from '@angular/core';
import { MappingService } from 'projects/mapping/src/app/service/mapping.service';
import { TaxonomyElement, PropertiesElement } from 'projects/shared/src/lib/Data/dto/elements.dto';
import { Attribute } from 'projects/shared/src/lib/Data/models/attribute.model';

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
export class ElementComponent implements OnInit{
  @Input() element?: TaxonomyElement;
  @Input() modelSide: ModelSide = new LeftModelSide();

  elementWithValues?: PropertiesElement;

  constructor(private mappingService: MappingService) {}

  ngOnInit() {
    if (!this.element) return;
    this.elementWithValues = {
      name: this.element.name,
      id: this.element.id,
      attributes: [
        {
          id: "attrId01",
          name: "location",
        },
        {
          id: "attrId02",
          name: "name",
        },
        {
          id: "attrId03",
          name: "type",
        },
      ],
      ownedElements: [],
    }
  }

  onSelected(attribute: Attribute) {
    this.modelSide.onSelect(attribute, this.mappingService);
  }
}
