import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { MappingService } from '../../../service/mapping.service';
import { LeftModelSide, RightModelSide } from './element/element.component';

@Component({
  selector: 'app-mapping',
  templateUrl: './mapping.component.html',
  styleUrls: ['./mapping.component.scss']
})
export class MappingComponent implements OnInit, OnDestroy {
  readonly LEFT_MODEL_SIDE = new LeftModelSide();
  readonly RIGHT_MODEL_SIDE = new RightModelSide();
  private subscriptions?: Subscription[] = [];
  
  highLightLeft = false;
  highLightRight = false;
  highLightArrows = false;

  constructor(private mappingService: MappingService) {}

  ngOnInit(): void {
    this.subscriptions?.push(
      this.mappingService.observers.leftSideHighlight.subscribe((highLight) => {
        this.highLightLeft = highLight;
      })
    );
    this.subscriptions?.push(
      this.mappingService.observers.rightSideHighlight.subscribe((highLight) => {
        this.highLightRight = highLight;
      })
    );
    this.subscriptions?.push(
      this.mappingService.observers.arrowsHighlight.subscribe((highLight) => {
        this.highLightArrows = highLight;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions?.forEach(element => {
      element.unsubscribe();
    });
  }

}
