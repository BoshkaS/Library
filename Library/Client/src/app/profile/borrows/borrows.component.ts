import { Component, OnInit } from '@angular/core';
import { BorrowBookResponseDTO } from '../../dto/borrowBookResponseDTO.model';
import { UserDTO } from '../../dto/userDTO.model';
import { AccountService } from '../../auth/sign-in/account.service';
import { BorrowsService } from './borrows.service';
import { take } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ProfileService } from '../profile-header/profile.service';

@Component({
  selector: 'app-borrows',
  templateUrl: './borrows.component.html',
  styleUrl: './borrows.component.css',
})
export class BorrowsComponent implements OnInit {
  borrowsBooks: BorrowBookResponseDTO[] = [];
  userDTO: UserDTO | null = null;
  currentUser: UserDTO | null = null;
  isAdmin = false;

  id: number;

  activeBooks = this.borrowsBooks.filter((book) => !book.isReturned);
  returnedBooks = this.borrowsBooks.filter((book) => book.isReturned);

  constructor(
    private borrowsService: BorrowsService,
    private profileService: ProfileService,
    private route: ActivatedRoute,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        this.currentUser = user;
      },
    });
    this.isAdmin = this.currentUser?.roles.includes('admin') ?? false;
    this.route.parent?.params.subscribe((params) => {
      this.id = +params['id'];
      this.profileService.getUser(this.id).subscribe((user) => {
        this.userDTO = user;
        this.borrowsService.getBorrowsBook().subscribe((books) => {
          this.borrowsBooks = books;
          this.activeBooks = this.borrowsBooks.filter(
            (book) => !book.isReturned
          );
          this.returnedBooks = this.borrowsBooks.filter(
            (book) => book.isReturned
          );
        });
      });
    });
  }

  goToCreateBorrowing(): void {
    this.router.navigate(['/create-borrowing'], {
      queryParams: { userId: this.userDTO.id },
    });
  }
}
