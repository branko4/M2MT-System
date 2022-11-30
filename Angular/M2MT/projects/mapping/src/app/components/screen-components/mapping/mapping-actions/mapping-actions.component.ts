import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MappingService } from 'projects/mapping/src/app/service/mapping.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-mapping-actions',
  templateUrl: './mapping-actions.component.html',
  styleUrls: ['./mapping-actions.component.scss']
})
export class MappingActionsComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  cancelable = false;
  mappingRuleID?: string;

  constructor(private mappingService: MappingService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscriptions.push(
      this.mappingService.observers.cancelable.subscribe((cancelable) => {
        this.cancelable = cancelable
      })
    );
    this.subscriptions.push(
      this.route.params.subscribe((params) => {this.mappingRuleID = params["mappingRuleID"]})
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    })
  }

  onDelete() {
    this.mappingService.delete();
  }

  onCreate() {
    if (!this.mappingRuleID) return;
    this.mappingService.create(this.mappingRuleID);
  }

  onCancel() {
    this.mappingService.cancel();
  }

}
