import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup,FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
public signupform !:FormGroup;
  constructor(private formbuilder:FormBuilder,private httclient:HttpClient,private router:Router) { }

  ngOnInit(): void {

    this.signupform=this.formbuilder.group({
      fullname:[''],
      email:[''],
      password:[''],
      mobile:['']
    })
  }
signUp()
{
this.httclient.post<any>("http://localhost:3000/signupusers",this.signupform.value).subscribe
(res=>{
  alert("signup succesful");
  this.signupform.reset();
  this.router.navigate(['login']);
},
err=>{
  alert("seomething went wrong")
})
}
}