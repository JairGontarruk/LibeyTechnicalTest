import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { Region } from "src/app/entities/region";
import { Province } from "src/app/entities/province";
import { Ubigeo } from "src/app/entities/ubigeo";
@Injectable({
    providedIn: "root",
})
export class UbigeoService {
    private apiUrl = `${environment.pathLibeyTechnicalTest}Ubigeo/`; //nuevo
    constructor(private http: HttpClient) {}

    getRegions(): Observable<Region[]> {
        const uri = `${this.apiUrl}Region`;
        return this.http.get<Region[]>(uri);
    }

    getProvinces(regionCode: string): Observable<Province[]> {
        const uri = `${this.apiUrl}Province/${regionCode}`;
        return this.http.get<Province[]>(uri);
    }

    getUbigeo(regionCode: string,provinceCode: string): Observable<Ubigeo[]> {
        const uri = `${this.apiUrl}Ubigeo/${regionCode}/${provinceCode}`;
        return this.http.get<Ubigeo[]>(uri);
    }
}