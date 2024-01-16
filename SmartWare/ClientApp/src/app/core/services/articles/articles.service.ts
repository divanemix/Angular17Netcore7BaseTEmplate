import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, BehaviorSubject, Subject, lastValueFrom } from 'rxjs';
import { catchError, tap, map, takeUntil } from 'rxjs/operators';
import CustomStore from 'devextreme/data/custom_store';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
  apiUrl = "";
  articleDataSource = {};
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.apiUrl = ` ${baseUrl}api/articles/`;
  }

  getArticleDatasource(): any {
    let self = this; 
    return this.articleDataSource = {
      key: "articleId",
      load(loadOptions) {
        let params = new HttpParams();
        if (loadOptions) {

        }
        return lastValueFrom(
          self.getAllarticles()
        );
      }
      , insert: (values) => {
        return lastValueFrom(self.editAllarticles(values))
          .catch(() => { alert("Insertion Failed"); });
      },
      update: (key, values) => {
        return lastValueFrom(self.editAllarticles(values))
          .catch(() => { alert("update Failed"); });
      },
      remove: (key) => {
        return lastValueFrom(self.removeAllarticles(key))
          .catch(() => { throw 'Deletion failed' });
      }
    };
  };
  getAllarticles() {
    return this.http.get<any>(this.apiUrl + 'all-articles')
      .pipe(tap(_ => { }));
  };
  editAllarticles(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'edit-articles', data)
      .pipe(tap(_ => { }));
  };
  removeAllarticles(articleId: number) {
    return this.http.delete<any>(this.apiUrl + `delete-articles?articleId=${articleId}`)
      .pipe(tap(_ => { }));
  };
}
