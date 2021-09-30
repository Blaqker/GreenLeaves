import { Component, OnInit } from '@angular/core';
import { UbicacionService } from './services/ubicacion.service';
import { ContactoService } from './services/contacto.service';
import { Ubicacion } from './models/ubicacion';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ModalComponent } from './components/modal/modal.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
    ubicaciones: Ubicacion[] = [];
    emailPattern: string = '^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$';
    phonePattern: string = '^[0-9_-]{10,12}';

    contactoFormulario: FormGroup = this.fb.group({
        nombre     : [, Validators.required],
        email      : [, [Validators.required, Validators.pattern(this.emailPattern)]],
        telefono   : [, [Validators.required, Validators.pattern(this.phonePattern)]],
        fecha      : [, Validators.required],
        ubicacionId: [, Validators.required],
    });

    get nombreErrorMsg(): string {
        const error = this.contactoFormulario.get('nombre')?.errors;

        if (error?.required) {
            return 'Nombre es obligatorio';
        }

        return '';
    }

    get emailErrorMsg(): string {
        const error = this.contactoFormulario.get('email')?.errors;

        if (error?.required) {
            return 'Email es obligatorio';
        } else if (error?.pattern) {
            return 'Email no valido';
        }

        return '';
    }

    get telefonoErrorMsg(): string {
        const error = this.contactoFormulario.get('telefono')?.errors;

        if (error?.required) {
            return 'Telefono es obligatorio';
        } else if (error?.pattern) {
            return 'Telefono no valido';
        }

        return '';
    }

    get fechaErrorMsg(): string {
        const error = this.contactoFormulario.get('fecha')?.errors;

        if (error?.required) {
            return 'Fecha es obligatorio';
        }

        return '';
    }

    get estadoErrorMsg(): string {
        const error = this.contactoFormulario.get('ubicacionId')?.errors;

        if (error?.required) {
            return 'Estado y Ciudad es obligatorio';
        }

        return '';
    }

    constructor(
        private ubicacionService: UbicacionService,
        private contactoService: ContactoService,
        private fb: FormBuilder,
        private dialog: MatDialog
    ) {}

    ngOnInit(): void {
        this.ubicacionService.getUbicaciones().subscribe((resp) => {
            this.ubicaciones = resp;
        });
    }

    // campoNoValido(campo: string) {
    //     return (
    //         this.contactoFormulario.controls[campo].errors &&
    //         this.contactoFormulario.controls[campo].touched
    //     );
    // }

    submit() {
        if (this.contactoFormulario.invalid) {
            const invalid = [];

            if (this.nombreErrorMsg !== '') invalid.push(this.nombreErrorMsg);

            if (this.emailErrorMsg !== '') invalid.push(this.emailErrorMsg);

            if (this.telefonoErrorMsg !== '') invalid.push(this.telefonoErrorMsg);

            if (this.fechaErrorMsg !== '') invalid.push(this.fechaErrorMsg);

            if (this.estadoErrorMsg !== '') invalid.push(this.estadoErrorMsg);

            //this.contactoFormulario.markAllAsTouched();

            this.dialog.open(ModalComponent, {
                data: invalid,
            });

            return;
        }

        this.contactoService.create(this.contactoFormulario.value).subscribe();
        this.contactoFormulario.reset();
    }
}
