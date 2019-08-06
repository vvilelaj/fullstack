
export class Cliente {
    constructor(
        public _id: string,
        public Nombre: string,
        public Apellido: string,
        public FechaNacimiento: string,
        public Edad: number,
        public FechaProbableMuerte: string
    ) {}
}
