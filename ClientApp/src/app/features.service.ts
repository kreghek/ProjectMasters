import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feature } from './models/project';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FeaturesService {

  constructor(private http: HttpClient) { }

  private url = "/api/features";

  getFeatures(projectId: number): Observable<Feature[]> {
    const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
    return this.http.get<Feature[]>(this.url + "/project/" + projectId, { headers: myHeaders });
  }
}
