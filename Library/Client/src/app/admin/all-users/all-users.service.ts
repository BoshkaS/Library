import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserDTO } from '../../dto/userDTO.model';

@Injectable()
export class AllUsersService {
  constructor(private http: HttpClient, private toastr: ToastrService) {}

  getUsers() {
    return this.http.get<UserDTO[]>('https://localhost:5001/api/users');
  }
}
