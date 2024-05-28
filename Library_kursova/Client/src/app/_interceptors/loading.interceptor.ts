import { HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusyService } from '../busyService';
import { Observable, delay, finalize } from 'rxjs';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private busyService: BusyService){}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // this.busyService.busy();
    
    // return next.handle(req).pipe(
    //   delay(1000),
    //   finalize(() => {
    //     this.busyService.idle();
    //   })
    // )
    return next.handle(req)
  }
}