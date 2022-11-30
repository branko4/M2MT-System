import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MappingService } from 'projects/mapping/src/app/service/mapping.service';
import { MappingRelation } from 'projects/shared/src/lib/Data/models/mapping-relation.model';
import { MappingRule } from 'projects/shared/src/lib/Data/models/mapping-rule.model';
import { Subscription } from 'rxjs';

interface Coordinate {x:number, y:number}
interface ViewRelation {relation: MappingRelation, left: Coordinate, right: Coordinate }

@Component({
  selector: 'app-svg',
  templateUrl: './svg.component.svg',
  styleUrls: ['./svg.component.scss']
})
export class SvgComponent implements OnInit, OnDestroy {
  relations: MappingRelation[] = [];
  private subscriptions: Subscription[] = [];
  viewRelations: ViewRelation[] = [];
  highlight: boolean = false;
  offsetRightSide:number = 200
  offsetTopRightSide: number = 0;
  offsetTopLeftSide: number = 0;

  clicked(relation: MappingRelation) {
    this.mappingService.RelationSelected(relation);
  }

  constructor(private mappingService: MappingService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscriptions.push(
      this.route.params.subscribe((params) => {
        this.loadRelations(params['mappingRuleID']);
      })
    );
    this.subscriptions.push(
      this.mappingService.observers.updateRelations.subscribe((newestCreatedRelation: MappingRelation) => {
        this.loadRelations(newestCreatedRelation.mappingRule);
      })
    );
    this.subscriptions.push(
      this.mappingService.observers.arrowsHighlight.subscribe((highlight) => {
        this.highlight = highlight;
      })
    );
    this.loadSetup();
  }

  loadSetup(): {leftSide: {top: number, width: number}, rightSide: {left: number, top: number}} {
    var leftElement = document.getElementById("app-model-side-left");
    var rightElement = document.getElementById("app-model-side-right");
    if (!(rightElement)) throw "right app-model-side not found";
    if (!(leftElement)) throw "left app-model-side not found";
    this.offsetRightSide = rightElement.offsetLeft;
    this.offsetTopRightSide = rightElement.offsetTop;
    this.offsetTopLeftSide = leftElement.offsetTop;
    return {
      leftSide: {
        top: leftElement.offsetTop,
        width: leftElement.offsetWidth,
      },
      rightSide: {
        top: rightElement.offsetTop,
        left: rightElement.offsetLeft,
      }
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
  }

  loadRelations(mappingRuleID: string) {
    this.mappingService.getMappingRelations(mappingRuleID).subscribe((relations: MappingRelation[]) => {
      this.relations = relations;
    })
  }

  getXY(attributeID: string) {
    var element = document.getElementById(`attribute-${attributeID}`);

    if (element == null) return {x: 0, y: 0, width: 0, height: 0};
    return { y: element.offsetTop, x: element.offsetLeft, width: element.offsetWidth, height: element.offsetHeight };
  }

  private heightOffset = 0;

  formatPath(relation : MappingRelation) {
    var offsets = this.loadSetup();
    var left = this.getXY(relation.attributeLeft);
    var right = this.getXY(relation.attributeRight);

    if (!this.heightOffset) this.heightOffset = left.height/2;

    return `M ${left.x + left.width},${left.y + offsets.leftSide.top + this.heightOffset} L ${right.x + offsets.rightSide.left},${right.y + offsets.rightSide.top + this.heightOffset}`;
  }
}
