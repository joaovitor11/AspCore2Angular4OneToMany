import { Component, OnInit } from '@angular/core';
import 'rxjs/add/operator/catch';
import { EmpregadoService } from "./empregadoService";
import { Observable } from "rxjs/Observable";
import { Empregado } from "./empregadoService";

@Component({
    selector: 'empregado',
    providers: [EmpregadoService],
    templateUrl: './empregado.component.html'
})
export class EmpregadoComponent implements OnInit {
    departamento: any;
    public empregadoList: Observable<Empregado[]>;
    showEditor = true;
    myName: string;
    empregado: Empregado;
   
    constructor(private dataService: EmpregadoService) {
        this.empregado = new Empregado();
    }
    // if you want to debug info  just uncomment the console.log lines.  
    ngOnInit() {
        //    console.log("in ngOnInit");
        this.dataService.getDeps().subscribe(deps => this.departamento = deps);    
        this.empregadoList = this.dataService.empregadoList;
        this.dataService.getAll();          
    }

    public addEmpregado(item: Empregado) {
        let empregadoId = this.dataService.addEmpregado(this.empregado);
    }
    public updateEmpregado(item: Empregado) {
        this.dataService.updateEmpregado(item);
    }
    public deleteEmpregado(empregadoId: number) {
        this.dataService.removeItem(empregadoId);
    }
}  