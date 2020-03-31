using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_11.Model
{
    class Department
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public Department(string name, int id, int parentId)
        {
            Name = name;
            Id = id;
            ParentId = parentId;
        }
    }
}
