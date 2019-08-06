
import { Kpi } from './kpi.model';

export class ResultKpi {
    constructor(
        public PageIndex: number,
        public PageSize: number,
        public TotalPages: number,
        public Items: Kpi[],
        public TotalItems: number
    ) {}
}
