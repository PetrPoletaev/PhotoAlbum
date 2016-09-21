using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEvent
{
    class classEvent
    {
        private String nameEvent;
        private classPhoto photo;

        public classEvent() { Photo = new classPhoto(); }

        public String Name
        {
            set { this.nameEvent = value; }
            get { return this.nameEvent; }
        }

        public classPhoto Photo
        {
            set { this.photo = value; }
            get { return this.photo; }
        }
    }
}
