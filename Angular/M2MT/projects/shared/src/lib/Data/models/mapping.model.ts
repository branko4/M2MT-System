import { NamedBase } from "./base.model";

export interface Mapping extends NamedBase{
  modelFrom: string;
  modelTo: string;
}