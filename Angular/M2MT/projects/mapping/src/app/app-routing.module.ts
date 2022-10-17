import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeScreenComponent } from './components/screen-components/home-screen/home-screen.component';
import { MappingListComponent } from './components/screen-components/mapping-list/mapping-list.component';
import { MappingRuleListComponent } from './components/screen-components/mapping-rule-list/mapping-rule-list.component';
import { MappingComponent } from './components/screen-components/mapping/mapping.component';

const routes: Routes = [
  { path: 'mapping', component: MappingListComponent },
  { path: 'mapping/:mappingID', component: MappingRuleListComponent },
  { path: 'mapping/:mappingID/:mappingRuleID', component: MappingComponent },
  { path: '', component: HomeScreenComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
