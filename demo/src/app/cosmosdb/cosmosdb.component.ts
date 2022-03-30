import { Component, OnInit } from '@angular/core';
import { User } from './User';
import {FormBuilder,FormControl,FormGroup,ReactiveFormsModule} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { ApiserviceService } from '../shared/apiservice.service';
import { first } from 'rxjs';
import { Router } from '@angular/router';
@Component({
  selector: 'app-cosmosdb',
  templateUrl: './cosmosdb.component.html',
  styleUrls: ['./cosmosdb.component.css']
})
export class CosmosdbComponent implements OnInit {
id:string
  user:User[];
userobj:User=new User();
formvalue1:FormGroup;
filetoupload:any;
  constructor(private service:ApiserviceService,private http:HttpClient,private formbuiler:FormBuilder,private router:Router) { 
    this.getUser();
    this.formvalue1=this.formbuiler.group({
      id:[''],
      firstname:[''],
      lastname:[''],
   email:[''],
   file:['']
    })  }

  ngOnInit(): void {
  }
  getUser()
  {
    this.service.getData().subscribe((res)=>{this.user= res},
    (err)=>console.log("error"))
  }
  createDb()
  {
    this.service.createdb().subscribe(()=>{alert("Db successfully created")},
    (err)=>console.log("error"))

  }
createCollection()
{
  this.service.createcollection().subscribe(()=>{console.log("collection successfully created")},
    (err)=>console.log("error"))
 

    
}
AddUser()
    {
     
      
      this.userobj.FirstName=this.formvalue1.value.firstname;
      this.userobj.LastName=this.formvalue1.value.lastname;
      this.userobj.id=this.formvalue1.value.id;
      this.userobj.email=this.formvalue1.value.email
      this.userobj.file=this.formvalue1.value.file
      this.service.post(this.userobj).subscribe((res)=>{
         console.log(res);
    alert("employee added succesfully")
    this.formvalue1.reset();
    },
    err=>{
      alert("something went wrong");
    })
   

}
deleteEmployee(row:any)
{
  this.service.deleteuser(row.id).subscribe(()=>
  
  {alert('User deleted')})
 
 }
Logout1()
{
  this.router.navigate(['/login']);
  localStorage.removeItem("jwt");
 
}

handlefileinput(e:any)
{
  this.filetoupload=e?.target?.files[0];


}
savefile()
{
  debugger
    const formData:FormData=new FormData();
    formData.append('ImageFile',this.filetoupload);
  const token=localStorage.getItem("jwt")
 formData.append('id',this.id);
    this.http.post('https://localhost:44362/api/Image/upload/id',formData,
    {
      
      headers:new HttpHeaders({
        'Authorization':`Bearer ${token}`,
        'Content-Type': 'application/json'
      }
      )}).
      subscribe((res)=>
      
      alert("fileuploaded"));
  
    }
  
  AddUser1()
  {
   
    const formData:FormData=new FormData();
  formData.append('id',this.formvalue1.value.id)
  formData.append('FirstName',this.formvalue1.value.firstname)
  formData.append('LastName',this.formvalue1.value.lastname)
  formData.append('Email',this.formvalue1.value.email)
  formData.append('ImageFile',this.filetoupload)
  this.http.post<any>('https://localhost:44362/api/Cosmos/createdocument',formData, 
  {
    headers:new HttpHeaders()}).
    subscribe((res)=>{
    console.log(res)
    
    alert("Employee Added")});
this.getUser();
    this.formvalue1.reset()
  }



}
