import { Component, inject, input, output, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-rgister',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './rgister.component.html',
  styleUrl: './rgister.component.css'
})
export class RgisterComponent {
  userFromHomeComponent = input.required<any>()
  //  @Output() cancelRegister = new EventEmitter()
  private accountservice = inject(AccountService);
  cancelRegister = output<boolean>();

  model: any = {};

  register() {
    this.accountservice.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: err => {
        console.log(err);
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
