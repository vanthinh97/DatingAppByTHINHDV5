import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FaceApi } from '../_models/faceApi';


@Injectable({
  providedIn: 'root'
})
export class FaceApiService {
  baseUrl = environment.apiUrl;
  

  constructor(private http: HttpClient) { }

  sendUrlImage(urlImageUI: string) {
    return this.http.post<FaceApi>(this.baseUrl + 'users/face-detected-url', {urlImage: urlImageUI})
  }
}
