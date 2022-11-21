import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-delete-mapping-rule',
  templateUrl: './delete-mapping-rule.component.html',
  styleUrls: ['./delete-mapping-rule.component.scss']
})
export class DeleteMappingRuleComponent {
  readonly returnLinkId = "delete-mapping-rule-return-link";

  constructor(private router: Router, private route: ActivatedRoute) { }


  delete() {
    // call to service
    this.back();
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
