import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';  // debug  
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class DepartamentoService {

    public departamentoList: Observable<Departamento[]>;
    private _departamentoList: BehaviorSubject<Departamento[]>;
    private baseUrl: string;
    private dataStore: {
        departamentoList: Departamento[];
    };

    constructor(private http: Http) {
        this.baseUrl = '/api/';
        this.dataStore = { departamentoList: [] };
        this._departamentoList = <BehaviorSubject<Departamento[]>>new BehaviorSubject([]);
        this.departamentoList = this._departamentoList.asObservable();
    }

    getAll() {
        this.http.get(`${this.baseUrl}GetAllDepartamento`)
            .map(response => response.json())
            .subscribe(data => {
                this.dataStore.departamentoList = data;
                this._departamentoList.next(Object.assign({}, this.dataStore).departamentoList);
            }, error => console.log('Could not load departamento.'));
    }

    public addDepartamento(departamento: Departamento) {
        console.log("add Departamento");
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
       
        this.http.post(`${this.baseUrl}AddDepartamento/`, JSON.stringify(departamento), { headers: headers })
            .map(response => response.json()).subscribe(data => {
                this.dataStore.departamentoList.push(data);
                this._departamentoList.next(Object.assign({}, this.dataStore).departamentoList);
            }, error => console.log('Could not create Departamento.'));
    };

    public updateDepartamento(newDepartamento: Departamento) {
        console.log("update Departamento");
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        
        this.http.put(`${this.baseUrl}UpdateDepartamento/`, JSON.stringify(newDepartamento), { headers: headers })
            .map(response => response.json()).subscribe(data => {
                this.dataStore.departamentoList.forEach((t, i) => {
                    if (t.departamentoId === data.id) { this.dataStore.departamentoList[i] = data; }
                });
            }, error => console.log('Could not update departamento.'));
    };

    removeItem(departamentoId: number) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        
        this.http.delete(`${this.baseUrl}DeleteDepartamento/${departamentoId}`, { headers: headers }).subscribe(response => {
            this.dataStore.departamentoList.forEach((t, i) => {
                if (t.departamentoId === departamentoId) { this.dataStore.departamentoList.splice(i, 1); }
            });

            this._departamentoList.next(Object.assign({}, this.dataStore).departamentoList);
        }, error => console.log('Could not delete Departamento.'));
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

export class Departamento {
    public departamentoId: number;
    public nome: string;
}  