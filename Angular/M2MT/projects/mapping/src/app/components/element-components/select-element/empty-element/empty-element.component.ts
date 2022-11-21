import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

export enum PositionInList {
  FIRST,
  MIDDLE,
  LAST,
  ALL,
}

@Component({
  selector: 'app-empty-element',
  templateUrl: './empty-element.component.html',
  styleUrls: ['./empty-element.component.scss']
})
export class EmptyElementComponent implements OnInit {
  private static readonly HORIZONTAL_LINE = "hor-line";
  @Input() element: {name: string} = {name: "No element given"};
  @Input() positionInList: PositionInList = PositionInList.FIRST;
  @Input() isRootElement = true;
  @Input() isLast = true;
  @Output() clicked = new EventEmitter<{name: string}>();
  classes = EmptyElementComponent.HORIZONTAL_LINE;

  ngOnInit(): void {
    this.classes = `${EmptyElementComponent.HORIZONTAL_LINE} ${this.getClass()}`;
  }

  selected() {
    this.clicked.emit(this.element);
  }

  getClass() {
    switch (this.positionInList) {
      case PositionInList.FIRST:
        return "right";
      case PositionInList.MIDDLE:
        return "full";
      case PositionInList.LAST:
        return "left";
      case PositionInList.ALL:
        return "off"
      default:
        return "";
    }
  }

}
