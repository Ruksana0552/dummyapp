import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CosmosdbComponent } from './cosmosdb/cosmosdb.component';
import { ApiserviceService } from './shared/apiservice.service';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { AuthGuardGuard } from './guards/auth-guard.guard';
import { SignupComponent } from './signup/signup.component';
@NgModule({
  declarations: [
    AppComponent,
    CosmosdbComponent,
    LoginComponent,
    SignupComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [ApiserviceService,AuthGuardGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
