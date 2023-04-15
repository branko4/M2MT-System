import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModelSidesDTO } from 'projects/shared/src/lib/Data/dtos/model-sides.dto';
import { Element } from 'projects/shared/src/lib/Data/models/element.model';
import { Subscription } from 'rxjs';
import { MappingService } from '../../../service/mapping.service';
import { ModelService } from '../../../service/model.service';
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
  mappingRule?: ModelSidesDTO;

  elementsLeft: Element[] = [];
  elementsRight: Element[] = [];
  modelIDLeft: string = "";
  modelIDRight: string = "";

  activeElementLeft?: Element;
  activeElementRight?: Element;
  
  highLightLeft = false;
  highLightRight = false;
  highLightArrows = false;

  constructor(private route: ActivatedRoute, private mappingService: MappingService, private modelService: ModelService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      var mappingRuleID = params['mappingRuleID'];
      this.mappingService.GetMappingRuleForSides(mappingRuleID).subscribe((mappingRule: ModelSidesDTO) => {
        this.mappingRule = mappingRule;
        this.elementsLeft = mappingRule.left.elements;
        this.elementsRight = mappingRule.right.elements;
        this.activeElementLeft = this.elementsLeft[0];
        this.activeElementRight = this.elementsRight[0];
        
        this.modelIDLeft = mappingRule.left.model.id;
        this.modelIDRight = mappingRule.right.model.id;
      });
    });

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

  leftActiveElementChange(element: Element) {
    if (element === this.activeElementLeft || element === undefined) return;
    this.activeElementLeft = element;
  }

  rightActiveElementChange(element: Element) {
    if (element === this.activeElementRight || element === undefined) return;
    this.activeElementRight = element;
  }

  ngOnDestroy(): void {
    this.subscriptions?.forEach(element => {
      element.unsubscribe();
    });
  }
}
