using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoAvalara.DAL;
using ToDoAvalara.Model;

namespace ToDoAvalara.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        ToDoDAL dal;

        public ToDoController()
        {
            dal = new ToDoDAL();
        }

        [HttpGet]
        public IEnumerable<ToDo> GetToDoList(int id)
        {
            return dal.GetToDoList(id);
        }

        [HttpPost]
        public void AddToDo(string name)
        {
            dal.AddNewToDo(name);            
        }

        [HttpPut]
        public void UpdateToDo(int id, string name)
        {
            dal.UpdateToDo(id, name);
        }

        [HttpDelete]
        public void DeleteToDo(int id)
        {
            dal.DeleteToDo(id);
        }
    }
}
