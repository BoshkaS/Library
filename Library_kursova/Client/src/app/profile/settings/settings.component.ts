import { Component, OnInit, ViewChild } from '@angular/core';
import { UserDTO } from '../../dto/userDTO.model';
import { AccountService } from '../../auth/sign-in/account.service';
import { take, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { User } from '../../dto/models/user.model';
import { UpdateUserDTO } from '../../dto/updateUserDTO.model';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css',
})
export class SettingsComponent implements OnInit {
  userDTO: UserDTO | null = null;

  status: 'initial' | 'uploading' | 'success' | 'fail' = 'initial';
  file: File | null = null; // Variable to store file

  updateUserDTO: UpdateUserDTO | null;
  @ViewChild('updateUserForm') updateUserForm: NgForm | undefined;

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (userDTO) => (this.userDTO = userDTO),
    });
  }

  updateUser() {
    this.updateUserDTO = {
      nickname: this.userDTO.nickname,
      email: this.userDTO.email,
      userImage: this.userDTO.userImage,
    };
    console.log(this.updateUserDTO);
    this.accountService.updateUser(this.updateUserDTO).subscribe({
      next: (responseData) => {
        this.toastr.success('Ви успішно змінили інформацію!');
      },
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }

  onChange(event) {
    const file: File = event.target.files[0];

    if (file) {
      this.status = 'initial';
      this.file = file;
    }
  }

  onUpload() {
    this.status = 'uploading';
    new Promise((resolve, reject) => {
      this.accountService.addPhoto(this.file).subscribe({
        next: () => {
          this.status = 'success';
          resolve(null);
        },
        error: (error: any) => {
          this.status = 'fail';
          return throwError(() => error);
        },
      });
    }).then(() => this.accountService.getPhoto().subscribe());
  }
}
