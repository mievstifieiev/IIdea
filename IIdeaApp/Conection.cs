using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IIdeaApp
{
    [Serializable]
    public class Conection : ISerializable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Projects { get; set; }

        private Conection(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetInt32(nameof(Id));
            Name = info.GetString(nameof(Name));
            Password = info.GetString(nameof(Password));
            Projects = info.GetString(nameof(Projects));
        }

        public Conection(string _name, string _password, string _progectrs = "")
        {
            Name = _name;
            Password = _password;
            Projects = _progectrs;
        }
        public Conection(long _id, string _name, string _password, string _progectrs = "")
        {
            Id = _id;
            Name = _name;
            Password = _password;
            Projects = _progectrs;
        }

        public Conection()
        {

        }

        public void GetObjectData(SerializationInfo info,StreamingContext context)
        {
            info.AddValue("id", this.Id);
            info.AddValue("name", this.Name);
            info.AddValue("password", this.Password);
            info.AddValue("projects", this.Projects);
        }

        public string Http_post(string url, string data)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            using (var requestStream = req.GetRequestStream())//отправляем поток данных
            using (var sw = new StreamWriter(requestStream)) //создаём переменную в которой будет храниться запрос
            {
                sw.Write(data);//записываем в поток данные
            }

            using (var responseStream = req.GetResponse().GetResponseStream())//возвращаем поток данных
            using (var sr = new StreamReader(responseStream))//переменная в которой храниться ответ
            {
                var result = sr.ReadToEnd();//считывем ответ в переменную
                return result;
            }
        }

        public string Http_put(string url)
        {
            string locURL = url + "/" + Convert.ToString(this.Id);
            string data = JsonConvert.SerializeObject(this);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(locURL);
            req.Method = "PUT";
            req.ContentType = "application/json";
            using (var requestStream = req.GetRequestStream())//отправляем поток данных
            using (var sw = new StreamWriter(requestStream)) //создаём переменную в которой будет храниться запрос
            {
                sw.Write(data);//записываем в поток данные
            }

            using (var responseStream = req.GetResponse().GetResponseStream())//возвращаем поток данных
            using (var sr = new StreamReader(responseStream))//переменная в которой храниться ответ
            {
                var result = sr.ReadToEnd();//считывем ответ в переменную
                return result;
            }
        }

        public List<Conection> Http_GET(string url)
        {
            List<Conection> conections;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ContentType = "application/json";
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            //resp.Close();
            var text = sr.ReadToEnd();
            resp.Close();
            sr.Close();
            conections = JsonConvert.DeserializeObject<List<Conection>>(text);
            return conections;
        }

        public Conection Http_GET_id(string url)
        {
            string locURL = url + "/" + Convert.ToString(this.Id);
            Conection conections;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(locURL);
            req.ContentType = "application/json";
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            //resp.Close();
            var text = sr.ReadToEnd();
            resp.Close();
            sr.Close();
            conections = JsonConvert.DeserializeObject<Conection>(text);
            return conections;
        }

        public void SerProj(Project project)
        {
            List<string> projInList = new List<string>();
            project.toJSONstring(projInList);
            string jsonString = JsonConvert.SerializeObject(projInList);
            Projects = jsonString;
        }

        public Project DeserProj ()
        {
            List<string> projInList;
            projInList = JsonConvert.DeserializeObject<List<string>>(Projects);
            Project project;
            project = new Project(projInList[0], projInList[1], Convert.ToInt32(projInList[2]), Convert.ToInt32(projInList[3]), projInList[4]);
            int i = 5;
            while (i < projInList.Count)
            {
                project.PartAdd(projInList[i], projInList[i + 1], Convert.ToInt32(projInList[i + 2]), Convert.ToInt32(projInList[i + 3]), projInList[i+4]);
                i += 5;
            }
            return project;
        }
    }
}
