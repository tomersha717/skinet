import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators'
import { ShopParams } from '../shared/models/shopParams';


@Injectable({
  providedIn: 'root'
})
export class ShopService {


  baseUrl = 'https://localhost:5001/api/';

  constructor(private _http : HttpClient) { }




  public getProducts (shopParams: ShopParams){
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append('brandId',shopParams.brandId);
    }

    if (shopParams.typeId !== 0) {
      params = params.append('typeId',shopParams.typeId);
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort',shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());


    return this._http.get<IPagination>(this.baseUrl + 'products',{observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }


  public getBrands (){
    return this._http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  public getTypes (){
    return this._http.get<IType[]>(this.baseUrl + 'products/types');
  }






}
