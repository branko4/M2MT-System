import { Attribute } from "./attribute.model";

export interface  Element {
  id: string; 
  name: string;
  model: string;
  parent: string;
  attributes: Attribute[];
}