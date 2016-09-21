using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEvent
{
    class classPlace
    {
        private String name;
        private List<classEvent> listEvent;
        
        public classPlace()
        {
            listEvent = new List<classEvent>();
        }

        public int Count { get { return this.listEvent.Count; } }
        public classEvent this[int index] { get { return this.listEvent[index]; } }
        public void Add(classEvent _event) { this.listEvent.Add(_event); }
        public void Delete(int index) { this.listEvent.RemoveAt(index); }
        public String Name
        {
            set { this.name = value; }
            get { return this.name; }
        }
    }
}
