using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEvent
{
    public partial class Search : Form
    {
        classAlbum album = null;
        public Search(Object album)
        {
            InitializeComponent();
            this.album = (classAlbum)album;
        }

        private void Search_Load(object sender, EventArgs e)
        {
            //список мест
            for(int i=0; i<album.Count; i++)
            {
                String namePlace = album[i].Name;
                listBox1.Items.Add(namePlace);
            }
            //список событий
            for (int i = 0; i < album.Count; i++)
            {
                String namePlace = album[i].Name;
                for(int j=0; j<album[i].Count; j++)
                {
                    String nameEvent = album[i][j].Name;
                    listBox2.Items.Add(nameEvent + " (" + namePlace + ")");
                }
            }
            //показать все фотки
            for (int i = 0; i < album.Count; i++)
                for (int j = 0; j < album[i].Count; j++)
                    for (int k = 0; k < album[i][j].Photo.Count; k++)
                    {
                        PictureBox pBox = new PictureBox();
                        pBox.BackgroundImage = Image.FromFile(album[i][j].Photo.Path(k));
                        pBox.Size = new Size(200, 200);
                        pBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                        flowLayoutPanel1.Controls.Add(pBox);
                        pBox.Size = new Size(200, 200);
                    }
        }

        //искать
        private void button1_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            List<String> list = new List<String>();
            //среди мест
            if (textBox1.Text != "")
                for (int i = 0; i < album.Count; i++)
                    if (album[i].Name.Contains(textBox1.Text)) list.Add(album[i].Name);
            //среди событий
            if (textBox2.Text != "")
                for (int i = 0; i < album.Count; i++)
                    for (int j = 0; j < album[i].Count; j++)
                        if (album[i][j].Name.Contains(textBox2.Text)) list.Add(album[i][j].Name);
            //и там и там
            if (textBox3.Text != "")
                for (int i = 0; i < album.Count; i++)
                {
                    if (album[i].Name.Contains(textBox3.Text)) list.Add(album[i].Name);
                    for (int j = 0; j < album[i].Count; j++)
                        if (album[i][j].Name.Contains(textBox3.Text)) list.Add(album[i][j].Name);
                }
            foreach (String str in list)
            {
                listBox3.Items.Add(str);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) { textBox2.Text = textBox3.Text = ""; }
        private void textBox2_KeyDown(object sender, KeyEventArgs e) { textBox1.Text = textBox3.Text = ""; }
        private void textBox3_KeyDown(object sender, KeyEventArgs e) { textBox1.Text = textBox2.Text = ""; }
    }
}
