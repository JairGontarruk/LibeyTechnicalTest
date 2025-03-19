import { NgModule } from "@angular/core";
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { RouterModule } from "@angular/router";
import { UserModule } from "./User/user/user.module";
import { CommonModule } from '@angular/common'; // IMPORTAR CommonModule
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
@NgModule({
	declarations: [AppComponent],
	imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule, RouterModule,HttpClientModule ,CommonModule,FormsModule,ReactiveFormsModule, UserModule ],
	providers: [],
	bootstrap: [AppComponent],
})
export class AppModule {}