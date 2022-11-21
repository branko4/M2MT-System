import { Component, EventEmitter } from '@angular/core';
import { StepComponent } from 'projects/shared/src/public-api';

interface ExpectedData {
  output: {
    selectedMappingRuleName?: string,
  },
}

@Component({
  selector: 'app-mapping-rule-meta-data',
  templateUrl: './mapping-rule-meta-data.component.html',
  styleUrls: ['./mapping-rule-meta-data.component.scss']
})
export class MappingRuleMetaDataComponent implements StepComponent<ExpectedData> {
  data: ExpectedData = { 
    output: {},
  };

  static GetReference(data: ExpectedData) {
    return {
      component: MappingRuleMetaDataComponent,
      data: data,
    }
  }

  onFormDataChanged() {
    this.dataChange.emit(this.data);
  }

  injectData(data: ExpectedData) {
    this.data = data;
  }

  dataChange: EventEmitter<ExpectedData> = new EventEmitter();
}
