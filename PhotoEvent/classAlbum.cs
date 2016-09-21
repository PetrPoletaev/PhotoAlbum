using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEvent
{
    class classAlbum
    {
        List<classPlace> list;

        public classAlbum() { this.list = new List<classPlace>(); }

        public classPlace this[int index] { get { return this.list[index]; } }

        public int Count { get { return this.list.Count; } }

        public void Add(classPlace place) { this.list.Add(place); }

        public void Delete(int index) { this.list.RemoveAt(index); }
    }
}