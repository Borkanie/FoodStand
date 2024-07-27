using FoodMeasuringObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSONService
{
    internal class JSONDatabase<T>
    {
        private List<T> Values { get; } = new List<T>();

        private string filepath;

        public JSONDatabase(string path)
        {
            if(File.Exists(path)) 
            {
                try
                {
                    filepath = path;
                    Values = JObject.Parse(json: File.ReadAllText(path)).ToObject<List<T>>();
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }

                
            }
            else
            {
                File.Create(path).Close();
                filepath = path;
            }
            
        }

        private void UpdateList()
        {
            if(File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            File.WriteAllText(filepath, JObject.FromObject(Values).ToString());
        }

        public void Add(T element)
        {
            Values.Add(element);
            UpdateList();
        }

        public void Remove(T element)
        {
            Values.Remove(element);
            UpdateList();
        }

        public void Reset()
        {
            Values.Clear();
            UpdateList();
        }

        /// <summary>
        /// This will return a copy of the values in the lsit not a pointer to the list tiself.
        /// </summary>
        /// <returns></returns>
        public T[] GetElements()
        {
            return Values.ToArray();
        }
    }
}
