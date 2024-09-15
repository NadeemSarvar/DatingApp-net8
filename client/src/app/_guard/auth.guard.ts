import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const accountServies = inject(AccountService);
  const toastr = inject(ToastrService);

  if (accountServies.currentUser()) {
    return true;
  } 
  else
  {
    toastr.error("You are not authorized!");
    return false;
  }
};
