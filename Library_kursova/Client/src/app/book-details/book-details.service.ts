import { Injectable, OnInit } from '@angular/core';
import { Book } from '../dto/models/book.model';
import { BookDTO } from '../dto/bookDTO.model';
import { HttpClient } from '@angular/common/http';
import { CommentRequestDTO } from '../dto/commentRequestDTO.model';
import { ToastrService } from 'ngx-toastr';
import { CommentResponseDTO } from '../dto/commentResponseDTO.model';
import { BehaviorSubject, Observable, Subject, tap } from 'rxjs';

@Injectable()
export class BookDetailService {
  bookDetails: BookDTO | null = null;
  //commentAdded = new Subject<CommentResponseDTO>();
  private isLikedSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  getbookDetails(id: number) {
    console.log(id);
    return this.http.get<BookDTO>('https://localhost:5001/api/book/' + id);
  }

  addComment(comment: CommentRequestDTO) {
    return this.http.post<CommentResponseDTO>(
      'https://localhost:5001/api/comment',
      comment
    );
  }

  isBookHasLike(bookId: number, email: string) {
    return this.http
      .get<boolean>(
        'https://localhost:5001/api/likedbook/liked/?bookId=' +
          bookId +
          '&email=' +
          email
      )
      .subscribe((isBookmark) => {
        this.isLikedSubject.next(isBookmark); // Emit the updated bookmark status
      });
  }

  getIsLikedObservable() {
    return this.isLikedSubject.asObservable();
  }

  addLike(bookmarkBookDTO: { bookId: number; email: string }) {
    return this.http
      .post('https://localhost:5001/api/likedbook/addlike', bookmarkBookDTO)
      .pipe(
        tap(() => {
          this.isLikedSubject.next(true); // Emit that the book is now bookmarked
        })
      );
  }

  deleteLike(bookmarkBookDTO: { bookId: number; email: string }) {
    return this.http
      .delete(
        'https://localhost:5001/api/likedbook/deletelike/?bookId=' +
          bookmarkBookDTO.bookId +
          '&email=' +
          bookmarkBookDTO.email
      )
      .pipe(
        tap(() => {
          this.isLikedSubject.next(false); // Emit that the book is no longer bookmarked
        })
      );
  }
}
