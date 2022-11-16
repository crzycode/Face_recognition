import { StickynavbarModule } from './stickynavbar/stickynavbar.module';
import { RegistrationModule } from './registration/registration.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';







@NgModule({
  declarations: [
    AppComponent,
 
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RegistrationModule,
    StickynavbarModule
  

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
