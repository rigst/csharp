using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class Costumer
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public Costumer(String firstName, String lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public override string ToString()
        {
            return FirstName + "\t" + LastName;
        }
    }
    public class Costumers : ObservableCollection<Costumer>
    {
        public Costumers() {
            Add(new Costumer("Michael", "Anderberg"));
            Add(new Costumer("Julie", "Andrews"));
        }

    }
}
