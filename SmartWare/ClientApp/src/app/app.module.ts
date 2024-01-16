import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToggleFullScreenDirective } from './shared/components';

import { AdminComponent } from './core/theme/admin.component';


import { SharedModule } from './shared';
import { ConfigurationComponent, GuestComponent, NavBarComponent, NavLogoComponent, NavigationComponent ,NavContentComponent, NavCollapseComponent, NavGroupComponent, NavItemComponent, NavigationItem} from './core/theme/layout';
import { NavLeftComponent } from './core/theme/layout/admin/nav-bar/nav-left/nav-left.component';
import { NavRightComponent } from './core/theme/layout/admin/nav-bar/nav-right/nav-right.component';
import { NavSearchComponent } from './core/theme/layout/admin/nav-bar/nav-left/nav-search/nav-search.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './core/interceptors/jwtInterceptor';
import { TokenInterceptor } from './core/interceptors/token.interceptor';

import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    GuestComponent,
    AdminComponent,
    ConfigurationComponent,
    NavBarComponent,
    NavigationComponent,
    NavLeftComponent,
    NavRightComponent,
    NavContentComponent,
    NavLogoComponent,
    NavCollapseComponent,
    NavGroupComponent,
    NavItemComponent,
    NavSearchComponent,
    ToggleFullScreenDirective,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [
    NavigationItem,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,useClass: TokenInterceptor,multi: true
    },
  ],
  exports: [FormsModule,
    SharedModule],
  bootstrap: [AppComponent],

  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class AppModule {}
