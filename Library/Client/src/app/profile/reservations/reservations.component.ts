import { Component, OnInit } from '@angular/core';
import { ReservationDTO } from '../../dto/reservationDTO.model';
import { UserDTO } from '../../dto/userDTO.model';
import { ReservationService } from './reservations.service';
import { ProfileService } from '../profile-header/profile.service';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../../auth/sign-in/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.css',
})
export class ReservationsComponent implements OnInit {
  reservedBooks: ReservationDTO[] = [];
  userDTO: UserDTO | null = null;
  currentUser: UserDTO | null = null;
  isLoading = true;

  id: number;

  constructor(
    private reservationService: ReservationService,
    private profileService: ProfileService,
    private route: ActivatedRoute,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        this.currentUser = user;
      },
    });
    setTimeout(() => {
      this.route.parent?.params.subscribe((params) => {
        this.id = +params['id'];
        this.profileService.getUser(this.id).subscribe((user) => {
          this.userDTO = user;
          this.reservationService
            .getReservationBook(this.userDTO.id)
            .subscribe((books) => {
              this.reservedBooks = books;
              this.isLoading = false;
            });
        });
      });
    }, 500);
  }
}
