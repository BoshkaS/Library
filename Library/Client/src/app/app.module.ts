import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { MainComponent } from './main/main.component';
import { NoveltiesComponent } from './main/novelties/novelties.component';
import { NoveltiesItemComponent } from './main/novelties/novelties-item/novelties-item.component';
import { NoveltiesItemService } from './main/novelties/novelties.service';
import { PopularBooksComponent } from './main/popular-books/popular-books.component';
import { PopularBookComponent } from './main/popular-books/popular-book/popular-book.component';
import { PopularBooksService } from './main/popular-books/popular.service';
import { RandomBookComponent } from './main/random-book/random-book.component';
import { RandomBookService } from './main/random-book/random.service';
import { FooterComponent } from './footer/footer.component';
import { DropdownDirective } from './UI/dropdown/dropdown.directive';
import { DropdownContentDirective } from './UI/dropdown/dropdown-content.directive';
import { SelectorComponent } from './UI/selector/selector.component';
import { CatalogComponent } from './catalog/catalog.component';
import { FiltersComponent } from './catalog/filters/filters.component';
import { BooksComponent } from './catalog/books/books.component';
import { BookComponent } from './catalog/books/book/book.component';
import { BooksService } from './catalog/books/books.service';
import { FilterItemComponent } from './catalog/filters/filter-item/filter-item.component';
import { FiltersService } from './catalog/filters/filter-item/filters.service';
import { ProfileComponent } from './profile/profile.component';
import { BookmarksComponent } from './profile/bookmarks/bookmarks.component';
import { SettingsComponent } from './profile/settings/settings.component';
import { BorrowsComponent } from './profile/borrows/borrows.component';
import { ProfileHeaderComponent } from './profile/profile-header/profile-header.component';
import { BookmarksService } from './profile/bookmarks/bookmarks.service';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BookDetailService } from './book-details/book-details.service';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CreateAuthorComponent } from './admin/create-author/create-author.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from './auth/sign-in/account.service';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { ToastrModule } from 'ngx-toastr';
import {
  BrowserAnimationsModule,
  NoopAnimationsModule,
} from '@angular/platform-browser/animations';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BusyService } from './busyService';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { SearchComponent } from './search/search.component';
import { SearchBookService } from './search/search.service';
import { SearchBookItemComponent } from './search/search-book-item/search-book-item.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'primeng/carousel';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { CreatePublishingHouseComponent } from './admin/create-publishing-house/create-publishing-house.component';
import { CreateBookComponent } from './admin/create-book/create-book.component';
import { DeleteBookComponent } from './book-details/delete-book/delete-book.component';
import { DeleteBookService } from './book-details/delete-book/delete-book.service';
import { BorrowsService } from './profile/borrows/borrows.service';
import { BorrowBookItemComponent } from './profile/borrows/borrow-book-item/borrow-book-item.component';
import { ContinueDateService } from './profile/borrows/borrow-book-item/continue-date/continue-date.service';
import { ContinueDateComponent } from './profile/borrows/borrow-book-item/continue-date/continue-date.component';
import { CreateBorrowingComponent } from './admin/create-borrowing/create-borrowing.component';
import { NgxIntlTelInputModule } from 'ngx-intl-tel-input';
import { ProcessReturnDateComponent } from './admin/process-return-date/process-return-date.component';
import { ReturnDateComponent } from './admin/process-return-date/return-date/return-date.component';
import { ReturnDateService } from './admin/process-return-date/process-return-date.service';
import { ProfileService } from './profile/profile-header/profile.service';
import { CreateBorrowingService } from './admin/create-borrowing/create-borrowing.service';
import { AllUsersComponent } from './admin/all-users/all-users.component';
import { AllUsersService } from './admin/all-users/all-users.service';
import { BookCheckComponent } from './admin/book-check/book-check.component';
import { BookCheckService } from './admin/book-check/book-check.service';
import { ReturnBookComponent } from './profile/borrows/borrow-book-item/return-book/return-book.component';
import { EditBookComponent } from './book-details/edit-book/edit-book.component';
import { ReserveBookComponent } from './book-details/reserve-book/reserve-book.component';
import { ReservationsComponent } from './profile/reservations/reservations.component';
import { ReservationItemComponent } from './profile/reservations/reservation-item/reservation-item.component';
import { ReservationService } from './profile/reservations/reservations.service';
import { UserRatingLogComponent } from './profile/user-rating-log/user-rating-log.component';
import { SimilarBookComponent } from './book-details/similar-book/similar-book.component';
import { ConfirmReservationComponent } from './profile/reservations/reservation-item/confirm-reservation/confirm-reservation.component';
import { registerLocaleData } from '@angular/common';
import localeUk from '@angular/common/locales/uk';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';

registerLocaleData(localeUk);

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainComponent,
    NoveltiesComponent,
    NoveltiesItemComponent,
    PopularBooksComponent,
    PopularBookComponent,
    RandomBookComponent,
    FooterComponent,
    DropdownDirective,
    DropdownContentDirective,
    SelectorComponent,
    CatalogComponent,
    FiltersComponent,
    BooksComponent,
    BookComponent,
    FilterItemComponent,
    ProfileComponent,
    BookmarksComponent,
    SettingsComponent,
    BorrowsComponent,
    ProfileHeaderComponent,
    BookDetailsComponent,
    SignInComponent,
    CreateAuthorComponent,
    SignUpComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    SearchComponent,
    SearchBookItemComponent,
    AdminPanelComponent,
    HasRoleDirective,
    CreatePublishingHouseComponent,
    CreateBookComponent,
    DeleteBookComponent,
    BorrowBookItemComponent,
    ContinueDateComponent,
    CreateBorrowingComponent,
    ProcessReturnDateComponent,
    ReturnDateComponent,
    AllUsersComponent,
    BookCheckComponent,
    ReturnBookComponent,
    EditBookComponent,
    ReserveBookComponent,
    ReservationsComponent,
    ReservationItemComponent,
    UserRatingLogComponent,
    SimilarBookComponent,
    ConfirmReservationComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatIconModule,
    MatTooltipModule,
    MatButtonModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NoopAnimationsModule,
    NgxSpinnerModule.forRoot({
      type: 'ball-fall',
    }),
    PaginationModule.forRoot(),
    CarouselModule,
    ReactiveFormsModule,
    NgxIntlTelInputModule,
  ],
  providers: [
    NoveltiesItemService,
    PopularBooksService,
    RandomBookService,
    BooksService,
    FiltersService,
    BookmarksService,
    BookDetailService,
    AccountService,
    BusyService,
    SearchBookService,
    PaginationModule,
    DeleteBookService,
    BorrowsService,
    ContinueDateService,
    ReturnDateService,
    ProfileService,
    AllUsersService,
    CreateBorrowingService,
    BookCheckService,
    ReservationService,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: 'uk' },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
