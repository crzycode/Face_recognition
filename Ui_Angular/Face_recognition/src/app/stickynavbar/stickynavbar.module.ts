import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopNavComponent } from './top-nav/top-nav.component';
import {MatToolbarModule} from '@angular/material/toolbar';

import {MatIconModule,MatIcon} from '@angular/material/icon';
import {MatButtonModule,MatButton} from '@angular/material/button';






@NgModule({
  declarations: [
    TopNavComponent,
    
    
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule

    


    
  ],
  exports: [
    TopNavComponent,
    MatIcon
    
  ]
})
export class StickynavbarModule { }
