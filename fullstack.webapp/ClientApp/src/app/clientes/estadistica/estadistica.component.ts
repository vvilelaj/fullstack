import { Component, OnInit, ViewChild  } from '@angular/core';
// import { ChartDataSets, ChartOptions } from 'chart.js';
// import { Color, BaseChartDirective, Label } from 'ng2-charts';
import { EstadisticaService } from '../../services/estadistica.service';
import { ResultKpi } from '../../model/result.kpi.model';

@Component({
  selector: 'app-estadistica',
  templateUrl: './estadistica.component.html',
  styles: []
})
export class EstadisticaComponent implements OnInit {

  promedioEdad: number;
  desviacionEstandar: number;
  resultKpi: ResultKpi;

  constructor(private estadisticaService: EstadisticaService) { }

  ngOnInit() {
    this.ListarClientes();
  }

  ListarClientes() {
    return this.estadisticaService.getKpis().subscribe(
      data => {
        // console.log(data);
        this.resultKpi = data;
        this.promedioEdad = this.resultKpi.Items[0].PromedioEdad;
        this.desviacionEstandar = this.resultKpi.Items[0].DesviacionEstandar;
      }, error => console.error(error));
  }

}
