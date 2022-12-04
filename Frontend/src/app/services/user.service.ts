import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Person from '../models/person';
import { PersonCreationDTO } from '../models/person-creation-dto';

const API_URL = 'https://localhost:7026/api/person/';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Person[]> {
    return this.http.get<Person[]>(API_URL);
  }

  getById(id: string): Observable<Person> {
    return this.http.get<Person>(`${API_URL}${id}`);
  }

  Create(newPerson: PersonCreationDTO){
    return this.http.post(API_URL, newPerson);
  }

  DeletePerson(id: string){
    return this.http.delete<{ msg: string }>(`${API_URL}${id}`);
  }

  EditPerson(person: Person){
    return this.http.put(`${API_URL}${person.id}`, person);
  }
}
