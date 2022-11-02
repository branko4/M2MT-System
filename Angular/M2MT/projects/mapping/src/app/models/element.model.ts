export interface BasicElement {
  name: string;
  id: string;
}

export interface Element extends BasicElement {
  parent?: Element;
  childs: Element[];
}