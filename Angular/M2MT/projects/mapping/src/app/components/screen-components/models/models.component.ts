import { Component, OnInit } from '@angular/core';
import { StepComponent } from 'projects/shared/src/public-api';

@Component({
  selector: 'app-models',
  templateUrl: './models.component.html',
  styleUrls: ['./models.component.scss']
})
export class ModelsComponent implements OnInit, StepComponent {
  models: {name: string, ID: string}[] = [
    {name: "IMSpoor", ID: "randomID"},
    {name: "EULYNX", ID: "randomID2"},
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
