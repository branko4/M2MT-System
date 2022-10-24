import { Component, Input } from '@angular/core';

export class Action<T> {
  public constructor(public readonly name: string, private toBeCalled: Function, private that: T) {}

  public Clicked(index: number):void {
    this.toBeCalled(index, this.that);
  }
}

export interface Table {headers: string[], rows: any[][], actions: Action<any>[], onSelected?: Action<any>}

@Component({
  selector: 'lib-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent {
  @Input() public data: Table = {
    headers: [],
    rows: [],
    actions: [],
  }

  rowSelected(index: number) {
    if (this.data.onSelected) this.data.onSelected.Clicked(index);
  }
}
