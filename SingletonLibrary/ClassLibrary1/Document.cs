using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Document
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Document(string name, string content)
        {
            Name = name;
            Content = content;
            Date = DateTime.Now;
        }
    }
}
