import { Component, inject, OnInit } from '@angular/core';
import { RgisterComponent } from "../rgister/rgister.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RgisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  http = inject(HttpClient)
  registerMode = false;
  users: any;

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getUsers() {
    this.http.get("https://localhost:5001/api/Users/GetUsers").subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log(this.users)
    })
  }
  cancelRegisterMode(event : boolean){
    this.registerMode = event;
  }
}
