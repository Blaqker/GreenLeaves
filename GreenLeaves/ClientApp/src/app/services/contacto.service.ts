import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Contacto } from '../models/contacto';

@Injectable({
  providedIn: 'root'
})
export class ContactoService {

    constructor(private http: HttpClient) { }

    create(contacto: Contacto) {
        return this.http.post('api/contactos', contacto);
    }
}
