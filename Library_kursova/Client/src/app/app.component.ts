import { Component, OnInit } from '@angular/core';
import { AccountService } from './auth/sign-in/account.service';
import { UserDTO } from './dto/userDTO.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'my-library-project';

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if(!userString) return;
      const user: UserDTO = JSON.parse(userString);
      this.accountService.setCurrentUser(user);
  }
}
