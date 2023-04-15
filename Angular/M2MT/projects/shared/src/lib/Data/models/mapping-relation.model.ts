import { Base } from "./base.model";

export interface MappingRelation extends Base {
  mappingRule: string;
  attributeLeft: string;
  attributeRight: string;
}