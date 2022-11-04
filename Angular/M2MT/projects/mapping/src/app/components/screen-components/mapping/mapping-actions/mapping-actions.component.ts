import { Component, OnDestroy, OnInit } from '@angular/core';
import { MappingService } from 'projects/mapping/src/app/service/mapping.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-mapping-actions',
  templateUrl: './mapping-actions.component.html',
  styleUrls: ['./mapping-actions.component.scss']
})
export class MappingActionsComponent implements OnInit, OnDestroy {
  subscription?: Subscription;
  cancelable = false;

  constructor(private mappingService: MappingService) { }

  ngOnInit(): void {
    this.subscription = this.mappingService.observers.cancelable.subscribe((cancelable) => {
      this.cancelable = cancelable
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onDelete() {
    this.mappingService.delete();
  }

  onCreate() {
    this.mappingService.create();
  }

  onCancel() {
    this.mappingService.cancel();
  }

}
