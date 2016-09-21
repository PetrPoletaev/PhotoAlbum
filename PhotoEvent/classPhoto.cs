using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoEvent
{
    class classPhoto
    {
        private List<Image> listPhoto;
        private List<String> listPath;

        public classPhoto()
        {
            listPhoto = new List<Image>();
            listPath = new List<String>();
        }

        public int Count { get { return this.listPhoto.Count; } }

        public void Add(Image photo, String path)
        {
            this.listPhoto.Add(photo);
            this.listPath.Add(path);
        }

        public Image this[int index] { get { return this.listPhoto[index]; } }

        public String Path(int index) { return this.listPath[index]; }

        public void Delete(int index)
        {
            this.listPhoto.RemoveAt(index);
            this.listPath.RemoveAt(index);
        }
    }
}
