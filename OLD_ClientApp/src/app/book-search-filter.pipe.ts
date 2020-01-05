import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'bookSearchFilter'
})
export class BookSearchFilterPipe implements PipeTransform {

  transform(items: any[], filter: any): any {
    if (!items || !filter) {
      return items;
    }

    return items.filter(item => item.title.toLowerCase().indexOf(filter.toLowerCase()) !== -1);
  }

}
