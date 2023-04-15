import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MappingService } from '../../../service/mapping.service';

@Component({
  selector: 'app-delete-mapping-rule',
  templateUrl: './delete-mapping-rule.component.html',
  styleUrls: ['./delete-mapping-rule.component.scss']
})
export class DeleteMappingRuleComponent implements OnInit{
  readonly returnLinkId = "delete-mapping-rule-return-link";

  mappingRule?: string;

  constructor(private router: Router, private route: ActivatedRoute, private mappingService: MappingService) { }

  ngOnInit() {
    this.route.parent?.params.subscribe(params => {
      this.mappingRule = params['mappingRuleID'];
    });
  }

  delete() {
    if(this.mappingRule === undefined) return;
    this.mappingService.DeleteMappingRule(this.mappingRule).subscribe(() => {
      this.back();
    })
  }

  cancel() {
    this.back();
  }

  back() {
    // See comment at back() function in ../delete-mapping
    document.getElementById(this.returnLinkId)?.click();
    // this.router.navigate(["..",".."], {relativeTo: this.route});
  }

}
