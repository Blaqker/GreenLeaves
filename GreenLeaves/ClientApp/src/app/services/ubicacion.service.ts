import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Ubicacion } from '../models/ubicacion';

@Injectable({
  providedIn: 'root'
})
export class UbicacionService {

    constructor(private http: HttpClient) { }

    getUbicaciones() {
        return this.http.get<Ubicacion[]>('api/ubicaciones');
    }
}
