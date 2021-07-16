using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoAvalara.DAL;
using ToDoAvalara.Model;

namespace ToDoAvalara.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        TaskDAL dal;

        public TaskController()
        {
            dal = new TaskDAL();
        }

        [HttpGet]
        public IEnumerable<Task> GetToDoList(int todoId, int taskId)
        {
            return dal.GetTaskList(todoId, taskId);
        }

        [HttpPost]
        public void AddNewTask(Task task)
        {
            this.dal.AddNewTask(task);
        }

        [HttpDelete]
        public void DeleteTask(int todoId, int taskId)
        {
            this.dal.DeleteTask(todoId, taskId);
        }

        [HttpPut]
        public void UpdateTask(Task task)
        {
            this.dal.UpdateTask(task);
        }
    }
}
