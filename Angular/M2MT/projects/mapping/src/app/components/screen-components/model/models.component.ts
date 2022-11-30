import { Component, Output, Input, EventEmitter, OnInit } from '@angular/core';
import { Model } from '../../../../../../shared/src/lib/Data/models/model.model';
import { ModelService } from '../../../service/model.service';

@Component({
  selector: 'app-models',
  templateUrl: './models.component.html',
  styleUrls: ['./models.component.scss']
})
export class ModelsComponent implements OnInit {
  models: Model[] = [];
  @Input() name = "model";
  disabled = true;

  @Input() model?: string;
  @Output() modelChange = new EventEmitter<string>();
  
  constructor(private modelService: ModelService) {}

  ngOnInit() {
    this.modelService.GetModels().subscribe((data: Model[]) => {
      this.models = data;
      this.disabled = false;
    });
  }

  modelSelected() {
    this.modelChange.emit(this.model);
  }
}
