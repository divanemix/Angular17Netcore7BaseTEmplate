import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared';
import { lastValueFrom } from 'rxjs';

import {
  ArticlesService,
  AuthenticationService,
  NotificationService
} from '../../../core/services';
import { User } from '../../../core/models';
import { Subject, takeUntil } from 'rxjs';
import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import { HttpClient, HttpParams } from '@angular/common/http';
import CustomStore from 'devextreme/data/custom_store';

@Component({
  selector: 'app-sample-page',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './sample-page.component.html',
  styleUrls: ['./sample-page.component.scss'],
})
export default class SamplePageComponent implements OnInit, OnDestroy {
  private _destroyed$ = new Subject();

  public cUser: User = new User();
  public articles: Array<any>;
  articlesDataSource: any;

  constructor(http: HttpClient, private articlesService: ArticlesService) {
    this.articlesDataSource = this.articlesService.getArticleDatasource();
  //  this.articlesDataSource = {
  //    load(loadOptions) {
  //      let params = new HttpParams();
  //      if (loadOptions) {
        
  //      }
  //      return lastValueFrom(
  //        articlesService.getAllarticles()
  //      );
  //    },
  //  };
  }


  ngOnInit(): void {

  }
  ngOnDestroy(): void {
    this._destroyed$.next(true);
    this._destroyed$.complete();
  }
  getAllArticles() {

    return this.articlesService.getAllarticles().pipe(takeUntil(this._destroyed$))
      .subscribe({
        next: response => {

          this.articles = response;
        },
        error: error => { //this.notificationService.error(error);
        }
      });

  }

  editorPreparing(e) {
    //if (e.dataField === 'articleId' && e.row.data.articleId === 1) {
    //  e.editorOptions.disabled = true;
    //  e.editorOptions.value = null;
    //}
  }

  initNewRow(e) {
    e.data.articleId = 0;
  }

  allowDeleting(e) {
    return true;//e.row.data.articleId !== 1;
  }
}
