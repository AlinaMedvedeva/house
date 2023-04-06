using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace house
{
    public partial class Form1 : Form
    {

        bool pn1, pn2;
        HomeCollection hc;
        ListViewItem dragItem = null;
        TreeNode dragNode = null;
        bool NodeMode = false;
        bool ItemMode = false;
        public Form1()
        {
            InitializeComponent();
            hc = new HomeCollection();
            ListHome.Dock = DockStyle.Fill;
            pn1 = true;
            pn2 = true;
            imageList1.ImageSize = new Size(48,48);
            imageList1.Images.Add(Image.FromFile("G:\\2 курс\\лабы\\house\\house\\bin\\Debug\\moln.png"));
            imageList1.Images.Add(Image.FromFile("G:\\2 курс\\лабы\\house\\house\\bin\\Debug\\electro.png"));
            imageList1.Images.Add(Image.FromFile("G:\\2 курс\\лабы\\house\\house\\bin\\Debug\\icons8-экстерьер-96.png"));
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int tag = Int32.Parse(e.Node.Tag.ToString());
            ListHome.Items.Clear();
            hc.print(tag, ListHome);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1.Hide();
            splitContainer2.SplitterDistance = 0;
            pn1 = false;
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Hide();
            splitContainer2.SplitterDistance = splitContainer2.Height;
            pn2 = false;
        }

        private void домаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pn2 == true)
                splitContainer2.SplitterDistance = splitContainer2.Height / 2;
            splitContainer2.Panel1.Show();
            ListHome.Dock = DockStyle.Fill;
            pn1 = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            splitContainer1.Hide();
        }

        private void рП1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var doc = new XmlDocument();
            doc.Load("sc.xml");
            XmlNode root = doc.DocumentElement;
            TreeNode rootNode = new TreeNode();
            rootNode.Text = root.Attributes.Item(0).Value;
            rootNode.Tag = root.Attributes.Item(2).Value;
            rootNode.ImageIndex = Convert.ToInt32(root.Attributes.Item(1).Value);
            rootNode.SelectedImageIndex = Convert.ToInt32(root.Attributes.Item(1).Value);
            treeView1.Nodes.Add(rootNode);

            if((root.ChildNodes != null) && (root.ChildNodes.Count > 0))
            {
                RecursiveTreeBuilder(rootNode, root);
            }

            treeView1.ExpandAll();
            treeView1.SelectedNode = rootNode;
        }

        private void RecursiveTreeBuilder(TreeNode tn, XmlNode xn)
        {
            foreach(XmlNode c in xn.ChildNodes)
            {
                TreeNode childNode = new TreeNode();
                childNode.Text = c.Attributes.Item(0).Value;
                childNode.ImageIndex = Convert.ToInt32(c.Attributes.Item(1).Value);
                childNode.SelectedImageIndex = Convert.ToInt32(c.Attributes.Item(1).Value);
                childNode.Tag = c.Attributes.Item(2).Value;
                tn.Nodes.Add(childNode);
                if ((c.ChildNodes != null) && (c.ChildNodes.Count > 0))
                    RecursiveTreeBuilder(childNode, c);
            }
        }

        private void ListHome_MouseDown(object sender, MouseEventArgs e)
        {
            dragItem = ListHome.GetItemAt(e.X, e.Y);
        }

        private void ListHome_MouseUp(object sender, MouseEventArgs e)
        {
            CancelDrag();
        }

        private void CancelDrag()
        {
            this.Cursor = Cursors.Default;
            dragItem = null;
            dragNode = null;
            ItemMode = false;
            NodeMode = false;
        }

        private void ListHome_MouseMove(object sender, MouseEventArgs e)
        {
            if ((!ItemMode) && (dragItem != null))
                this.Cursor = AdvancedCursor.Create("HomeCursor.cur");
            ItemMode = true;
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((!NodeMode) && (dragNode != null))
            {
                this.Cursor = AdvancedCursor.Create("MolnCursor.cur");
                NodeMode = true;
            }
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeNode targetNode = treeView1.GetNodeAt(e.X, e.Y);
            if(targetNode != null)
            {
                if(ItemMode)
                {
                    dragItem.Remove();
                    ListHome.Refresh();
                }
                if(NodeMode)
                {
                    targetNode.Nodes.Add(dragNode.Clone() as TreeNode);
                    dragNode.Remove();
                    treeView1.Refresh();
                    treeView1.ExpandAll();
                }
            }
            CancelDrag();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            dragNode = treeView1.GetNodeAt(e.X, e.Y);
        }

        private void panel2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Show();
            if(pn1 == true)
                splitContainer2.SplitterDistance = splitContainer2.Height / 2;
            else
                splitContainer2.SplitterDistance = 0;
            pn2 = true;
        }
    }
}
