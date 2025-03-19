import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { FormControl  } from '@angular/forms';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { LibeyUsers } from 'src/app/entities/libeyusers';


@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})

export class UserlistComponent implements OnInit {
  
    users: LibeyUsers[] = []; // Lista de usuarios
    searchDocumentNumber = new FormControl('');
    constructor(private userService: LibeyUserService) { }
    ngOnInit(): void {
        this.loadUsers();
    }
  //Submit(){
  //  swal.fire("Oops!", "Something went wrong!", "error");
  //}
    loadUsers(): void {
        this.userService.FindAll().subscribe(
        (data) => {
            this.users = data;
        },
        (error) => {
            console.error('Error al obtener usuarios', error);
        }
        );
    }

    deleteUser(documentNumber: string): void {
        swal.fire({
          title: "¿Estás seguro?",
          text: "Esta acción no se puede deshacer.",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#d33",
          cancelButtonColor: "#3085d6",
          confirmButtonText: "Sí, eliminar",
          cancelButtonText: "Cancelar"
        }).then((result) => {
          if (result.isConfirmed) {
            this.userService.deleteUser(documentNumber).subscribe(() => {
              swal.fire({
                title: "Eliminado",
                text: "El usuario ha sido eliminado correctamente.",
                icon: "success",
                confirmButtonText: "OK"
              });
      
              // Volver a cargar la lista de usuarios después de eliminar
              this.loadUsers();
            }, error => {
              swal.fire({
                title: "Error",
                text: "No se pudo eliminar el usuario.",
                icon: "error",
                confirmButtonText: "OK"
              });
            });
          }
        });
      }

      searchUser(): void {
        this.userService.FindAll(this.searchDocumentNumber.value).subscribe(users => {
          this.users = users;
        });
      }
}