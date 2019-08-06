import { Component, OnInit } from '@angular/core';
import { ClienteService } from '../services/cliente.service';
import { Cliente } from '../model/cliente.model';
import { Result } from '../model/result.model';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styles: []
})
export class ClientesComponent implements OnInit {

  clientes: Cliente[];
  result: Result;

  constructor(public clienteService: ClienteService) { }

  ngOnInit() {
    // this.Usuarios();
    this.ListarClientes();
  }

  ListarClientes() {
    return this.clienteService.getClientes().subscribe(
      data => {
        // console.log(data);
        this.result = data;
        this.clientes = this.result.Items;
      }, error => console.error(error));
  }

  borrarCliente(cliente: Cliente) {
    return this.clienteService.deleteCliente(cliente._id).subscribe(
      response => {
        if (response.Success) {
          Swal.fire('Éxito', 'Se eliminó correctamente el usuario', 'success');
          this.ListarClientes();
        } else {
          Swal.fire('Error', response.Message , 'error');
        }
      },
      error => Swal.fire('Error', error.message , 'error')
    );
  }

}
