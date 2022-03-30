import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginform:FormGroup;
  value1:any;
  value2:any;
  invalidlogin:boolean
  constructor(private http:HttpClient,private router:Router,private formbuilder:FormBuilder) { }

  ngOnInit(): void {
  }

  login(loginForm:NgForm){

    const credentials=  { 'username':loginForm.value.username,
      'password':loginForm.value.password
  
      }
  this.http.post("https://localhost:44362/api/Authentication/post",credentials).
  subscribe(res=>{
    const token=(<any>res).token;
    var bearerheaders=new Headers();
    localStorage.setItem("jwt",token)
    this.invalidlogin=false;
    if(token)
    {
bearerheaders.append('Authorization','Bearer'+token);
   this.router.navigate(['/cosmosdb'])
    }
   else
   this.router.navigate(['/login'])
  },err=>{
    this.invalidlogin=true
  })
}

}
