import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddElementComponentComponent } from './components/screen-components/add-element-component/add-element-component.component';
import { CreateMappingComponentComponent } from './components/screen-components/create-mapping-component/create-mapping-component.component';
import { CreateMappingRuleComponentComponent } from './components/screen-components/create-mapping-rule-component/create-mapping-rule-component.component';
import { MappingListComponentComponent } from './components/screen-components/mapping-list-component/mapping-list-component.component';
import { MappingRuleListComponentComponent } from './components/screen-components/mapping-rule-list-component/mapping-rule-list-component.component';
import { ModelsComponentComponent } from './components/screen-components/models-component/models-component.component';
import { HomeScreenComponentComponent } from './components/screen-components/home-screen-component/home-screen-component.component';
import { HomeHeaderComponentComponent } from './components/screen-components/home-screen-component/home-header-component/home-header-component.component';
import { MappingComponentComponent } from './components/screen-components/mapping-component/mapping-component.component';
import { ElementComponentComponent } from './components/screen-components/mapping-component/element-component/element-component.component';
import { ElementTabComponentComponent } from './components/screen-components/mapping-component/element-tab-component/element-tab-component.component';
import { MappingActionsComponentComponent } from './components/screen-components/mapping-component/mapping-actions-component/mapping-actions-component.component';
import { ModelSideComponentComponent } from './components/screen-components/mapping-component/model-side-component/model-side-component.component';
import { SvgComponentComponent } from './components/screen-components/mapping-component/svg-component/svg-component.component';
import { DeleteComponentComponent } from './components/element-components/delete-component/delete-component.component';
import { ElementListComponentComponent } from './components/element-components/element-list-component/element-list-component.component';
import { FooterComponentComponent } from './components/element-components/footer-component/footer-component.component';
import { NavBarComponentComponent } from './components/element-components/nav-bar-component/nav-bar-component.component';
import { SelectElementComponentComponent } from './components/element-components/select-element-component/select-element-component.component';
import { ElementListSelectorComponent } from './components/element-components/select-element-component/element-list-selector/element-list-selector.component';
import { ElementTaxonomySelectorComponentComponent } from './components/element-components/select-element-component/element-taxonomy-selector-component/element-taxonomy-selector-component.component';
import { EmptyElementComponentComponent } from './components/element-components/select-element-component/empty-element-component/empty-element-component.component';

@NgModule({
  declarations: [
    AppComponent,
    AddElementComponentComponent,
    CreateMappingComponentComponent,
    CreateMappingRuleComponentComponent,
    MappingListComponentComponent,
    MappingRuleListComponentComponent,
    ModelsComponentComponent,
    HomeScreenComponentComponent,
    HomeHeaderComponentComponent,
    MappingComponentComponent,
    ElementComponentComponent,
    ElementTabComponentComponent,
    MappingActionsComponentComponent,
    ModelSideComponentComponent,
    SvgComponentComponent,
    DeleteComponentComponent,
    ElementListComponentComponent,
    FooterComponentComponent,
    NavBarComponentComponent,
    SelectElementComponentComponent,
    ElementListSelectorComponent,
    ElementTaxonomySelectorComponentComponent,
    EmptyElementComponentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
