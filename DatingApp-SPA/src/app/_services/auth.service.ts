import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }

login(model: any) {
  return this.http.post(this.baseUrl + 'login', model).pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
        localStorage.setItem('user', JSON.stringify(user.user));
        // this.decodedToken = this.jwtHelper.decodeToken(user.token);
        // this.currentUser = user.user;
        // this.changeMemberPhoto(this.currentUser.photoUrl);
      }
    })
  );
}

register(model: any){
  return this.http.post(this.baseUrl + 'register', model);
}

}
