import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Element } from 'projects/shared/src/lib/Data/models/element.model';
import { Step, StepComponent } from 'projects/shared/src/public-api';
import { ModelService } from '../../../service/model.service';
import { PositionInList } from './empty-element/empty-element.component';

interface ExpectedData {
  input: {
    modelRef: string
  },
  output: {
    selectedElement?: {name: string}
  },
}

interface TaxonomyElement extends Element {
  childeren: TaxonomyElement[];
}

@Component({
  selector: 'app-select-element',
  templateUrl: './select-element.component.html',
  styleUrls: ['./select-element.component.scss']
})
export class SelectElementComponent implements StepComponent<ExpectedData> {
  @Output() selected = new EventEmitter<{name: string}>()
  data?: ExpectedData;

  constructor(private modelService: ModelService) {}
  
  static GetReference(data: ExpectedData): Step<ExpectedData> {
    return {
      component: SelectElementComponent,
      data: data,
    };
  }

  clicked(elementRef: {name: string}) {
    if (this.rootElement) this.updateData(elementRef);
    this.selected.emit(elementRef);
  }

  updateData(elementRef: {name: string}) {
    if (this.data === undefined) return;
    this.data.output.selectedElement = elementRef;
    this.dataChange.emit(this.data);
  }

  injectData(data: ExpectedData): void {
    this.modelService.GetElementsOfModel(data.input.modelRef).subscribe((data: Element[]) => {
      this.rootElement = this.createTaxonomy(data);
    });
    
    this.data = data;
  }

  private createTaxonomy(elements: Element[]): TaxonomyElement {
    var topElement = elements.filter((element: Element) => element.parent === "00000000-0000-0000-0000-000000000000")[0];
    var childeren = this.createTaxonomies(elements, topElement.id);
    return {
      ...topElement,
      childeren: childeren,
    }
  }

  private createTaxonomies(elements: Element[], elementID: string): TaxonomyElement[] {
    var filteredElements = elements.filter((element: Element) => element.parent === elementID);
    var taxonomyElements: TaxonomyElement[] = [];
    for (let index = 0; index < filteredElements.length; index++) {
      const element = filteredElements[index];
      taxonomyElements[index] = {
        ...element,
        childeren: this.createTaxonomies(elements, element.id),
      };
    };
    return taxonomyElements;
  }

  dataChange = new EventEmitter();
  readonly FALSE = false;

  @Input() rootElement?: TaxonomyElement;
  @Input() positionInList: PositionInList = PositionInList.FIRST;
  @Input() isRootElement = true;

  position(index: number): PositionInList {
    if (this.rootElement?.childeren.length === 1) return PositionInList.ALL;
    if (index === 0) return PositionInList.FIRST;
    if (this.rootElement?.childeren && index === this.rootElement?.childeren.length - 1) return PositionInList.LAST;
    return PositionInList.MIDDLE;
  }

}
