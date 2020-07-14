using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IIdeaApp
{

    public class Project
    {
        protected string name;
        protected string ancestor;
        protected List<int> links;
        public List<Project> part;
        public int id;
        public List<int> levels = new List<int>();
        

        public Project(string _name, string _ansestor, int _id = 0)
        {
            Random random = new Random();
            id = random.Next(10000) + 1;
            name = _name;
            ancestor = _ansestor;
            links = new List<int>();
            links.Add(_id);
            part = new List<Project>();
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

            if (counter.Count > 0)
            {
                foreach (var item in counter)
                {
                    prList += item.ToString() + ".";
                }
            }
            //lev.Add(0);
            //List<int> ct2 = lev;
            prList +=" " +id+ " " + name + ";";

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

        public void PartAdd(string _ansestor, string _name, int _id)
        {
            if ((this.name == _ansestor)&&(id==_id))
            {
                this.part.Add(new Project(_name, _ansestor, id));
                foreach (var item in part)
                {
                    item.PartAdd(_ansestor, _name, _id);
                }
            }
            else
            {
                foreach (var item in part)
                {
                    item.PartAdd(_ansestor, _name, _id);
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
