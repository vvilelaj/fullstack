import { Cliente } from './cliente.model';

export class Result {
    constructor(
        public PageIndex: number,
        public PageSize: number,
        public TotalPages: number,
        public Items: Cliente[],
        public TotalItems: number
    ) {}
}
