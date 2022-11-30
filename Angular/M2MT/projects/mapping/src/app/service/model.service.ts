import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Element } from 'projects/shared/src/lib/Data/models/element.model';
import { Model } from 'projects/shared/src/lib/Data/models/model.model';

@Injectable({
  providedIn: 'root'
})
export class ModelService {

  constructor(private http: HttpClient) { }

  GetModel(ID: string) {
    return this.http.get<Model>(`/api/informationmodel/${ID}`);
  }

  GetModels() {
    return this.http.get<Model[]>(`/api/informationmodel`);
  }

  GetElementsOfModel(ID: string) {
    return this.http.get<Element[]>(`/api/informationmodel/${ID}/elements`);
  }

  GetElementWithParent(ID : string) {
    return this.http.get<Element[]>(`/api/element/${ID}/parents`);
  }
}
