import { Component, OnInit } from '@angular/core';
import { ISearchResult } from 'app/interfaces/search-result';
import { BookService } from 'app/services/book.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css']
})
export class TableListComponent implements OnInit {

  _searchResult: ISearchResult = null;
  _searchQuery: string = '';
  _isSearching: boolean = false;
  _resultCount: number;

  constructor(private _bookService: BookService) {
    console.log("Book Service is " + _bookService);
  }

  ngOnInit() {
  }

  onSearch() {
    this._isSearching = true;
    this._searchResult = null;
    this._resultCount = null;

    if (this._searchQuery.length > 0) {
      this._bookService.searchBooks(this._searchQuery).subscribe(
        (result: ISearchResult) => {
          this._searchResult = { ...result };
          this._isSearching = false;
          this._resultCount = this._searchResult.numFound;
        },
        error => { console.error(error); });
    }
  }

}
