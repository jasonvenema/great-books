import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IBook } from '../interfaces/book';
import { ISearchResult } from 'app/interfaces/search-result';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private _baseUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string, private _http: HttpClient) {
    this._baseUrl = baseUrl;
    console.log("BookService: Setting base URL to " + this._baseUrl);
  }

  getBooks(): Observable<IBook[]> {
    let url = this._baseUrl + '/api/Books/GetAll';
    console.log("BookService: Getting books from " + url);
    return this._http.get<IBook[]>(url);
  }

  searchBooks(query: string): Observable<ISearchResult> {
    let url = this._baseUrl + '/api/Books/Search?query=' + query;
    console.log("BookService: Searching books from " + url);
    return this._http.get<ISearchResult>(url);
  }

  addBook(isbn: string) {
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
}
