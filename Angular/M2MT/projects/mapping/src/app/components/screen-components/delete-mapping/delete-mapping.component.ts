import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-delete-mapping',
  templateUrl: './delete-mapping.component.html',
  styleUrls: ['./delete-mapping.component.scss']
})
export class DeleteMappingComponent {

  constructor(private router: Router, private route: ActivatedRoute) { }
  readonly returnLinkId = "delete-mapping-return-link";


  delete() {
    // call to service
    this.back();
  }

  cancel() {
    this.back();
  }

  back() {
    // the this.router.navigate did weirdly enough not work. Therefore it was necessary to use a (dirty) workaround.
    // The workaround is quite simple. It uses the routerLink to do the navigation. However this is less how it should be.
    //  Same problem in ../delete-mapping-rule
    // Possible fix could be to make this component a child of mappingList where the route is: /:mapping/delete. However this should be tested later on, for now this is good enough
    document.getElementById(this.returnLinkId)?.click();
    // this.router.navigate(["..",".."]);//, {relativeTo: this.route}
  }

}
