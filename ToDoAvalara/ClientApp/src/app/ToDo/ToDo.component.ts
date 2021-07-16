import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-ToDo',
  templateUrl: './ToDo.component.html',
  styleUrls: ['./ToDo.component.scss']
})
export class ToDoComponent implements OnInit {

  public ToDoList : ToDo[];


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.baseUrl = this.baseUrl + 'ToDo';
    this.loadAllToDo();
  }

  ngOnInit() {
  }

  onDelete(id: number) {
    this.http.delete(this.baseUrl + '?id=' + id, {}).subscribe(data => {
      this.loadAllToDo();
    });
  }

  onSubmit(Form : NgForm){
    this.http.post(this.baseUrl + '?name=' + Form.value.name, {}).subscribe(data => {
      this.loadAllToDo();
    });
    Form.reset();
  }

  loadAllToDo() {
    this.http.get<ToDo[]>(this.baseUrl).subscribe(result => {
      this.ToDoList = result;
    }, error => console.error(error));
  }
}


