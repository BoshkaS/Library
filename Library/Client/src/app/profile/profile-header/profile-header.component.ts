import { Component, OnInit } from '@angular/core';
import { UserDTO } from '../../dto/userDTO.model';
import { ProfileService } from './profile.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../auth/sign-in/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrl: './profile-header.component.css',
})
export class ProfileHeaderComponent implements OnInit {
  userDTO: UserDTO | null = null;
  currentUser: UserDTO | null = null;
  isAdmin = false;
  isOwnProfile = false;

  id: number;

  constructor(
    private profileService: ProfileService,
    private route: ActivatedRoute,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.id = +params['id'];
      this.profileService.getUser(this.id).subscribe((userDTO) => {
        this.userDTO = userDTO;
      });

      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next: (userDTO) => (this.currentUser = userDTO),
      });
    });
    this.isAdmin = this.currentUser?.roles.includes('admin') ?? false;
    this.isOwnProfile = this.currentUser?.id == this.id;
  }
}
