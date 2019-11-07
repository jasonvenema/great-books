import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { IBook } from './book';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html'
})
export class BookComponent {
  public _http: HttpClient;
  public _books: IBook[];
  public _baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;

    this._http.get<IBook[]>(baseUrl + 'api/Books/GetAll').subscribe(result => {
      this._books = result;
    }, error => console.error(error));
  }

  onAddBook(isbn: string) {
    console.info('Clicked add button: ' + isbn)

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    this._http.post<string>(this._baseUrl + 'api/Books/Add', isbn, httpOptions)
      .pipe(
        //catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    //return throwError(
    //  'Something bad happened; please try again later.');
  };
}

