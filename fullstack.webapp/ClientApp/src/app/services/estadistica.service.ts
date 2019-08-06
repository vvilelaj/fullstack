import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ResultKpi } from '../model/result.kpi.model';

@Injectable({
  providedIn: 'root'
})
export class EstadisticaService {

  private url = 'https://fullstack-kpis.azurewebsites.net/api/kpis';

  constructor(private http: HttpClient) { }


  getKpis() {
    return this.http.get(`${ this.url }`).pipe(
        map( resp => {
          // console.log(resp);
          const key = 'Result';
          return resp[key];
        })
      );
  }

}
