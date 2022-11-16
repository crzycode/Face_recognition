import { DialogComponent } from './../dialog/dialog.component';
import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl} from '@angular/forms';
import{ServiceService} from '.././Services/service.service'
import { Observable, Subject } from 'rxjs';
import { WebcamImage } from 'ngx-webcam';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  permission!:boolean; 
  data:string ='';
  stream: any = null;
  status: any = null;
  trigger: Subject<void> = new Subject();
  previewImage: string = '';
  btnLabel: string = 'Capture image';
  constructor(private user:ServiceService,public dialog: MatDialog) {
    this.stream = null;
   }
   ngOnInit(): void {
    this.checkPermissions();
  }
   
  
  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(DialogComponent, {
      
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
      
    });
  }
  get $trigger(): Observable<void> {
    return this.trigger.asObservable();
  }
  snapshot(event: WebcamImage) {
    console.log(event);
    this.previewImage = event.imageAsDataUrl;
    this.btnLabel = 'Re capture image'
  }
  checkPermissions() {
    navigator.mediaDevices.getUserMedia({
      video: {
        width:1000,
        height:1000
      }
    }).then((res) => {
      console.log("response", res);
      this.stream = res;
      this.status = 'My camera is accessing';
      this.btnLabel = 'Capture image';
      this.permission = true;
      
    }).catch(err => {
      console.log(err);
      if(err?.message === 'Permission denied') {
        this.status = 'Permission denied please try again by approving the access';
        this.openDialog('0ms','0ms');
        this.permission = false;
        
      } else {
        this.status = 'You may not having camera system, Please try again ...';
      }
    })
  }

  
  userdata = new FormGroup(
    {
      fullname: new FormControl(''),
      email: new FormControl(''),
      password: new FormControl(''),
      
    }

    
  )
  userse(){
    const d ={
      data:this.userdata.value,
      face:this.previewImage
    }
    return d;
  }
  saveuserdata(){
    
    this.trigger.next();
    this.previewImage
    var h = this.userse();
    const dive = {
      fullname:h.data.fullname,
      email:h.data.email,
      password:h.data.password,
      face:h.face,

    }
    this.user.saveuserdata(dive).subscribe((result)=>{
      console.log(result);
    });

  }

}
