import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CosmosdbComponent } from './cosmosdb/cosmosdb.component';
import { AuthGuardGuard } from './guards/auth-guard.guard';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

const routes: Routes = [
 // {path:'login',redirectTo:'login',pathMatch:'full'},
  {path:"cosmosdb",component:CosmosdbComponent,canActivate:[AuthGuardGuard]},
  {path:"login",component:LoginComponent},
  {path:"signup",component:SignupComponent}
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
