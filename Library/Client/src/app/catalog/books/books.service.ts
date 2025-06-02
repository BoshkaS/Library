import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BookDTO } from '../../dto/bookDTO.model';
import { map } from 'rxjs';
import { PaginatedResult } from '../../dto/models/pagination';
import { BookParams } from '../../dto/models/bookParams';
import { FilterDTO } from '../../dto/filterDTo.models';

@Injectable()
export class BooksService {
  booksDTO: BookDTO[] = [];
  constructor(private http: HttpClient) {}

  //     BookDTO[]
  //   >();

  //public set

  public fetchBooks(bookParams: BookParams) {
    let params = this.getPaginationHeaders(bookParams);

    return this.getPaginatedResult(params);
  }

  public filterBooks(bookParams: BookParams, filterDTO: FilterDTO) {
    let params = this.getPaginationHeaders(bookParams);

    const paginatedResult: PaginatedResult<BookDTO[]> = new PaginatedResult<
      BookDTO[]
    >();
    return this.http
      .post<BookDTO[]>('https://localhost:5001/api/book/filter', filterDTO, {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          if (response.body) {
            paginatedResult.result = response.body;
          }
          const pagination = response.headers.get('Pagination');
          if (pagination) {
            paginatedResult.pagination = JSON.parse(pagination);
          }
          return paginatedResult;
        })
      );
  }

  private getPaginatedResult(params: HttpParams) {
    const paginatedResult: PaginatedResult<BookDTO[]> = new PaginatedResult<
      BookDTO[]
    >();
    return this.http
      .get<BookDTO[]>('https://localhost:5001/api/book', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          if (response.body) {
            paginatedResult.result = response.body;
          }
          const pagination = response.headers.get('Pagination');
          if (pagination) {
            paginatedResult.pagination = JSON.parse(pagination);
          }
          return paginatedResult;
        })
      );
  }

  private getPaginationHeaders(bookParams: BookParams) {
    let params = new HttpParams();
    params = params.append('pageNumber', bookParams.pageNumber);
    params = params.append('pageSize', bookParams.pageSize);
    params = params.append('type', bookParams.type);
    return params;
  }
}
