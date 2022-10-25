import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'lib-pop-up',
  templateUrl: './pop-up.component.html',
  styleUrls: ['./pop-up.component.scss']
})
export class PopUpComponent {
  @Output() backgroundClick = new EventEmitter<boolean>();

  backgroundClicked(): void {
    this.backgroundClick.emit(true);
  }
}
