import { Component, OnInit, ViewChild } from '@angular/core';
import { UserDTO } from '../../dto/userDTO.model';
import { AccountService } from '../../auth/sign-in/account.service';
import { take, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { User } from '../../dto/models/user.model';
import { UpdateUserDTO } from '../../dto/updateUserDTO.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ProfileComponent } from '../profile.component';
import { ProfileService } from '../profile-header/profile.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css',
})
export class SettingsComponent implements OnInit {
  userDTO: UserDTO | null = null;
  currentUser: UserDTO | null = null;
  id: number;

  status: 'initial' | 'uploading' | 'success' | 'fail' = 'initial';
  file: File | null = null; // Variable to store file

  updateUserDTO: UpdateUserDTO | null;
  @ViewChild('updateUserForm') updateUserForm: NgForm | undefined;

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private profileService: ProfileService
  ) {}

  ngOnInit(): void {
    this.route.parent?.params.subscribe((params) => {
      this.id = +params['id'];
      this.profileService.getUser(this.id).subscribe((userDTO) => {
        this.userDTO = userDTO;
      });

      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next: (userDTO) => (this.currentUser = userDTO),
      });
    });
  }

  updateUser() {
    if (!this.userDTO) return;

    this.updateUserDTO = new UpdateUserDTO(
      this.userDTO.nickname,
      this.userDTO.email,
      this.userDTO.userImage,
      this.userDTO.firstName,
      this.userDTO.lastName,
      this.userDTO.phoneNumber,
      this.userDTO.isMember
    );
    console.log(this.updateUserDTO);
    this.accountService.updateUser(this.updateUserDTO).subscribe({
      next: (responseData) => {
        this.toastr.success('Ви успішно змінили інформацію!');
      },
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }

  isAdmin(): boolean {
    return this.currentUser?.roles?.includes('admin') || false;
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
