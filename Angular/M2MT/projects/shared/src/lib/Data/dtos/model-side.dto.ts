import { Element } from "../models/element.model";
import { Model } from "../models/model.model";

export interface ModelSideDTO {
  model: Model
  elements: Element[]
}