using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToDoAvalara.Model;

namespace ToDoAvalara.DAL
{
    public class ToDoDAL
    {
        public List<ToDo> GetToDoList(int id)
        {   
            if (id == 0)
            {
                return GetData().ToDoList;
            }
            else
            {
                return GetData().ToDoList.FindAll(t => t.Id == id);
            }
        }

        public void AddNewToDo(string name)
        {
            var listOfToDo = GetData();
            int maxId = 1;
            if (listOfToDo.ToDoList.Count > 0)
            {
                maxId = listOfToDo.ToDoList.OrderByDescending(t => t.Id).Take(1).First().Id;
            } 
            ToDo newToDo = new ToDo { Id = maxId + 1, Name = name, CreateDate = DateTime.Now, Tasks = new List<Task>() };
            listOfToDo.ToDoList.Add(newToDo);
            SaveJson(listOfToDo);
        }

        public void UpdateToDo(int id, string name)
        {
            var listOfToDo = GetData();
            var todo = listOfToDo.ToDoList.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.Name = name;
                SaveJson(listOfToDo);
            }
        }

        public void DeleteToDo(int id)
        {
            var listOfToDo = GetData();
            var todo = listOfToDo.ToDoList.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                listOfToDo.ToDoList.Remove(todo);
                SaveJson(listOfToDo);
            }
        }


        public static ListOfToDo GetData()
        {
            var JSON = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"FileData\\ToDo.json"));
            return JsonConvert.DeserializeObject<ListOfToDo>(JSON);
        }

        public static void SaveJson(ListOfToDo listOfToDo)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), $"FileData\\ToDo.json")))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, listOfToDo);
            }
        }
    }
}
