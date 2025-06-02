import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDTO } from '../../dto/loginDTO.model';
import {
  BehaviorSubject,
  ConnectableObservable,
  Observable,
  Subject,
  map,
  mergeMap,
  tap,
} from 'rxjs';
import { UserDTO } from '../../dto/userDTO.model';
import { UpdateUserDTO } from '../../dto/updateUserDTO.model';

@Injectable()
export class AccountService {
  loggedInChanged = new Subject<boolean>();
  //private loggedIn: boolean = false;
  private currentUserSource = new BehaviorSubject<UserDTO | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(loginDTO: LoginDTO): Observable<UserDTO> {
    console.log(loginDTO);
    return this.http
      .post<UserDTO>('https://localhost:5001/api/account/login', loginDTO)
      .pipe(
        map((response: UserDTO) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.setCurrentUser(user);
          }
          return user;
        })
      );
  }

  getCurrentUser(): UserDTO | null {
    return this.currentUserSource.value;
  }

  register(userDTO: UserDTO) {
    return this.http
      .post<UserDTO>('https://localhost:5001/api/account/register', userDTO)
      .pipe(
        map((user) => {
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.setCurrentUser(user);
          }
        })
      );
  }

  setCurrentUser(user: UserDTO) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  updateUser(userDTO: UpdateUserDTO) {
    return this.http.put('https://localhost:5001/api/users', userDTO);
  }

  addPhoto(file: File) {
    const formData = new FormData();

    formData.append('file', file, file.name);

    let params = new HttpParams();
    params = params.append('email', this.getCurrentUser().email);

    return this.http.post(
      'https://localhost:5001/api/users/add-photo',
      formData,
      {
        params,
      }
    );
  }

  getPhoto() {
    let params = new HttpParams();
    params = params.append('email', this.getCurrentUser().email);
    //const photoUrl = response.secureUrl;
    return this.http
      .get<string>('https://localhost:5001/api/users/get-photo', {
        params,
        responseType: 'text' as 'json',
      })
      .pipe(
        tap((imageResponse) => {
          console.log(imageResponse);
          const currentUser = this.currentUserSource.getValue();
          if (currentUser) {
            const updatedUser: UserDTO = {
              ...currentUser,
              userImage: imageResponse,
            };
            console.log(updatedUser);
            this.setCurrentUser(updatedUser);
          }
        })
      );
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
