import { Component, EventEmitter, Output, Input } from '@angular/core';

export class ConfomationInformation {
  constructor(
    public readonly title: string, 
    public readonly isDelete = false,
    public readonly actionMessage: string,
    public readonly message?: string,
    public readonly value?: string,
    ) {}
}

@Component({
  selector: 'lib-conformation-pop-up',
  templateUrl: './conformation-pop-up.component.html',
  styleUrls: ['./conformation-pop-up.component.scss']
})
export class ConformationPopUpComponent {
  @Input() conformationInformation: ConfomationInformation = { title: "Delete x", isDelete: true, value: "random name id", actionMessage: "Delete"};
  @Output() canceled = new EventEmitter();
  @Output() conformed = new EventEmitter();
  inputValue:any = "";

  notExecutable = !this.isExecutable();

  cancel() {
    this.canceled.emit();
  }

  executeAction() {
    this.conformed.emit();
  }

  onFormDataChanged() {
    this.notExecutable = !this.isExecutable();
  }

  isExecutable(): boolean {
    const inputIsSameAsValue = (this.conformationInformation.value === this.inputValue);

    return !this.conformationInformation.isDelete || inputIsSameAsValue;

  }
}
