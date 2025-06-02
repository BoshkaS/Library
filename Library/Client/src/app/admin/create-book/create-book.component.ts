import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BookDTO } from '../../dto/bookDTO.model';
import { Book } from '../../dto/models/book.model';
import { BookRequest } from '../../dto/models/bookRequest.model';
import { BookDTORequest } from '../../dto/bookDTORequest.model';
import { Author } from '../../dto/models/author.model';
import { map, throwError } from 'rxjs';
import { Category } from '../../dto/models/category.model';
import { PublishingHouse } from '../../dto/models/publising-house.model';
import { Language } from '../../dto/models/language.model';
import { Location } from '@angular/common';

@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styleUrl: './create-book.component.css',
})
export class CreateBookComponent implements OnInit {
  isOpen = false;
  authors = [];
  publishingHouses = [];
  languages = [];
  categories = [];
  selectedAuthor: string = '';
  selectedHouse: string = '';
  selectedLang: string = '';
  selectedCategory: string = '';

  status: 'initial' | 'uploading' | 'success' | 'fail' = 'initial';
  file: File | null = null; // Variable to store file
  photoUrl: string = '';

  @Output() onClose = new EventEmitter();

  constructor(
    private http: HttpClient,
    private toastr: ToastrService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.http
      .get<Author[]>('https://localhost:5001/api/author')
      .pipe(
        map((responseData: Author[]) => {
          const authorsNames = responseData.map((item) => item.pseudonym);
          return authorsNames;
        })
      )
      .subscribe((authors) => (this.authors = authors));
    this.http
      .get<PublishingHouse[]>('https://localhost:5001/api/publishinghouse')
      .pipe(
        map((responseData: PublishingHouse[]) => {
          const housesNames = responseData.map((item) => item.name);
          return housesNames;
        })
      )
      .subscribe(
        (publishingHouses) => (this.publishingHouses = publishingHouses)
      );
    this.http
      .get<Language[]>('https://localhost:5001/api/language')
      .pipe(
        map((responseData: Language[]) => {
          const langNames = responseData.map((item) => item.name);
          return langNames;
        })
      )
      .subscribe((languages) => (this.languages = languages));
    this.http
      .get<Category[]>('https://localhost:5001/api/category')
      .pipe(
        map((responseData: Category[]) => {
          const categoryNames = responseData.map((item) => item.name);
          return categoryNames;
        })
      )
      .subscribe((categories) => (this.categories = categories));
  }

  onCreateBook(bookInput: any) {
    const book: Book = {
      bookId: 0,
      title: bookInput.title,
      description: bookInput.description,
      yearOfPublication: bookInput.yearOfPublication,
      numberOfBorrows: 0,
      numberOfComments: 0,
      numberOfLikes: 0,
      bookImage: this.photoUrl,
      isbn: bookInput.isbn,
      createdDate: null,
    };
    console.log(book);
    //const authors = bookInput.authorNames.split();
    const bookDTO: BookDTORequest = {
      book: book,
      authorNames: [this.selectedAuthor],
      language: this.selectedLang,
      category: this.selectedCategory,
      publishingHouse: this.selectedHouse,
    };
    console.log(bookDTO);

    this.http
      .post('https://localhost:5001/api/book/add-book-request', bookDTO)
      .subscribe({
        next: (responseData) =>
          this.toastr.success('Ви успішно додали автора!'),
        error: (error) => console.log(error),
        complete: () => console.log('Request has completed'),
      });
  }

  onChange(event) {
    const file: File = event.target.files[0];

    if (file) {
      this.file = file;
    }
  }

  onUpload() {
    this.status = 'uploading';
    const formData = new FormData();

    formData.append('file', this.file, this.file.name);
    this.http
      .post('https://localhost:5001/api/book/add-photo-book', formData, {
        responseType: 'text' as 'json',
      })
      .subscribe({
        next: (Url: string) => {
          this.status = 'success';
          this.photoUrl = Url;
          console.log(this.photoUrl);
        },
        error: (error: any) => {
          this.status = 'fail';
          return throwError(() => error);
        },
      });
  }

  closeHandler() {
    this.location.back();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }

  selectHandlerAuthor(attributes: { value: string }) {
    this.selectedAuthor = attributes.value;
    console.log(attributes);
  }

  selectHandlerHouse(attributes: { value: string }) {
    this.selectedHouse = attributes.value;
    console.log(attributes);
  }

  selectHandlerLang(attributes: { value: string }) {
    this.selectedLang = attributes.value;
    console.log(attributes);
  }

  selectHandlerCategory(attributes: { value: string }) {
    this.selectedCategory = attributes.value;
    console.log(attributes);
  }
}
