import { Component, OnInit, Inject } from '@angular/core';

import { IBook } from '../books/book';
import { BookService } from '../services/book.service';

@Component({
  selector: 'app-book-search',
  templateUrl: './book-search.component.html',
  styleUrls: ['./book-search.component.css']
})
export class BookSearchComponent implements OnInit {

  public _books: IBook[];
  public _baseUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string, private _bookService: BookService) {
    this._baseUrl = baseUrl;
  }

  ngOnInit() {
    console.log("BookComponent: Initializing " + this._baseUrl);

    // Load the list of books
    this._bookService.getBooks().subscribe(
      result => { this._books = result; },
      error => { console.error(error); }
    );
  }

  onAddBook(isbn: string) {
    console.info('Clicked add button: ' + isbn)
    this._bookService.addBook(isbn);
  }
}
