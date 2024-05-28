import { NgModule } from '@angular/core';
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
import { FormsModule } from '@angular/forms';
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
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    //{provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi:true}
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
