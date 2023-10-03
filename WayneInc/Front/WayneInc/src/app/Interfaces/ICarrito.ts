import { ICarritoItem } from "./ICarritoItem";

export interface ICarrito{
  idCarrito:number,
  total:number,
  items?:ICarritoItem[]
}
