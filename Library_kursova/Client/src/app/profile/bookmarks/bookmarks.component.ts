import { Component, OnInit } from '@angular/core';
import { BookmarksService } from './bookmarks.service';
import { Book } from '../../dto/models/book.model';
import { UserDTO } from '../../dto/userDTO.model';
import { AccountService } from '../../auth/sign-in/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-bookmarks',
  templateUrl: './bookmarks.component.html',
  styleUrl: './bookmarks.component.css',
})
export class BookmarksComponent implements OnInit {
  bookmarksBook: Book[] = [];
  userDTO: UserDTO | null = null;

  constructor(
    private bookmarksService: BookmarksService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (userDTO) => (this.userDTO = userDTO),
    });
    this.bookmarksService
      .getBookmarksBook(this.userDTO.email)
      .subscribe((books) => {
        this.bookmarksBook = books;
        console.log(this.bookmarksBook);
      });
  }
}
