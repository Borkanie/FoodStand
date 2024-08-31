using FoodMeasuringObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace JSONService
{
    internal class JSONDatabase<T>
    {
        private List<T> Values { get; set; } = new List<T>();

        private string filepath;

        public JSONDatabase(string path)
        {
            if(File.Exists(path)) 
            {
                try
                {
                    filepath = path;
                    Values = JsonConvert.DeserializeObject<List<T>>( File.ReadAllText(path));
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

        private bool UpdateList()
        {
            try
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                var obj = JsonConvert.SerializeObject(Values, Formatting.Indented);
                File.WriteAllText(filepath, obj);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Add(T element)
        {
            var _oldval = Values.ToArray();
            Values.Add(element);
            if (UpdateList())
            {
                return true;
            }
            else
            {
                Values = _oldval.ToList<T>();
                return false;
            }
        }

        public bool Remove(T element)
        {
            var _oldval = Values.ToArray();
            Values.Remove(element);
            if (UpdateList())
            {
                return true;
            }
            else
            {
                Values = _oldval.ToList<T>();
                return false;
            }
        }

        public bool Reset()
        {
            var _oldval = Values.ToArray();
            Values.Clear();
            if (UpdateList())
            {
                return true;
            }
            else
            {
                Values = _oldval.ToList<T>();
                return false;
            }
        }
        
        public bool Update(T item)
        {
            if (Values.Contains(item) || !Values.Remove(item))
            {
                return false;
            }
            Values.Add(item);
            return UpdateList();            
        }

        /// <summary>
        /// This will return a copy of the values in the lsit not a pointer to the list tiself.
        /// </summary>
        /// <returns></returns>
        public T[] GetElements()
        {
            return (T[])Values.ToArray().Clone();
        }

        public bool Contains(T element)
        {
            return Values.Contains(element);
        }
    }
}
