import { Component, OnInit } from '@angular/core';
import 'rxjs/add/operator/catch';
import { DepartamentoService } from "./departamentoService";
import { Observable } from "rxjs/Observable";
import { Departamento } from "./departamentoService"

@Component({
    selector: 'departamento',
    providers: [DepartamentoService],
    templateUrl: './departamento.component.html'
})
export class DepartamentoComponent implements OnInit {
    public departamentoList: Observable<Departamento[]>;

    showEditor = true;
    myName: string;
    departamento: Departamento;
    constructor(private dataService: DepartamentoService) {
        this.departamento = new Departamento();
    }
    // if you want to debug info  just uncomment the console.log lines.  
    ngOnInit() {
        //    console.log("in ngOnInit");  
        this.departamentoList = this.dataService.departamentoList;
        this.dataService.getAll();
    }
    public addDepartamento(item: Departamento) {
        let departamentoId = this.dataService.addDepartamento(this.departamento);
    }
    public updateDepartamento(item: Departamento) {
        this.dataService.updateDepartamento(item);
    }
    public deleteDepartamento(departamentoId: number) {
        this.dataService.removeItem(departamentoId);
    }
}  