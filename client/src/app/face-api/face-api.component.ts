import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FaceApi } from '../_models/faceApi';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { BusyService } from '../_services/busy.service';
import { FaceApiService } from '../_services/face-api.service';
import { Location } from "@angular/common";

@Component({
  selector: 'app-face-api',
  templateUrl: './face-api.component.html',
  styleUrls: ['./face-api.component.css']
})
export class FaceApiComponent implements OnInit{
  @ViewChild('urlForm') urlForm: NgForm;
  listFaceInfo: FaceApi[] = []; 
  uploader!: FileUploader;
  hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  user!: User;
  show: boolean = false;
  count = 0;
  urlImageUI!: string;


  constructor(private accountService: AccountService, private busyService: BusyService, 
      private faceService: FaceApiService, private route : ActivatedRoute, 
      private router: Router, private readonly location: Location) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      if (user) {
        this.user = user
      }
    });
  }

  ngOnInit(): void {
    this.initializeUploader();
    this.route.queryParams.subscribe(params => {
      this.urlImageUI = params['urlImage'];
      if (this.urlImageUI) {
        this.sendUrImage();
      }
      this.location.go(this.router.url.split("?")[0], params.toString());
    });
  }

  sendUrImage() {
    // console.log(this.urlImageUI);
    this.faceService.sendUrlImage(this.urlImageUI).subscribe(data => {
      this.listFaceInfo.push(data);
    })
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e
  }

  showUpload() {
    this.show = true;
    this.count++;
    if (this.count == 2) {
      this.show = false;
      this.count = 0;
    }
  }

  
  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/face-detected',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingAll = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onProgressAll = () => {
      this.busyService.busy();
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        this.busyService.idle();
        const faceInfo: FaceApi = JSON.parse(response);
        this.listFaceInfo.push(faceInfo);
        this.show = false;
        this.count = 0;
      }
    }
  }


  
}
