import { Attribute } from "../models/attribute.model";
import { BasicElement } from "../models/element.model";

export interface TaxonomyElement extends BasicElement {
  parent?: TaxonomyElement;
  childs: TaxonomyElement[];
}

export interface PropertiesElement extends BasicElement {
  attributes: Attribute[];
  ownedElements: BasicElement[];
}