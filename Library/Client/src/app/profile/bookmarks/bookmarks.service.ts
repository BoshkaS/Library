import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Book } from '../../dto/models/book.model';
import { Injectable } from '@angular/core';
import { AccountService } from '../../auth/sign-in/account.service';
import { BehaviorSubject, take, tap } from 'rxjs';
import { UserDTO } from '../../dto/userDTO.model';

@Injectable()
export class BookmarksService {
  private isBookmarkSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {}

  getBookmarksBook(userId: number) {
    return this.http.get<Book[]>(
      `https://localhost:5001/api/bookmarkbook/bookmarks/${userId}`
    );
  }

  isBookInBookmarks(bookId: number) {
    return this.http
      .get<boolean>(
        'https://localhost:5001/api/bookmarkbook/bookmark/?bookId=' + bookId
      )
      .subscribe((isBookmark) => {
        this.isBookmarkSubject.next(isBookmark); // Emit the updated bookmark status
      });
  }

  getIsBookmarkObservable() {
    return this.isBookmarkSubject.asObservable();
  }

  addBookmarkBook(bookmarkBookDTO: { bookId: number }) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http
      .post(
        'https://localhost:5001/api/bookmarkbook/addbookmark',
        bookmarkBookDTO,
        { headers: headers }
      )
      .pipe(
        tap(() => {
          this.isBookmarkSubject.next(true); // Emit that the book is now bookmarked
        })
      );
  }

  deleteBookmarkBook(bookmarkBookDTO: { bookId: number }) {
    return this.http
      .delete(
        'https://localhost:5001/api/bookmarkbook/deletebookmark/?bookId=' +
          bookmarkBookDTO.bookId
      )
      .pipe(
        tap(() => {
          this.isBookmarkSubject.next(false); // Emit that the book is no longer bookmarked
        })
      );
  }
}
