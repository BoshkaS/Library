import { Component, OnInit } from '@angular/core';
import { BookmarksService } from './bookmarks.service';
import { Book } from '../../dto/models/book.model';
import { UserDTO } from '../../dto/userDTO.model';
import { AccountService } from '../../auth/sign-in/account.service';
import { take } from 'rxjs';
import { ProfileService } from '../profile-header/profile.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bookmarks',
  templateUrl: './bookmarks.component.html',
  styleUrl: './bookmarks.component.css',
})
export class BookmarksComponent implements OnInit {
  bookmarksBook: Book[] = [];
  filteredBooks: Book[] = [];
  searchQuery: string = '';
  userDTO: UserDTO | null = null;
  currentUser: UserDTO | null = null;

  id: number;

  constructor(
    private bookmarksService: BookmarksService,
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

    this.route.parent?.params.subscribe((params) => {
      this.id = +params['id'];
      this.profileService.getUser(this.id).subscribe((user) => {
        this.userDTO = user;
        this.bookmarksService.getBookmarksBook().subscribe((books) => {
          this.bookmarksBook = books;
          this.filteredBooks = books; // initialize filtered
        });
      });
    });
  }

  onSearchChange(): void {
    const query = this.searchQuery.toLowerCase();
    this.filteredBooks = this.bookmarksBook.filter((book) =>
      book.title.toLowerCase().includes(query)
    );
  }
}
