using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptManager.Models
{
    class ComboBoxItem
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ComboBoxItem(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
