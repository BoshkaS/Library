import { Component, EventEmitter, Output } from '@angular/core';
import { AccountService } from '../sign-in/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent {
  @Output() onClose = new EventEmitter();
  model: any = {};

  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  closeHandler() {
    this.onClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.closeHandler();
        this.router.navigateByUrl('/profile');
      },
      error: (error) => {
        this.toastr.error(error.error);
      },
    });
  }
}
