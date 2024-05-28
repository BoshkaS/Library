import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { LoginDTO } from '../../dto/loginDTO.model';
import { AccountService } from './account.service';
import { Observable, of } from 'rxjs';
import { UserDTO } from '../../dto/userDTO.model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css',
})
export class SignInComponent implements OnInit {
  @Output() onClose = new EventEmitter();

  loggedIn: boolean = false;
  isRegisterModalOpened = false;
  currentUser$: Observable<UserDTO | null> = of(null);

  model: LoginDTO = {
    email: '',
    password: '',
  };
  //loggedIn = false;

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  closeHandler() {
    this.onClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }

  navigateToSingUp() {
    this.isRegisterModalOpened = true;
  }

  handleClose() {
    this.isRegisterModalOpened = false;
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: (_) => this.router.navigateByUrl('/profile'),
    });
    this.closeHandler();
    this.handleClose();
    //console.log(this.model);
  }

  // logout() {
  //   this.accountService.logout();
  //   this.accountService.setLoginIn(false);
  // }
}
