import { NgModule }                               from '@angular/core';
import { BrowserModule }                          from '@angular/platform-browser';
import { FormsModule }                            from '@angular/forms';
import { HttpClientModule }                       from '@angular/common/http';
import { SharedModule }                           from 'projects/shared/src/public-api';

import { AppRoutingModule }                       from './app-routing.module';
import { AppComponent }                           from './app.component';
import { AddElementComponent }                    from './components/screen-components/add-element/add-element.component';
import { CreateMappingComponent }                 from './components/screen-components/create-mapping/create-mapping.component';
import { CreateMappingRuleComponent }             from './components/screen-components/create-mapping-rule/create-mapping-rule.component';
import { MappingListComponent }                   from './components/screen-components/mapping-list/mapping-list.component';
import { MappingRuleListComponent }               from './components/screen-components/mapping-rule-list/mapping-rule-list.component';
import { ModelsComponent }                        from './components/screen-components/model/models.component';
import { HomeScreenComponent }                    from './components/screen-components/home-screen/home-screen.component';
import { HomeHeaderComponent }                    from './components/screen-components/home-screen/home-header/home-header.component';
import { MappingComponent }                       from './components/screen-components/mapping/mapping.component';
import { ElementComponent }                       from './components/screen-components/mapping/element/element.component';
import { ElementTabComponent }                    from './components/screen-components/mapping/element-tab/element-tab.component';
import { MappingActionsComponent }                from './components/screen-components/mapping/mapping-actions/mapping-actions.component';
import { ModelSideComponent }                     from './components/screen-components/mapping/model-side/model-side.component';
import { SvgComponent }                           from './components/screen-components/mapping/svg/svg.component';
import { DeleteComponent }                        from './components/element-components/delete/delete.component';
import { ElementListComponent }                   from './components/element-components/element-list/element-list.component';
import { FooterComponent }                        from './components/element-components/footer/footer.component';
import { NavBarComponent }                        from './components/element-components/nav-bar/nav-bar.component';
import { SelectElementComponent }                 from './components/element-components/select-element/select-element.component';
import { ElementListSelectorComponent }           from './components/element-components/select-element/element-list-selector/element-list-selector.component';
import { ElementTaxonomySelectorComponent }       from './components/element-components/select-element/element-taxonomy-selector/element-taxonomy-selector.component';
import { EmptyElementComponent }                  from './components/element-components/select-element/empty-element/empty-element.component';
import { MappingRuleMetaDataComponent } from './components/screen-components/create-mapping-rule/mapping-rule-meta-data/mapping-rule-meta-data.component';
import { DeleteMappingComponent } from './components/screen-components/delete-mapping/delete-mapping.component';
import { DeleteMappingRuleComponent } from './components/screen-components/delete-mapping-rule/delete-mapping-rule.component';

@NgModule({
  declarations: [
    AppComponent,
    AddElementComponent,
    CreateMappingComponent,
    CreateMappingRuleComponent,
    MappingListComponent,
    MappingRuleListComponent,
    ModelsComponent,
    HomeScreenComponent,
    HomeHeaderComponent,
    MappingComponent,
    ElementComponent,
    ElementTabComponent,
    MappingActionsComponent,
    ModelSideComponent,
    SvgComponent,
    DeleteComponent,
    ElementListComponent,
    FooterComponent,
    NavBarComponent,
    SelectElementComponent,
    ElementListSelectorComponent,
    ElementTaxonomySelectorComponent,
    EmptyElementComponent,
    MappingRuleMetaDataComponent,
    DeleteMappingComponent,
    DeleteMappingRuleComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
