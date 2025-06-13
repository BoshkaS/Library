import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookDTO } from '../../dto/bookDTO.model';
import { Book } from '../../dto/models/book.model';
import { Author } from '../../dto/models/author.model';
import { map, throwError } from 'rxjs';
import { PublishingHouse } from '../../dto/models/publising-house.model';
import { Language } from '../../dto/models/language.model';
import { Category } from '../../dto/models/category.model';
import { ToastrService } from 'ngx-toastr';
import { BookDTORequest } from '../../dto/bookDTORequest.model';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrl: './edit-book.component.css',
})
export class EditBookComponent implements OnInit {
  bookId!: number;
  book!: Book;
  authors: string[] = [];
  publishingHouses: string[] = [];
  languages: string[] = [];
  categories: string[] = [];
  bookDTO: BookDTO;

  selectedAuthor: string = '';
  selectedLang: string = '';
  selectedCategory: string = '';
  selectedHouse: string = '';

  status: 'initial' | 'uploading' | 'success' | 'fail' = 'initial';
  file: File | null = null; // Variable to store file
  photoUrl: string = '';

  @Output() onClose = new EventEmitter();

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.bookId = +this.route.snapshot.paramMap.get('id')!;
    this.fetchBookDetails();
    this.loadSelectors();
  }

  fetchBookDetails() {
    this.http
      .get<BookDTO>(`https://localhost:5001/api/book/${this.bookId}`)
      .subscribe((dto) => {
        this.book = dto.book;
        this.selectedAuthor = dto.authorNames[0];
        this.selectedLang = dto.language;
        this.selectedCategory = dto.category;
        this.selectedHouse = dto.publishingHouse;
        this.bookDTO = dto;
        console.log(this.book);
      });
  }

  loadSelectors() {
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

  onChange(event: any) {
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

  onUpdateBook(formData: any) {
    if (this.status === 'uploading') {
      this.toastr.warning('Зачекайте, поки фото буде завантажено!');
      return;
    }

    const imageUrl = this.photoUrl || this.book.bookImage;

    const book: Book = {
      bookId: this.book.bookId,
      title: formData.title,
      yearOfPublication: formData.yearOfPublication,
      description: formData.description,
      numberOfBorrows: this.book.numberOfBorrows,
      numberOfComments: this.book.numberOfComments,
      numberOfLikes: this.book.numberOfLikes,
      isbn: formData.isbn,
      bookImage: this.photoUrl,
      createdDate: this.book.createdDate,
    };
    const bookDTO: BookDTORequest = {
      book: book,
      authorNames: [this.selectedAuthor],
      language: this.selectedLang,
      category: this.selectedCategory,
      publishingHouse: this.selectedHouse,
    };

    console.log(bookDTO);

    this.http
      .put('https://localhost:5001/api/book/update-book', bookDTO)
      .subscribe({
        next: (responseData) =>
          this.toastr.success('Ви успішно додали автора!'),
        error: (error) => console.log(error),
        complete: () => console.log('Request has completed'),
      });
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

  closeHandler() {
    this.router.navigate([`/book/${this.bookId}`]);
  }
}
