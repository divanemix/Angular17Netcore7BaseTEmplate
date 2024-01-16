import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationService } from '../../authentication.service';
import { NotificationService } from '../../../notifications/notification.service';
import { SharedModule } from '../../../../../shared/shared.module';

@Component({
  selector: 'app-auth-signin',
  standalone: true,
  imports: [CommonModule, RouterModule, SharedModule],
  templateUrl: './auth-signin.component.html',
  styleUrls: ['./auth-signin.component.scss'],
})
export default class AuthSigninComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
 
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService
    //private appSettingsService: AppSettingsService,
    //public matDialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.authenticationService.logout();

    this.loginForm = this.formBuilder.group({
      'username': ['', Validators.required],
      'password': ['', Validators.required],
      
    });
  }
  onFormSubmit(form: NgForm) {

    if (this.loginForm.dirty && !this.loginForm.valid) return;

    this.authenticationService.login(form)
      .subscribe({
        next: response =>
        {
          let cUser = this.authenticationService.currentUserValue;
          //this.appSettingsService.loadAppConfigByLab(res);
          this.router.navigate(['']);
        },
        error: error => { this.notificationService.error(error); }
      });
  }
}
