import { Component, OnDestroy, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { AccountService } from '../auth/sign-in/account.service';
import { Observable, Subscription, of, take } from 'rxjs';
import { UserDTO } from '../dto/userDTO.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit, OnDestroy {
  isModalOpened = false;
  isModalSearchOpened = false;
  loggedIn = false;
  userDTO: UserDTO | null;
  isAdmin = false;
  userSubscription: Subscription;
  //currentUser$:Observable<User | null> = of(null);

  constructor(private router: Router, public accountService: AccountService) {}

  ngOnInit(): void {
    this.userSubscription = this.accountService.currentUser$
      .subscribe({
        next: (userDTO) => {
          this.userDTO = userDTO;
          this.loggedIn = !!userDTO;
          this.isAdmin = userDTO?.roles.includes('admin') ?? false;
        },
      });

    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      const user = JSON.parse(storedUser);
      this.accountService.setCurrentUser(user);
    }
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  navigateToBookmarks() {
    this.router.navigate(['profile/bookmarks']);
  }

  navigateToBorrows() {
    this.router.navigate(['profile/borrows']);
  }

  navigateToSettings() {
    this.router.navigate(['profile/settings']);
  }

  handleClose() {
    this.isModalOpened = false;
  }

  handleOpenModal() {
    this.isModalOpened = true;
  }

  handleOpenSearchModal() {
    this.isModalSearchOpened = true;
  }

  handleSearchClose() {
    this.isModalSearchOpened = false;
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/main');
    this.handleClose();
  }
}
