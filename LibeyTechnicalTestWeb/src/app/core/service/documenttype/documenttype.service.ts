import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { DocumentType } from "src/app/entities/documenttype";
@Injectable({
    providedIn: "root",
})
export class DocumentTypeService {
    private apiUrl = `${environment.pathLibeyTechnicalTest}DocumentType/`; //nuevo
    constructor(private http: HttpClient) {}
    FindAll(): Observable<DocumentType[]> {
        return this.http.get<DocumentType[]>(this.apiUrl);
    }
}