import { NamedBase } from "./base.model";

export interface MappingRule extends NamedBase {
  mapping: string;
  elements: { ID: string }[];
}