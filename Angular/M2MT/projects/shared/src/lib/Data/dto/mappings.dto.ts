import { Mapping } from "../models/mapping.model";
import { Model } from "../models/model.model";
import { Protected } from "../models/_protected.model";

export interface CreateMapping {
  id?: string,
  name?: string,
  modelFrom?: Protected<Model>,
  modelTo?: Protected<Model>,
}