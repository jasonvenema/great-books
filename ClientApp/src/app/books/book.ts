export interface IBook {
  id: string;
  url: string;
  title: string;
  subtitle: string;
  authors: IBookAuthor[];
  identifiers: IBookIdentifier[];
  subjects: IBookSubject[];
  publishers: IBookPublisher[];
  publish_date: string;
  excerpts: IBookExcerpt[];
  cover: IBookCover[];
  number_of_pages: number;
  weight: string;
}

export interface IBookAuthor {
  name: string;
  url: string;
}

export interface IBookIdentifier {
  lccn: string[];
  openLibrary: string[];
  isbn_10: string[];
  wikidata: string[];
  goodreads: string[];
  libraryanything: string[];
}

export interface IBookSubject {
  url: string;
  name: string;
}

export interface IBookPublisher {
  name: string;
}

export interface IBookExcerpt {
  comment: string;
  text: string;
}

export interface IBookCover {
  small: string;
  medium: string;
  large: string;
}
