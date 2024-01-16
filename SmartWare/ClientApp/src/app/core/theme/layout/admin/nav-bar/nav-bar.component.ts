import { Component, EventEmitter, Output } from '@angular/core';
import { AuthenticationService } from '../../../../services';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  @Output() onNavCollapsedMob = new EventEmitter();
  navCollapsedMob;
  headerStyle: string;
  menuClass: boolean;
  collapseStyle: string;

  public cUser: any = {}
;

  constructor() {
    this.navCollapsedMob = false;
    this.headerStyle = '';
    this.menuClass = false;
    this.collapseStyle = 'none';

  
  }
 
  toggleMobOption() {
    this.menuClass = !this.menuClass;
    this.headerStyle = this.menuClass ? 'none' : '';
    this.collapseStyle = this.menuClass ? 'block' : 'none';
  }
}
