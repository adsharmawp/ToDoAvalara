using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToDoAvalara.Model;


namespace ToDoAvalara.DAL
{
    public class TaskDAL
    {
        public List<Task> GetTaskList(int todoId, int taskId)
        {
            var listOfToDo = ToDoDAL.GetData();
            var todo = listOfToDo.ToDoList.FirstOrDefault(t => t.Id == todoId);
            if(todo != null)
            {
                if(taskId != 0)
                {
                    return todo.Tasks.FindAll(t => t.Id == taskId);
                }
                return todo.Tasks;
            }
            return null;
        }

        public void AddNewTask(Task task)//int todoId, string subject, string description)
        {
            var listOfToDo = ToDoDAL.GetData();
            var todo = listOfToDo.ToDoList.FirstOrDefault(t => t.Id == task.ToDoId);
            if (todo != null)
            {
                int maxId = 1;
                if (todo.Tasks.Count > 0)
                { 
                    maxId = todo.Tasks.OrderByDescending(t => t.Id).Take(1).First().Id;
                }
                 
                todo.Tasks.Add(new Task
                {
                    Id = maxId + 1,
                    ToDoId = task.ToDoId,
                    Subject = task.Subject,
                    Description = task.Description,
                    Status = task.Status
                });;
                ToDoDAL.SaveJson(listOfToDo);
            }
        }

        public void DeleteTask(int todoId, int taskId)
        {
            var listOfToDo = ToDoDAL.GetData();
            var todo = listOfToDo.ToDoList.FirstOrDefault(t => t.Id == todoId);
            if (todo != null)
            {
                var task = todo.Tasks.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    todo.Tasks.Remove(task);
                    ToDoDAL.SaveJson(listOfToDo);
                }
            }
        }

        public void UpdateTask(Task task)
        {
            var listOfToDo = ToDoDAL.GetData();
            var todo = listOfToDo.ToDoList.FirstOrDefault(t => t.Id == task.ToDoId);
            if (todo != null)
            {
                var dataTask = todo.Tasks.FirstOrDefault(t => t.Id == task.Id);
                if (dataTask != null)
                {
                    dataTask.Subject = task.Subject;
                    dataTask.Description = task.Description;
                    dataTask.Status = task.Status;
                    ToDoDAL.SaveJson(listOfToDo);
                }
            }
        }
    }
}
