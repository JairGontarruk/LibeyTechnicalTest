import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";
import { LibeyUsers } from "src/app/entities/libeyusers";
@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	private apiUrl = `${environment.pathLibeyTechnicalTest}LibeyUser/`; //nuevo
	constructor(private http: HttpClient) {}
	Find(documentNumber: string): Observable<LibeyUser> {
		const uri = `${this.apiUrl}${documentNumber}`;
		return this.http.get<LibeyUser>(uri);
	}
	FindAll(documentNumber?: string): Observable<LibeyUsers[]> {
		let params = new HttpParams();
		if (documentNumber) {
			params = params.set('documentNumber', documentNumber);
		}
		return this.http.get<LibeyUsers[]>(this.apiUrl, { params });
	}
	// Crear usuario (POST)
	createUser(user: LibeyUser): Observable<any> {
		return this.http.post(this.apiUrl, user);
	}
	// Actualizar usuario (PUT)
	updateUser(numberDocument: string, user: LibeyUser): Observable<any> {
		return this.http.put(`${this.apiUrl}${numberDocument}`, user);
	}

	deleteUser(numberDocument: string): Observable<void> {
		return this.http.delete<void>(`${this.apiUrl}${numberDocument}`);
	}
}