using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAvalara.Model
{
    public class Task
    {
        public int Id { get; set; }
        public int ToDoId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
    }

    public class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Task> Tasks { get; set; }
    }

    public class ListOfToDo
    {
        public List<ToDo> ToDoList { get; set; }
    }
}
