using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniHttpServerEgorFramework.Settings
{
    public class Singleton
    {
        private static Singleton instance;
        public JsonEntity Settings { get; private set; }

        private Singleton()
        {
            //Берем настройки из settings.json
            if (!File.Exists("C:\\Users\\egors\\source\\repos\\OrisSemestrWork1\\OrisSemestrWork1\\MiniHttpServerEgor.Framework\\settings.json"))
            {
                throw new Exception("Файла settings.json по пути settings.json не существует");
            }

            string jsonFromFile = File.ReadAllText("C:\\Users\\egors\\source\\repos\\OrisSemestrWork1\\OrisSemestrWork1\\MiniHttpServerEgor.Framework\\settings.json");
            JsonEntity? jsonEntity = JsonSerializer.Deserialize<JsonEntity>(jsonFromFile);

            if (jsonEntity == null)
            {
                throw new Exception("jsonObject пустой!");
            }

            Settings = jsonEntity;
        }


        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
}
