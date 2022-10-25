import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateMappingRuleComponent } from './components/screen-components/create-mapping-rule/create-mapping-rule.component';
import { CreateMappingComponent } from './components/screen-components/create-mapping/create-mapping.component';
import { HomeScreenComponent } from './components/screen-components/home-screen/home-screen.component';
import { MappingListComponent } from './components/screen-components/mapping-list/mapping-list.component';
import { MappingRuleListComponent } from './components/screen-components/mapping-rule-list/mapping-rule-list.component';
import { MappingComponent } from './components/screen-components/mapping/mapping.component';

const routes: Routes = [
  { 
    path: 'mapping', 
    component: MappingListComponent, 
    children: [
      {path: 'create', component: CreateMappingComponent },
    ],
  },
  { 
    path: 'mapping/:mappingID', 
    component: MappingRuleListComponent,
    children: [
      {path: 'create', component: CreateMappingRuleComponent },
    ],
  },
  { path: 'mapping/:mappingID/:mappingRuleID', component: MappingComponent },
  { path: '', component: HomeScreenComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
