import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Project } from './models/project';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  private url = "/api/projects";

  constructor(private http: HttpClient) { }

  createProject(): Observable<Project> {
    const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
    return this.http.post<Project>(this.url + "/generate", null, { headers: myHeaders });
  }
}
