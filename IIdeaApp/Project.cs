using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IIdeaApp
{
    [Serializable]
    public class Project
    {
        public string name;
        public string ancestor;
        [JsonIgnore]
        public List<int> links;
        [JsonIgnore]
        public List<Project> part;
        public int id;
        [JsonIgnore]
        public List<int> levels = new List<int>();
        public string status;
        

        public Project()
        {

        }
        public Project(string _name, string _ansestor, int _id = 0, int link = -1, string stat = "not started;")
        {
            Random random = new Random();
            links = new List<int>();
            if (link == -1)
            {
                id = random.Next(10000) + 1;
                links.Add(_id);
            }
            else
            {
                id = _id;
                links.Add(link);
            }
            name = _name;
            ancestor = _ansestor;
            status = stat;
            part = new List<Project>();
        }

        public void StatusChange(string statNew, int _id)
        {
            int ct = part.Count;
            if (id == _id)
            {
                int j = 0;
                if (ct>0)
                {
                    if (statNew.IndexOf("in progress")>-1)
                    {
                        for (int i = 0; i < ct; i++)
                        {
                            if ((part[i].status.IndexOf("in progress") > -1) || (part[i].status.IndexOf("completed") > -1))
                            {
                                j++;
                            }
                        }
                        if (j>0)
                        {
                            status = Convert.ToString(DateTime.Now) + " status changed: " + statNew + ";";
                        }
                    }
                    else if (statNew.IndexOf("completed") > -1)
                    {
                        for (int i = 0; i < ct; i++)
                        {
                            if (part[i].status.IndexOf("completed") > -1)
                            {
                                j++;
                            }
                        }
                        if (j == ct)
                        {

                            status = Convert.ToString(DateTime.Now) + " status changed: " + statNew + ";";
                        }
                    }

                }
                else
                {
                    status = Convert.ToString(DateTime.Now) + " status changed: " + statNew + ";";
                }
            }
            else
            {
                for (int i = 0; i < ct; i++)
                {
                    part[i].StatusChange(statNew, _id);
                }
                int k = 0;
                for (int i = 0; i < ct; i++)
                {
                    if ((part[i].status.IndexOf("in progress") > -1) || (part[i].status.IndexOf("completed") > -1))
                    {
                        k++;
                    }
                }
                if (k > 0)
                {
                    status = Convert.ToString(DateTime.Now) + " status changed: " + "in progress;";
                }
            }

        }

        public void toJSONstring(List<string> vs)
        {
            vs.Add(name);
            vs.Add(ancestor);
            vs.Add(id.ToString());
            vs.Add(links[0].ToString());
            vs.Add(status);
            foreach (var item in part)
            {
                item.toJSONstring(vs);
            }
        }

        public void AddLink(int _link)
        {
            int i;
            i = links.Count;
            links[i] = _link;
        }

        public string WritePrList(string prList, List<int> counter, List<int> lev)
        {
            if (counter.Count > 0)
            {
                for (int i = 0; i < counter.Count; i++)
                {
                    prList += " ";
                }
            }

            int co = part.Count;

            if (counter.Count > 0)
            {
                foreach (var item in counter)
                {
                    prList += item.ToString() + ".";
                }
            }
            //lev.Add(0);
            //List<int> ct2 = lev;
            prList +=" " +id+ " " + name + "; Status: " + status;

            if (co>0)
            {
                int j = 0;
                for (int i = 0; i < co; i++)
                {
                    if (part[i].status.IndexOf("completed")>-1)
                    {
                        j++;
                    }
                }
                double pr = 100 / (double)co * j;
                prList += " completed by " + pr.ToString() + "% |";
            }

            if (links.Count > 0)
            {
                prList += " Links: ";
                foreach (var item in links)
                {
                    prList += item + " | ";
                }
            }
            prList += "\n";

            if (part.Count > 0)
            {
                lev.Add(0);
                List<int> ct2 = lev;
                int k = counter.Count;
                counter.Add(1);
                for (int i = 0; i < part.Count; i++)
                {
                    List<int> ct = new List<int>();
                    foreach (var item in counter)
                    {
                        ct.Add(item);
                    }
                    prList = part[i].WritePrList(prList, ct, ct2);
                    counter[k]++;
                    lev[k]++;
                }
            }
            return prList;
        }

        public void PartAdd(string _name, string _ansestor, int _id, int link=-1, string stat = "not started;")
        {
            int j;
            if (link == -1)
            {
                j = _id;
            }
            else
            {
                j = link;
            }

            if ((this.name == _ansestor) && (id == j))
            {
                this.part.Add(new Project(_name, _ansestor, _id, link, stat));
                foreach (var item in part)
                {
                    item.PartAdd(_name, _ansestor, _id, link, stat);
                }
            }
            else
            {
                foreach (var item in part)
                {
                    item.PartAdd(_name, _ansestor, _id, link, stat);
                }
            }
        }


        public int FindDeep(List<int> deepList, int deep)
        {
            deepList[deep] = part.Count;
            deep++;
            deepList.Add(0);
            for (int i = 0; i < part.Count; i++)
            {
                int deep1 = deep;

            }
            return deep;
        }


    }

}
