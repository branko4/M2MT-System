import { Component, Input, OnInit } from '@angular/core';
import { BasicElement } from 'projects/shared/src/lib/Data/models/element.model';
import { TaxonomyElement } from 'projects/shared/src/lib/Data/dto/elements.dto';
import { LeftModelSide, ModelSide } from '../element/element.component';

@Component({
  selector: 'app-model-side',
  templateUrl: './model-side.component.html',
  styleUrls: ['./model-side.component.scss']
})
export class ModelSideComponent implements OnInit {
  @Input() model: { elements: BasicElement[]} = { 
    elements: [
      { name: "Bufferstop", id: "randomID123", },
      { name: "VehicleStop", id: "randomID124", },
    ], 
  };
  @Input() modelSide: ModelSide = new LeftModelSide();
  activeElement?: BasicElement;
  loadableElement?: TaxonomyElement;

  constructor() { }

  ngOnInit(): void {
    this.activeElement = this.model.elements[0];
    this.loadElement();
  }

  loadElement() {
    if (!this.activeElement) return;
    const element = this.activeElement;
    // server call
    this.loadableElement = {
      parent: {
        name: "VehicleStop",
        id: "randomId45",
        parent: {
          name: "TrackAsset",
          id: "randomId032",
          parent: {
            name: "EULYNX::Base",
            id: "randomId20",
            parent: {
              name: "RSM::Base",
              id: "randomId20",
              childs: [],
            },
            childs: [],
          },
          childs: [],
        },
        childs: [],
      },
      childs: [],
      ...element,
    };
  }
}
