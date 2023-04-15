import { Attribute } from "./attribute.model";
import { NamedBase } from "./base.model";

export interface  Element extends NamedBase {
  model: string;
  parent: string;
  attributes: Attribute[];
}