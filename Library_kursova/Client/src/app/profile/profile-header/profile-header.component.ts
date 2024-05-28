import { Component, OnInit } from '@angular/core';
import { UserDTO } from '../../dto/userDTO.model';
import { AccountService } from '../../auth/sign-in/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrl: './profile-header.component.css',
})
export class ProfileHeaderComponent implements OnInit {
  userDTO: UserDTO | null = null;

  constructor(private accountService: AccountService) {}
  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (userDTO) => (this.userDTO = userDTO),
      
    });
    console.log(this.userDTO);
  }
}
