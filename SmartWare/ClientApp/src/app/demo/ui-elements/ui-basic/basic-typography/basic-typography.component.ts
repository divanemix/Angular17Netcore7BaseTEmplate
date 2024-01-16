import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared';


@Component({
  selector: 'app-basic-typography',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './basic-typography.component.html',
  styleUrls: ['./basic-typography.component.scss'],
})
export default class BasicTypographyComponent {}
