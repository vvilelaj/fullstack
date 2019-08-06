import { Component, OnInit } from '@angular/core';
import { Cliente } from '../../model/cliente.model';
import { ClienteService } from '../../services/cliente.service';
import { DatePipe } from '@angular/common';
import { FormGroup, FormControl, Validators } from '../../../../node_modules/@angular/forms';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styles: [],
  providers: [
    DatePipe
  ]
})
export class ClienteComponent implements OnInit {

  formulario: FormGroup;
  cliente: Cliente;
  id: string;
  esEditar: boolean;

  constructor(private clienteService: ClienteService,
              private activatedRoute: ActivatedRoute,
              private datePipe: DatePipe) { }

  ngOnInit() {

    this.formulario = new FormGroup({
      'Nombre': new FormControl('', Validators.required),
      'Apellido': new FormControl('', Validators.required),
      'FechaNacimiento': new FormControl('', Validators.required),
    });
    this.esEditar = false;

    this.cargarInfoCliente();
  }


  guardarCliente() {
    this.cliente = this.formulario.value;

    if ( !this.esEditar) {
      return this.agregarCliente (this.cliente);
    } else {
      return this.actualizarCliente (this.cliente);
    }
  }

  agregarCliente (cliente: Cliente) {
    return this.clienteService.postCliente(this.cliente).subscribe(
      response => {
        if (response.Success) {
          Swal.fire('Éxito', 'Cliente agregado', 'success');
          this.formulario.reset();
        } else {
          Swal.fire('Error', response.Message , 'error');
        }
      },
      error => Swal.fire('Error', error.message , 'error')
    );
  }

  actualizarCliente(cliente: Cliente) {
    cliente._id = this.id;
    return this.clienteService.patchCliente(cliente).subscribe(
      response => {
        console.log(response);
        if (response.Success) {
          Swal.fire('Éxito', 'Cliente actulizado correctamente', 'success');
        } else {
          Swal.fire('Error', response.Message , 'error');
        }
      },
      error => Swal.fire('Error', error.message , 'error')
    );
  }

  cargarInfoCliente() {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
        this.clienteService.getCliente(this.id).subscribe(
          response => {
            this.cliente = response;
            this.formulario.controls['Nombre'].setValue(this.cliente.Nombre);
            this.formulario.controls['Apellido'].setValue(this.cliente.Apellido);

            // this.datePipe.transform(this.cliente.FechaNacimiento, 'yyyy-dd-MM');

            this.formulario.controls['FechaNacimiento'].setValue(this.datePipe.transform(this.cliente.FechaNacimiento, 'yyyy-MM-dd'));
            this.esEditar = true;
          },
          error => console.log(error.message)
        );
    }
  }

}
