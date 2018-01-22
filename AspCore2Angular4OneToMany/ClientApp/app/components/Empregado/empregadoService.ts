import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';  // debug  
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class EmpregadoService {

    public empregadoList: Observable<Empregado[]>;
    private _empregadoList: BehaviorSubject<Empregado[]>;

    private baseUrl: string;
    private dataStore: {
        empregadoList: Empregado[];

    };

    constructor(private http: Http) {
        this.baseUrl = '/api/';
        this.dataStore = { empregadoList: [] };
        this._empregadoList = <BehaviorSubject<Empregado[]>>new BehaviorSubject([]);
        this.empregadoList = this._empregadoList.asObservable();

    }

    getDeps() {
        return this.http.get('api/GetAllDepartamento')
            .map(res => res.json());
    } 
    
    getAll() {
        this.http.get(`${this.baseUrl}GetAllEmpregado`)
            .map(response => response.json())
            .subscribe(data => {
                this.dataStore.empregadoList = data;
                this._empregadoList.next(Object.assign({}, this.dataStore).empregadoList);
            }, error => console.log('Could not load empregado.'));
    }

    public addEmpregado(empregado: Empregado) {
        console.log("add empregado");
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        this.http.post(`${this.baseUrl}AddEmpregado/`, JSON.stringify(empregado), { headers: headers })
            .map(response => response.json()).subscribe(data => {
                this.dataStore.empregadoList.push(data);
                this._empregadoList.next(Object.assign({}, this.dataStore).empregadoList);
            }, error => console.log('Could not create empregado.'));
    };

    public updateEmpregado(newEmpregado: Empregado) {
        console.log("update empregado");
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        this.http.put(`${this.baseUrl}UpdateEmpregado/`, JSON.stringify(newEmpregado), { headers: headers })
            .map(response => response.json()).subscribe(data => {
                this.dataStore.empregadoList.forEach((t, i) => {
                    if (t.empregadoId === data.id) { this.dataStore.empregadoList[i] = data; }
                });
            }, error => console.log('Could not update empregado.'));
    };

    removeItem(empregadoId: number) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        this.http.delete(`${this.baseUrl}DeleteEmpregado/${empregadoId}`, { headers: headers }).subscribe(response => {
            this.dataStore.empregadoList.forEach((t, i) => {
                if (t.empregadoId === empregadoId) { this.dataStore.empregadoList.splice(i, 1); }
            });

            this._empregadoList.next(Object.assign({}, this.dataStore).empregadoList);
        }, error => console.log('Could not delete empregado.'));
    }
    private _serverError(err: any) {
        console.log('sever errorOK:', err);  // debug  
        if (err instanceof Response) {
            return Observable.throw(err.json().error || 'backend server error');
            // if you're using lite-server, use the following line  
            // instead of the line above:  
            //return Observable.throw(err.text() || 'backend server error');  
        }
        return Observable.throw(err || 'backend server error');
    }
}

export class Empregado {
    public empregadoId: number;
    public departamentoId: number;
    public nome: string;
    public sobrenome: string;
    public email: string;
}  
