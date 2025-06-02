import { Injectable } from '@angular/core';
import { UserDTO } from '../../dto/userDTO.model';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ProfileService {
  constructor(private http: HttpClient, private toastr: ToastrService) {}

  getUser(id: number) {
    return this.http.get<UserDTO>('https://localhost:5001/api/users/' + id);
  }
}
