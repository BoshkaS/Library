import { Component, OnInit } from '@angular/core';
import { UserDTO } from '../../dto/userDTO.model';
import { CreateBorrowingService } from '../create-borrowing/create-borrowing.service';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrl: './all-users.component.css',
})
export class AllUsersComponent implements OnInit {
  allUsers: UserDTO[] = [];
  filteredUsers: UserDTO[] = [];
  searchEmail: string = '';

  constructor(private borrowingService: CreateBorrowingService) {}

  ngOnInit(): void {
    this.borrowingService.getUsers().subscribe((users) => {
      this.allUsers = users;
      this.filteredUsers = users;
    });
  }

  onSearchChange(): void {
    const search = this.searchEmail.toLowerCase();
    this.filteredUsers = this.allUsers.filter((user) =>
      user.email.toLowerCase().includes(search)
    );
  }
}
