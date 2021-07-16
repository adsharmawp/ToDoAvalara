import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-Task',
  templateUrl: './Task.component.html',
  styleUrls: ['./Task.component.scss']
})
export class TaskComponent implements OnInit {

  todo : ToDo;
  newTask : Task = { id:0, todoId:0, subject : "", description:"", status:"NotStarted", createDate:new Date()};
  toDoId: number;

  constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.baseUrl = this.baseUrl;
   }

  ngOnInit() {
    this.route.params.subscribe(
      (params) => {
        this.toDoId = +params['id'];
        this.newTask.todoId = this.toDoId;
        this.loadAllTask();
      }
    );
  }

  loadAllTask() {
    this.http.get<ToDo[]>(this.baseUrl + 'ToDo?id=' + this.toDoId).subscribe(result => {
      this.todo = result[0];
    }, error => console.error(error));
  }

  onSubmit(Form : NgForm){
    if(this.newTask.subject === "" || this.newTask.description === ""){
      alert('Please provide Subject and Description.')
      return;
    }
    if(this.newTask.id == 0)
    {
      this.http.post(this.baseUrl + 'Task', this.newTask).subscribe(data => {
        alert('New task added to your todo list.');
        this.loadAllTask();
      });
    }
    else
    {
      this.http.put(this.baseUrl + 'Task', this.newTask).subscribe(data => {
        alert('Task updated.');
        this.loadAllTask();
      });
    }
    this.newTask  = { id:0, todoId: this.toDoId, subject : "", description:"", status:"NotStarted", createDate:new Date()};
  }

  onEdit(id: number) {
    this.http.get<Task[]>(this.baseUrl + 'Task?todoId=' + this.toDoId + '&taskId=' + id).subscribe(result => {
      this.newTask = result[0];
    }, error => console.error(error));
  }

  onDelete(id: number) {
    this.http.delete(this.baseUrl + 'Task?todoId=' + this.toDoId  + '&taskId=' + id, {}).subscribe(data => {
      this.loadAllTask();
    });
  }

  onCancel() {
    this.newTask  = { id:0, todoId: this.toDoId, subject : "", description:"", status:"NotStarted", createDate:new Date()};
    return false;
  }
}
