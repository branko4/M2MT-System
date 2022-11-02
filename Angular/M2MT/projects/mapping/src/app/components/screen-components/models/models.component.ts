import { Component, Output, Input, EventEmitter } from '@angular/core';
import { Model } from '../../../../../../shared/src/lib/Data/models/model.model';
import { Protected } from '../../../../../../shared/src/lib/Data/models/_protected.model';

@Component({
  selector: 'app-models',
  templateUrl: './models.component.html',
  styleUrls: ['./models.component.scss']
})
export class ModelsComponent {
  models: Protected<Model>[] = [
    {data: {name: "IMSpoor", id: "randomID"}, hash: "dfmbkg"},
    {data: {name: "EULYNX", id: "randomID2"}, hash: "dbfnakb"},
    {data: {name: "SDEF", id: "randomID3"}, hash: "mofbmni"},
    {data: {name: "SDEF", id: "randomID3"}, hash: "mofbmni"},
  ]
  @Input() name = "model";

  @Input() model?: Protected<Model>;
  @Output() modelChange = new EventEmitter<Protected<Model>>();

  modelSelected() {
    this.modelChange.emit(this.model);
  }
}
