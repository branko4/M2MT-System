import { Model } from "../models/model.model";
import { Protected } from "../models/protected.model";

export interface CreateMappingDTO {
  id?: string,
  name?: string,
  modelFrom?: Protected<Model>,
  modelTo?: Protected<Model>,
}