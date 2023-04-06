using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace house
{
    class HomeCollection
    {
        List<Home> lst = new List<Home>();
        public HomeCollection()
        {
            FileStream fin = new FileStream("G:\\home.txt", FileMode.Open);
            StreamReader rd = new StreamReader(fin, Encoding.UTF8);
            string line;
            string [] split;
            while((line = rd.ReadLine()) != null)
            {
                split = line.Split("+");
                Home h = new Home(Int32.Parse(split[0]), Int32.Parse(split[1]),
                    split[2], Int32.Parse(split[3]), Int32.Parse(split[4]), Int32.Parse(split[5]));
                lst.Add(h);
            }
            fin.Close();
        }
        public void print(int tag, ListView lb)
        {
            ListViewItem item;
            foreach(Home h in lst)
            {
                if (h.connect == tag)
                {
                    item = new ListViewItem(h.address);
                    item.ImageIndex = 2;
                    item.Tag = h.id;
                    lb.Items.Add(item);
                }
            }
        }
    }
}
