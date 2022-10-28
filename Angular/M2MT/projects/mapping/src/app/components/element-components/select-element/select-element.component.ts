import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Step, StepComponent } from 'projects/shared/src/public-api';
import { PositionInList } from './empty-element/empty-element.component';

interface ExpectedData {
  input: {
    modelRef: {
      id: string,
      id2: string,
    }
  },
  output: {
    selectedElement?: {name: string}
  },
}

@Component({
  selector: 'app-select-element',
  templateUrl: './select-element.component.html',
  styleUrls: ['./select-element.component.scss']
})
export class SelectElementComponent implements StepComponent<ExpectedData> {
  @Output() selected = new EventEmitter<{name: string}>()
  data?: ExpectedData;
  
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
    // service requist
    if (data.input.modelRef.id == "EULYNX") {
        this.rootElement = {
        name: "EULYNXDataPrepInterface",
        childElements: [],
      }
    }
    this.data = data;
  }
  dataChange = new EventEmitter();
  readonly FALSE = false;

  @Input() rootElement: any = {
    name: "tBase",
    childElements: [
      {name: "tPointLocation", childElements: [
        {name: "tPointLocation", childElements: [
          {name: "tPointLocation", childElements: []},
          {name: "tPointLocation", childElements: []},
        ]},
        {name: "tPointLocation", childElements: []},
        {name: "tPointLocation", childElements: []},
      ]},
      {name: "tPointLocation", childElements: [
        {name: "tPointLocation", childElements: []},
        {name: "tPointLocation", childElements: [
          {
            name: "tBase",
            childElements: [
              {name: "tPointLocation", childElements: [
                {name: "tPointLocation", childElements: [
                  {name: "tPointLocation", childElements: []},
                  {name: "tPointLocation", childElements: []},
                ]},
                {name: "tPointLocation", childElements: []},
                {name: "tPointLocation", childElements: []},
              ]},
              {name: "tPointLocation", childElements: [
                {name: "tPointLocation", childElements: []},
                {name: "tPointLocation", childElements: []},
              ]},
              {name: "tPointLocation", childElements: [
                {name: "tPointLocation", childElements: []},
              ]},
              {name: "tPointLocation", childElements: []},
            ]
          },
        ]},
      ]},
      {name: "tPointLocation", childElements: [
        {name: "tPointLocation", childElements: []},
      ]},
      {name: "tPointLocation", childElements: []},
    ]
  }
  @Input() positionInList: PositionInList = PositionInList.FIRST;
  @Input() isRootElement = true;

  position(index: number): PositionInList {
    if (this.rootElement.childElements.length === 1) return PositionInList.ALL;
    if (index === 0) return PositionInList.FIRST;
    if (index === this.rootElement.childElements.length - 1) return PositionInList.LAST;
    return PositionInList.MIDDLE;
  }

}
