import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared';

@Component({
  selector: 'app-basic-badge',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './basic-badge.component.html',
  styleUrls: ['./basic-badge.component.scss'],
})
export default class BasicBadgeComponent {}
