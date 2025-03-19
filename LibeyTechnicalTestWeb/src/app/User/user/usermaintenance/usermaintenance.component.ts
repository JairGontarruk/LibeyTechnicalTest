import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,Router  } from '@angular/router';
import { FormControl,FormGroup, FormBuilder  } from '@angular/forms';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { DocumentTypeService } from 'src/app/core/service/documenttype/documenttype.service';
import { UbigeoService } from 'src/app/core/service/ubigeo/ubigeo.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  userForm: FormGroup = new FormGroup({});
  isNew: boolean = false;
	documentNumber: string | null = null;
  documentTypes: any[] = [];
  documentTypeControl = new FormControl('');
  regionControl = new FormControl('');
  provinceControl = new FormControl('');
  ubigeoControl = new FormControl('');
  region: any[] = [];
  provinces: any[] = [];
  ubigeos: any[] = [];

  selectedRegion: string = '';

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute, 
              private router: Router,
              private libeyUserService: LibeyUserService,
              private documentTypeService: DocumentTypeService,
              private ubigeoService: UbigeoService) { 

                this.userForm = this.fb.group({
                  documentTypeId: [''],
                  documentNumber: [''],
                  name: [''],
                  fathersLastName: [''],
                  mothersLastName: [''],
                  email: [''],
                  phone: [''],
                  region: [''],
                  province: [''],
                  ubigeoCode: [''],
                  address: [''],
                  password: ['']
                });
              }
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id') ?? '';
			if (id === 'new') {
				this.isNew = true;
        console.log('es nuevo:');
			} else {
				this.isNew = false;

				this.documentNumber = id;
        this.loadUserData(this.documentNumber);
				console.log('Editando usuario con ID:', this.documentNumber);
			}
    });

    // Cargar los tipos de documentos
    this.documentTypeService.FindAll().subscribe(
      (data) => {
        this.documentTypes = data;
      },
      (error) => {
        console.error('Error al obtener los tipos de documentos', error);
      }
    );

    // Cargar los departamentos
    this.ubigeoService.getRegions().subscribe(
      (data) => {
        this.region = data;
      },
      (error) => {
        console.error('Error al obtener los departamentos', error);
      }
    );

    // Cuando cambia la región, cargar provincias
    this.regionControl.valueChanges.subscribe(regionCode => {
      this.provinceControl.setValue(''); // Reiniciar selección
      this.ubigeoControl.setValue('');
      this.loadProvinces(regionCode);
    });

    // Cuando cambia la provincia, cargar ubigeos
    this.provinceControl.valueChanges.subscribe(provinceCode => {
      const regionCode = this.regionControl.value;
      this.ubigeoControl.setValue('');
      if (regionCode && provinceCode) {
        this.loadUbigeo(regionCode, provinceCode);
      }
    });

  };

  loadProvinces(regionCode: string): void {
    if (regionCode) {
      this.ubigeoService.getProvinces(regionCode).subscribe(data => {
        this.provinces = data;
      });
    }
  }

  loadUbigeo(regionCode: string, provinceCode: string): void {
    if (regionCode && provinceCode) {
      this.ubigeoService.getUbigeo(regionCode, provinceCode).subscribe(data => {
        this.ubigeos = data;
      });
    }
  }

  loadUserData(documentNumber: string): void {
    console.log("leyendo data ::", documentNumber);
    this.libeyUserService.Find(documentNumber).subscribe(user => {
      this.userForm.patchValue({
        documentTypeId: user.documentTypeId,
        documentNumber: user.documentNumber,
        name: user.name,
        fathersLastName: user.fathersLastName,
        mothersLastName: user.mothersLastName,
        email: user.email,
        phone: user.phone,
        region: user.regionCode,
        province: user.provinceCode,
        ubigeoCode: user.ubigeoCode,
        address: user.address,
        password: user.password
      });

      this.loadProvinces(user.regionCode); // Cargar provincias según región
      this.loadUbigeo(user.regionCode, user.provinceCode); // Cargar ubigeos según provincia
    });
  }

  onRegionChange(regionCode: string): void {
    this.provinceControl.setValue(''); // Reiniciar selección
      this.ubigeoControl.setValue('');
      this.loadProvinces(regionCode);
  }

  onProvinceChange(provinceCode: string): void {
    const regionCode = this.userForm.value.region;
    this.ubigeoControl.setValue('');
    console.log("cargarubigeo_regcode : ", regionCode);
    console.log("cargarubigeo_provcode : ", provinceCode);
    if (regionCode && provinceCode) {
      this.loadUbigeo(regionCode, provinceCode);
    }
  }

  Submit(){

    if (this.userForm.invalid) {
      swal.fire({
        title: "Error",
        text: "Por favor, completa todos los campos requeridos.",
        icon: "error",
        confirmButtonText: "OK"
      });
      return;
    }

    const userData: LibeyUser = this.userForm.value;
    console.log("Datos enviados:", userData);
    console.log("numdoc : ",this.documentNumber);

    if (this.isNew) {
      this.libeyUserService.createUser(userData).subscribe(() => {
        swal.fire({
          title: "¡Guardado!",
          text: "Usuario creado con éxito.",
          icon: "success",
          confirmButtonText: "OK"
        });
        //alert('Usuario creado con éxito');
        this.router.navigate(['/user/list']);
      });
    } else {
      this.libeyUserService.updateUser(this.documentNumber!, userData).subscribe(() => {
        swal.fire({
          title: "¡Guardado!",
          text: "Usuario actualizado con éxito.",
          icon: "success",
          confirmButtonText: "OK"
        });
        //alert('Usuario actualizado con éxito');
        this.router.navigate(['/user/list']);
      });
    }
    //swal.fire("Oops!", "Something went wrong!", "error");
  }
}