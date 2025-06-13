import {
  ChangeDetectorRef,
  Component,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { BookDetailService } from './book-details.service';
import { Book } from '../dto/models/book.model';
import { BookDTO } from '../dto/bookDTO.model';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { AccountService } from '../auth/sign-in/account.service';
import { CommentRequestDTO } from '../dto/commentRequestDTO.model';
import { UserDTO } from '../dto/userDTO.model';
import { Subscription, switchMap, take } from 'rxjs';
import { NgForm } from '@angular/forms';
import { BookmarksService } from '../profile/bookmarks/bookmarks.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrl: './book-details.component.css',
})
export class BookDetailsComponent implements OnInit, OnChanges {
  bookDetails: BookDTO | null = null;
  id: number;
  commentDTO: CommentRequestDTO = {
    text: '',
    bookId: 0,
  };
  userDTO: UserDTO | null = null;
  similarBooks: BookDTO | null = null;

  isBookmark: boolean = false;
  isLiked: boolean = false;
  isLoggedIn = false;
  isAdmin = false;
  isModalDeleteOpened = false;
  isModalReservOpened = false;
  isLoading = true;

  private subscriptions: Subscription[] = [];

  constructor(
    private bookDetailService: BookDetailService,
    private route: ActivatedRoute,
    private accountService: AccountService,
    private bookmarkService: BookmarksService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.id) {
      this.bookDetailService.getbookDetails(this.id).subscribe((booksDTO) => {
        this.bookDetails = booksDTO;
      });
    }
    console.log(this.bookDetails.book.numberOfBorrows);
  }

  isCurrentUser() {
    if (this.accountService.currentUser$) return true;
    else return false;
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.id = this.route.snapshot.params['id'];

    this.subscriptions.push(
      this.bookDetailService.getbookDetails(this.id).subscribe((booksDTO) => {
        this.bookDetails = booksDTO;
        console.log(this.bookDetails);
      }),
      this.accountService.currentUser$.subscribe({
        next: (userDTO) => {
          this.userDTO = userDTO;
          this.isLoggedIn = !!userDTO;
          this.isAdmin = userDTO?.roles.includes('admin') ?? false;
          if (this.userDTO) {
            this.bookmarkService.isBookInBookmarks(this.id);
            this.bookDetailService.isBookHasLike(this.id);
          }
        },
      }),
      //this.bookmarkService.isBookInBookmarks(this.id, this.userDTO.email);
      this.bookmarkService.getIsBookmarkObservable().subscribe((isBookmark) => {
        this.isBookmark = isBookmark;
      }),

      //this.bookDetailService.isBookHasLike(this.id, this.userDTO.email);
      this.bookDetailService.getIsLikedObservable().subscribe((isLiked) => {
        this.isLiked = isLiked;
      }),

      this.bookDetailService
        .getSimilarBooks(this.id)
        .subscribe((similarBooks) => {
          this.similarBooks = similarBooks;
          console.log(this.similarBooks);
          this.isLoading = false;
        })
    );
  }

  updateState(newState: any) {
    // Update the state
    const previousId = this.id;
    this.id = newState; // Check if the state has changed and perform necessary actions
    if (this.id !== previousId) {
      this.bookDetailService.getbookDetails(this.id).subscribe((booksDTO) => {
        this.bookDetails = booksDTO;
      });
    }
  }

  onCreateComment(form: NgForm) {
    this.commentDTO.bookId = this.id;
    this.bookDetailService.addComment(this.commentDTO).subscribe({
      next: (responseData) => {
        if (responseData) {
          this.bookDetailService
            .getbookDetails(this.id)
            .subscribe((booksDTO) => {
              this.bookDetails = booksDTO;
            });
        }
        this.toastr.success('Ви додали коментар!');
      },
      error: (error) => console.log(error),
    });
    form.reset();
  }

  onAddBookmark() {
    if (this.isBookmark === false) {
      this.bookmarkService.addBookmarkBook({ bookId: this.id }).subscribe({
        next: () => {
          this.toastr.success(
            'Ви додали в закладинки "' + this.bookDetails.book.title + '"'
          );
          this.isBookmark = true;
          if (this.bookDetails) {
            this.bookDetails.book.numberOfBorrows += 1; // Update the bookmark count
          }
        },
      });
    } else {
      this.bookmarkService.deleteBookmarkBook({ bookId: this.id }).subscribe({
        next: () => {
          this.toastr.success(
            'Ви видалили із закладинок "' + this.bookDetails.book.title + '"'
          );
          this.isBookmark = false;
          if (this.bookDetails) {
            this.bookDetails.book.numberOfBorrows -= 1; // Update the bookmark count
          }
        },
      });
    }
  }

  onAddLike() {
    if (this.isLiked === false) {
      this.bookDetailService.addLike({ bookId: this.id }).subscribe({
        next: () => {
          this.toastr.success(
            'Ви поставили вподобайку "' + this.bookDetails.book.title + '"'
          );
          this.isLiked = true;
          if (this.bookDetails) {
            this.bookDetails.book.numberOfLikes += 1; // Update the bookmark count
          }
        },
      });
    } else {
      this.bookDetailService.deleteLike({ bookId: this.id }).subscribe({
        next: () => {
          this.toastr.success(
            'Ви забрали вподобайку з "' + this.bookDetails.book.title + '"'
          );
          this.isLiked = false;
          if (this.bookDetails) {
            this.bookDetails.book.numberOfLikes -= 1; // Update the bookmark count
          }
        },
      });
    }
  }

  handleOpenDeleteModal() {
    this.isModalDeleteOpened = true;
  }

  handleDeleteClose() {
    this.isModalDeleteOpened = false;
  }

  handleOpenReservModal() {
    this.isModalReservOpened = true;
  }

  handleReservClose() {
    this.isModalReservOpened = false;
  }

  navigateToEditBook() {
    this.router.navigate(['/edit-book/' + this.id]);
  }
}
