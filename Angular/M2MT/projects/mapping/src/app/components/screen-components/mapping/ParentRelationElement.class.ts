import { Attribute } from "projects/shared/src/lib/Data/models/attribute.model";

export interface ParentRelationElement {
  id: string,
  name: string,
  parent?: ParentRelationElement,
  attributes: Attribute[],
}