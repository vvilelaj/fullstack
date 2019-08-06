import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Cliente } from '../model/cliente.model';
import { ClienteResponse } from '../model/cliente.response.model';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  private url = 'https://fullstack-clients.azurewebsites.net/api/clients';

  constructor(private http: HttpClient) { }

  getCliente(_id: string) {
    return this.http.get(`${ this.url }/${_id}`).pipe(
      map( resp => {
        // console.log(resp);
        const key = 'Result';
        return resp[key];
      })
    );
  }

  getClientes() {
    return this.http.get(`${ this.url }`).pipe(
      map( resp => {
        // console.log(resp);
        const key = 'Result';
        return resp[key];
      })
    );
  }

  deleteCliente(_id: string) {
    return this.http.delete<ClienteResponse>(`${ this.url }/${_id}`);
  }

  postCliente( cliente: Cliente) {
    return this.http.post<ClienteResponse>(this.url, cliente);
  }

  patchCliente(cliente: Cliente) {
    console.log(cliente);
    return this.http.patch<ClienteResponse>(`${ this.url }/${cliente._id}`, cliente);
  }

  // getUser() {
  //   return this.http.get(`${ this.url2 }/users?per_page=6`).pipe(
  //     map( resp => {
  //       console.log(resp);
  //     })
  //   );
  // }
}
