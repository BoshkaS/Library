import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css',
})
export class AdminPanelComponent {
  isModalAuthorOpened = false;
  isModalHouseOpened = false;

  constructor(private router: Router) {}

  navigateToAuthor() {
    this.isModalAuthorOpened = true;
  }

  handleAuthorClose() {
    this.isModalAuthorOpened = false;
  }

  navigateToHouse() {
    this.isModalHouseOpened = true;
  }

  handleHouseClose() {
    this.isModalHouseOpened = false;
  }

  navigateToBook() {
    this.router.navigate(['book-check']);
  }

  navigateToUsers() {
    this.router.navigate(['/all-users']);
  }

  navigateToRequestDate() {
    this.router.navigate(['process-return-date']);
  }
}
