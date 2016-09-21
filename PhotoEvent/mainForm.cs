using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PhotoEvent
{
    public partial class mainForm : Form
    {
        classAlbum album;
        int numberPlace = -1, numberEvent = -1, numberPhoto = -1,   //текущие место, событие и фотография
            _place = -1, _event = -1, _photo = -1,                  //надо какие по счету объектом находимся (при вызову контекст. меню)
            number = -1;                                            //1 - работаем с местом, 2 - с событием (переимен. и удаление)
        bool change = false;                                        //если произошло изменение, то true
        String name_place = "", name_event = "";

        public mainForm()
        {
            InitializeComponent();
            album = new classAlbum();
        }

        //добавить место
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                //создаем кнопку
                Button button = new Button();
                button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                button.Size = new System.Drawing.Size(200, 200);
                button.TabIndex = 0;
                button.UseVisualStyleBackColor = true;
                //название места
                int m = 0;
                bool rename = false;
                do
                {
                    rename = false;
                    m++;
                    String str = "Место " + m.ToString();
                    for (int i = 0; i < album.Count; i++)
                        if (album[i].Name == str) rename = true;
                } while (rename);
                button.Text = "Место " + m.ToString();
                //
                button.Name = album.Count.ToString();
                button.ContextMenuStrip = contextMenuStrip1;
                button.Click += new System.EventHandler(this.ClickPlace);
                button.MouseEnter += new System.EventHandler(this.MouseEnterPlace);
                //создаем место
                classPlace place = new classPlace();
                place.Name = button.Text;
                album.Add(place);
                //
                flowLayoutPanel1.Controls.Add(button);
                change = true;
            }
        }

        //клик по месту
        private void ClickPlace(object sender, EventArgs e)
        {
            for (int i = flowLayoutPanel2.Controls.Count - 1; i >= 0; i--)
            {
                Button bb = (Button)flowLayoutPanel2.Controls[i];
                if (bb.Name != "button2") flowLayoutPanel2.Controls.RemoveAt(i);
            }
            //определяем номер места по названию
            Button b = (Button)sender;
            numberPlace = Convert.ToInt32(b.Name);
            //выводим все события
            for (int i = 0; i < album[numberPlace].Count; i++)
            {
                Button button = new Button();
                button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                button.Size = new System.Drawing.Size(200, 200);
                button.TabIndex = 0;
                button.UseVisualStyleBackColor = true;
                button.Text = album[numberPlace][i].Name;
                button.Name = i.ToString();
                button.ContextMenuStrip = contextMenuStrip3;
                button.Click += new System.EventHandler(this.ClickEvent);
                button.MouseEnter += new System.EventHandler(this.MouseEnterEvent);
                flowLayoutPanel2.Controls.Add(button);
            }
            //нажатую ячейку выделяем, остальные наоборот
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                Button but = (Button)flowLayoutPanel1.Controls[i];
                but.FlatAppearance.BorderSize = 1;
            }
            b.FlatAppearance.BorderSize = 3;
            name_place = b.Text;
            tabControl1.SelectedIndex = 1;
        }

        //наведение на место
        private void MouseEnterPlace(object sender, EventArgs e)
        {
            if (textBox1.Enabled == false)
            {
                Button button = (Button)sender;
                _place = Convert.ToInt32(button.Name);
            }
        }

        //переименовать место
        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                if ((_place >= 0) || (_place == -1 && numberPlace >= 0))
                {
                    number = 1;
                    label1.Enabled = true;
                    textBox1.Enabled = true;
                    textBox1.Focus();
                }
        }

        //удалить место
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                int numDelete = -1;
                if (_place >= 0) numDelete = _place;
                else if (_place == -1 && numberPlace >= 0) numDelete = numberPlace;
                if (numDelete >= 0)
                {
                    //удаляем нужную ячейку
                    album.Delete(numDelete);
                    //удаляем кнопку
                    for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                    {
                        Button button = (Button)flowLayoutPanel1.Controls[i];
                        if (button.Name == numDelete.ToString())
                        {
                            flowLayoutPanel1.Controls.RemoveAt(i);
                            break;
                        }
                    }
                    //новые имена button-ов согласно порядковому номеру
                    for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                    {
                        Button button = (Button)flowLayoutPanel1.Controls[i];
                        if (button.Name != "button1")
                            for (int j = 0; j < album.Count; j++)
                                if (button.Text == album[j].Name) button.Name = j.ToString();
                    }
                    change = true;
                }
            }
        }

        //добавить событие
        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                if (numberPlace >= 0)
                {
                    //создаем кнопку
                    Button button = new Button();
                    button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    button.Size = new System.Drawing.Size(200, 200);
                    button.TabIndex = 0;
                    button.UseVisualStyleBackColor = true;
                    //название события
                    int m = 0;
                    bool rename = false;
                    do
                    {
                        rename = false;
                        m++;
                        String str = "Событие " + m.ToString();
                        for (int i = 0; i < album[numberPlace].Count; i++)
                            if (album[numberPlace][i].Name == str) rename = true;
                    } while (rename);
                    button.Text = "Событие " + m.ToString();
                    //
                    button.Name = album[numberPlace].Count.ToString();
                    button.ContextMenuStrip = contextMenuStrip3;
                    button.Click += new System.EventHandler(this.ClickEvent);
                    button.MouseEnter += new System.EventHandler(this.MouseEnterEvent);
                    //создаем событие
                    classEvent _event = new classEvent();
                    _event.Name = button.Text;
                    album[numberPlace].Add(_event);
                    //
                    flowLayoutPanel2.Controls.Add(button);
                    change = true;
                }
        }

        //клик по событию
        private void ClickEvent(object sender, EventArgs e)
        {
            for (int i = flowLayoutPanel3.Controls.Count - 1; i >= 0; i--)
            {
                Button bb = (Button)flowLayoutPanel3.Controls[i];
                if (bb.Name != "button3") flowLayoutPanel3.Controls.RemoveAt(i);
            }
            //определяем номер события по названию
            Button b = (Button)sender;
            numberEvent = Convert.ToInt32(b.Name);
            //выводим все фотографии
            for (int i = 0; i < album[numberPlace][numberEvent].Photo.Count; i++)
            {
                Button button = new Button();
                button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                button.Size = new System.Drawing.Size(200, 200);
                button.TabIndex = 0;
                button.UseVisualStyleBackColor = true;
                button.Text = "";
                button.Name = i.ToString();
                button.ContextMenuStrip = contextMenuStrip2;
                button.Click += new System.EventHandler(this.ClickPhoto);
                button.MouseEnter += new System.EventHandler(this.MouseEnterPhoto);
                flowLayoutPanel3.Controls.Add(button);
                //картинку на кнопку
                button.BackgroundImage = album[numberPlace][numberEvent].Photo[i];
                button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            }
            //нажатую ячейку выделяем, остальные наоборот
            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                Button but = (Button)flowLayoutPanel2.Controls[i];
                but.FlatAppearance.BorderSize = 1;
            }
            b.FlatAppearance.BorderSize = 3;
            name_event = b.Text;
            tabControl1.SelectedIndex = 2;
        }

        //наведение на событие
        private void MouseEnterEvent(object sender, EventArgs e)
        {
            if (textBox1.Enabled == false)
            {
                Button button = (Button)sender;
                _event = Convert.ToInt32(button.Name);
            }
        }

        //переименовать событие
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                if ((_event >= 0) || (_event == -1 && numberEvent >= 0))
                {
                    number = 2;
                    label1.Enabled = true;
                    textBox1.Enabled = true;
                    textBox1.Focus();
                }
        }

        //удалить событие
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                int numDelete = -1;
                if (_event >= 0) numDelete = _event;
                else if (_event == -1 && numberEvent >= 0) numDelete = numberEvent;
                if (numDelete >= 0)
                {
                    //удаляем нужную ячейку
                    album[numberPlace].Delete(numDelete);
                    //удаляем кнопку
                    for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                    {
                        Button button = (Button)flowLayoutPanel2.Controls[i];
                        if (button.Name == numDelete.ToString())
                        {
                            flowLayoutPanel2.Controls.RemoveAt(i);
                            break;
                        }
                    }
                    //новые имена button-ов согласно порядковому номеру
                    for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                    {
                        Button button = (Button)flowLayoutPanel2.Controls[i];
                        if (button.Name != "button2")
                            for (int j = 0; j < album[numberPlace].Count; j++)
                                if (button.Text == album[numberPlace][j].Name) button.Name = j.ToString();
                    }
                    change = true;
                }
            }
        }

        //добавить фотографию
        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
                if (numberPlace >= 0)
                {
                    openFileDialog1.ShowDialog();
                    String path = openFileDialog1.FileName;
                    if (path != "")
                    {
                        //создаем кнопку
                        Button button = new Button();
                        button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        button.Size = new System.Drawing.Size(200, 200);
                        button.TabIndex = 0;
                        button.UseVisualStyleBackColor = true;
                        button.Text = "";
                        button.Name = album[numberPlace][numberEvent].Photo.Count.ToString();
                        button.ContextMenuStrip = contextMenuStrip2;
                        button.Click += new System.EventHandler(this.ClickPhoto);
                        button.MouseEnter += new System.EventHandler(this.MouseEnterPhoto);
                        //создаем фотку
                        Image image = Image.FromFile(path);
                        album[numberPlace][numberEvent].Photo.Add(image, path);
                        //фотка на кнопку
                        button.BackgroundImage = image;
                        button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                        //
                        flowLayoutPanel3.Controls.Add(button);
                        change = true;
                    }
                }
        }

        //клик по фотографии
        private void ClickPhoto(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            numberPhoto = Convert.ToInt32(b.Name);
            if (b.Height == 200) b.Size = new Size(500, 500);
            else b.Size = new Size(200, 200);
        }

        //наведение на фотку
        private void MouseEnterPhoto(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            _photo = Convert.ToInt32(button.Name);
        }

        //удалить фотографию
        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                int numDelete = -1;
                if (_photo >= 0) numDelete = _photo;
                else if (_photo == -1 && numberPhoto >= 0) numDelete = numberPhoto;
                if (numDelete >= 0)
                {
                    //удаляем нужную ячейку
                    album[numberPlace][numberEvent].Photo.Delete(numDelete);
                    //удаляем кнопку
                    for (int i = 0; i < flowLayoutPanel3.Controls.Count; i++)
                    {
                        Button button = (Button)flowLayoutPanel3.Controls[i];
                        if (button.Name == numDelete.ToString())
                        {
                            flowLayoutPanel3.Controls.RemoveAt(i);
                            break;
                        }
                    }
                    change = true;
                }
            }
        }

        //создать новый альбом
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (album != null)
                if (album.Count != 0)
                    if (change)
                    {
                        //предупреждение, если созданный альбом не был сохранен
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show("Сохранить текущий альбом?", "Предупреждение", buttons);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                            сохранитьToolStripMenuItem_Click(sender, e);
                    }
            album = new classAlbum();
            Button button = null;
            //чистим места
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                button = (Button)flowLayoutPanel1.Controls[i];
                if (button.Name == "button1") break;
            }
            if (button != null)
            {
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(button);
            }
            button = null;
            //чистим события
            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                button = (Button)flowLayoutPanel2.Controls[i];
                if (button.Name == "button2") break;
            }
            if (button != null)
            {
                flowLayoutPanel2.Controls.Clear();
                flowLayoutPanel2.Controls.Add(button);
            }
            button = null;
            //чистим фотографии
            for (int i = 0; i < flowLayoutPanel3.Controls.Count; i++)
            {
                button = (Button)flowLayoutPanel3.Controls[i];
                if (button.Name == "button3") break;
            }
            if (button != null)
            {
                flowLayoutPanel3.Controls.Clear();
                flowLayoutPanel3.Controls.Add(button);
            }
            numberPlace = numberEvent = numberPhoto = _place = _event = _photo = number = -1;
            tabControl1.SelectedIndex = 0;
            change = false;
        }

        //открыть альбом из файла
        private void открытьАльбомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (album != null)
                if (album.Count != 0)
                    if (change)
                    {
                        //предупреждение, если созданный альбом не был сохранен
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show("Сохранить текущий альбом?", "Предупреждение", buttons);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                            сохранитьToolStripMenuItem_Click(sender, e);
                        change = false;
                    }
            openFileDialog2.ShowDialog();
            String path = openFileDialog2.FileName;
            if (path != "")
            {
                try
                {
                    //удаляем старый альбом, и создаем новый
                    создатьToolStripMenuItem_Click(sender, e);
                    album = new classAlbum();
                    String[] file = System.IO.File.ReadAllLines(path);
                    int current = 1;
                    //обходим все места
                    int kolPlace = Convert.ToInt32(file[0]);
                    for (int i = 0; i < kolPlace; i++)
                    {
                        classPlace place = new classPlace();
                        place.Name = file[current++];
                        album.Add(place);
                        //обходим все события в месте
                        int kolEvent = Convert.ToInt32(file[current++]);
                        for (int j = 0; j < kolEvent; j++)
                        {
                            classEvent _event = new classEvent();
                            _event.Name = file[current++];
                            album[i].Add(_event);
                            //обходим все фотки
                            int kolPhoto = Convert.ToInt32(file[current++]);
                            classPhoto photo = null;
                            if (kolPhoto != 0) photo = new classPhoto();
                            for (int k = 0; k < kolPhoto; k++)
                            {
                                String pathImage = file[current++];
                                photo.Add(Image.FromFile(pathImage), pathImage);
                            }
                            _event.Photo = photo;
                        }
                    }
                    //создаем кнопки для мест
                    for (int i = 0; i < album.Count; i++)
                    {
                        Button button = new Button();
                        button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        button.Size = new System.Drawing.Size(200, 200);
                        button.TabIndex = 0;
                        button.UseVisualStyleBackColor = true;
                        button.Text = album[i].Name;
                        button.Name = i.ToString();
                        button.ContextMenuStrip = contextMenuStrip1;
                        button.Click += new System.EventHandler(this.ClickPlace);
                        button.MouseEnter += new System.EventHandler(this.MouseEnterPlace);
                        //
                        flowLayoutPanel1.Controls.Add(button);
                    }
                    change = false;
                    tabControl1.SelectedIndex = 0;
                }
                catch { MessageBox.Show("Файл испорчен!"); }
            }
        }

        //сохранить альбом в файл
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (album.Count != 0)
            {
                saveFileDialog1.ShowDialog();
                String file = "", path = saveFileDialog1.FileName;
                if (path != "")
                {
                    //количество мест
                    file += album.Count.ToString() + Environment.NewLine;
                    for (int i = 0; i < album.Count; i++)
                    {
                        //название текущего места
                        file += album[i].Name + Environment.NewLine;
                        //количество событий в месте
                        file += album[i].Count.ToString() + Environment.NewLine;
                        for (int j = 0; j < album[i].Count; j++)
                        {
                            //название текущего события
                            file += album[i][j].Name + Environment.NewLine;
                            //количество фоток в событии
                            file += album[i][j].Photo.Count.ToString() + Environment.NewLine;
                            for (int k = 0; k < album[i][j].Photo.Count; k++)
                                //путь к текущей фотке
                                file += album[i][j].Photo.Path(k).ToString() + Environment.NewLine;
                        }
                    }
                    try
                    {
                        System.IO.File.WriteAllText(path, file);
                        change = false;
                    }
                    catch { MessageBox.Show("Не удалось сохранить файл!"); }
                }
            }
            else MessageBox.Show("Альбом пуст!");
        }

        //выход
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }

        //место
        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e) { _place = -1; button1_Click_1(sender, e); }
        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e) { _place = -1; удалитьToolStripMenuItem_Click(sender, e); }
        private void переименоватьToolStripMenuItem1_Click(object sender, EventArgs e) { _place = -1; переименоватьToolStripMenuItem_Click(sender, e); }

        //событие
        private void создатьToolStripMenuItem2_Click(object sender, EventArgs e) { _event = -1; button2_Click(sender, e); }
        private void удалитьToolStripMenuItem3_Click(object sender, EventArgs e) { _event = -1; toolStripMenuItem2_Click(sender, e); }
        private void переименоватьToolStripMenuItem2_Click(object sender, EventArgs e) { _event = -1; toolStripMenuItem1_Click(sender, e); }

        //фотография
        private void создатьToolStripMenuItem3_Click(object sender, EventArgs e) { _photo = -1; button3_Click(sender, e); }
        private void удалитьToolStripMenuItem4_Click(object sender, EventArgs e) { _photo = -1; удалитьToolStripMenuItem1_Click(sender, e); }

        //о программе
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) { MessageBox.Show("Автор: Полынцев С.Ю. АлтГТУ ПИ-31", "О программе"); }

        //запрет на доступ к вкладкам, когда не выбрано место или событие
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch(tabControl1.SelectedIndex)
            {
                case 0: current_place_event.Text = ""; break;
                case 1: current_place_event.Text = name_place; break;
                case 2: current_place_event.Text = name_place + " -> " + name_event; break;
            }
            if (tabControl1.SelectedIndex == 0)
            {
                numberPlace = numberEvent = numberPhoto = -1;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                if (numberPlace == -1) tabControl1.SelectedIndex = 0;
                numberEvent = numberPhoto = -1;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                if (numberEvent == -1) tabControl1.SelectedIndex = 1;
                if (numberPlace == -1) tabControl1.SelectedIndex = 0;
                numberPhoto = -1;
            }
        }

        //завершение переименования
        private void textBox1_Leave(object sender, EventArgs e)
        {
            String newName = textBox1.Text;
            label1.Enabled = false;
            textBox1.Enabled = false;
            if (newName != "")
            {
                textBox1.Text = "";
                //замена имени
                switch (number)
                {
                    case 1:
                        if (_place != -1) album[_place].Name = newName;
                        else if (numberPlace != -1) album[numberPlace].Name = newName;
                        //обновление имени у кнопок
                        for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                        {
                            Button button = (Button)flowLayoutPanel1.Controls[i];
                            if (button.Name != "button1")
                                button.Text = album[Convert.ToInt32(button.Name)].Name;
                        }
                        break;
                    case 2:
                        if (_event != -1) album[numberPlace][_event].Name = newName;
                        else if (numberEvent != -1) album[numberPlace][numberEvent].Name = newName;
                        //обновление имени у кнопок
                        for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                        {
                            Button button = (Button)flowLayoutPanel2.Controls[i];
                            if (button.Name != "button2")
                                button.Text = album[numberPlace][Convert.ToInt32(button.Name)].Name;
                        }
                        break;
                }
                change = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox1_Leave(sender, null);
            if (e.KeyCode == Keys.Escape)
            {
                textBox1.Text = "";
                textBox1_Leave(sender, null);
            }
        }

        //закрытие программы
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (album != null)
                if (album.Count != 0)
                    if (change)
                    {
                        //предупреждение, если созданный альбом не был сохранен
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show("Сохранить текущий альбом?", "Предупреждение", buttons);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                            сохранитьToolStripMenuItem_Click(sender, e);
                    }
        }

        //поиск
        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (album != null)
                if (album.Count != 0)
                {
                    Search form = new Search(album);
                    form.ShowDialog();
                }
        }
    }
}