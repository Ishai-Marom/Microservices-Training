using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebAPI.Models
{
    public class Data(string id, string FirstName, string LastName)
    {
        private string id = id;
        public string ID{ get {return id;} }
        public string FirstName{ get; set; } = FirstName;
        public string LastName{ get; set; } = LastName;
    }
}