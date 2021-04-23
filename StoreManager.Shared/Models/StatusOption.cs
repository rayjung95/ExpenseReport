using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Shared.Models
{
    public class StatusOption
    {
        public string value { get; set; }
        public string displayName { get; set; }

        public StatusOption(string value, string displayName)
        {
            this.value = value;
            this.displayName = displayName;
        }
    }
}
