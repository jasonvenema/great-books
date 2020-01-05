export interface ISearchResult {
  numFound: number;
  documents: ISearchDocument[];
}

export interface ISearchDocument {
  titleSuggest: string;
  authorName: string[];
  title: string;
  publishDate: string[];
  first_publish_date: string;
  isbn: string[];
  lccn: string[];
  coverUrl: string[];
}