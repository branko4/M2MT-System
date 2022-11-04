import { Component, OnInit, OnDestroy } from '@angular/core';
import { MappingService } from 'projects/mapping/src/app/service/mapping.service';
import { MappingRelation } from 'projects/shared/src/lib/Data/models/mapping-relation.model';
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
  private subscription?: Subscription;
  viewRelations: ViewRelation[] = [
    {
      relation: {
        id: "toBeDeleted",
      }, 
      left: {x: 30, y: 100}, 
      right: {x: 700, y: 200},
    },
    {
      relation: {
        id: "toBeDeleted2", 
      },
      left: {x: 30, y: 200}, 
      right: {x: 700, y: 300},
    },
  ];
  highlight: boolean = false;

  clicked(relation: ViewRelation) {
    this.mappingService.RelationSelected(relation.relation);
  }

  constructor(private mappingService: MappingService) { }

  ngOnInit(): void {
    this.subscription = this.mappingService.observers.arrowsHighlight.subscribe((highlight) => {
      this.highlight = highlight;
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  formatPath(relation : ViewRelation) {
    return `M ${relation.left.x},${relation.left.y} L ${relation.right.x},${relation.right.y}`;
  }
}
