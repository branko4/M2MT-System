import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { LeftModelSide, ModelSide } from '../element/element.component';
import { ModelService } from 'projects/mapping/src/app/service/model.service';
import { Element } from 'projects/shared/src/lib/Data/models/element.model';
import { ParentRelationElement } from '../ParentRelationElement.class';

@Component({
  selector: 'app-model-side',
  templateUrl: './model-side.component.html',
  styleUrls: ['./model-side.component.scss']
})
export class ModelSideComponent implements OnInit, OnChanges {
  @Input() modelSide: ModelSide = new LeftModelSide();
  @Input() activeElement?: Element;
  loadableElement?: ParentRelationElement;

  constructor(private modelService: ModelService) { }

  ngOnInit(): void {
    if (!this.activeElement) return;
    var activeElement = this.activeElement;
    this.modelService.GetElementWithParent(activeElement.id).subscribe((elements: Element[]) => { 
      this.loadableElement = this.loadElement(elements, activeElement.id);
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.ngOnInit();
  }

  loadElement(elements: Element[], id: string):  ParentRelationElement | undefined{
    if (id == "00000000-0000-0000-0000-000000000000") return;
    var element = elements.filter(element => element.id === id)[0];
    return {
      ...element,
      parent: this.loadElement(elements, element.parent),
    }
  }
}
