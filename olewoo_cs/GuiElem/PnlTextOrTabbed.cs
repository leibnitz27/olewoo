using System;
using System.Windows.Forms;

namespace Org.Benf.OleWoo.GuiElem
{
    public partial class PnlTextOrTabbed : UserControl
    {
        private Wooctrl _parent;
        private NodeLocator _nl;

        public PnlTextOrTabbed()
        {
            InitializeComponent();
            txtOleText.Visible = true;
            txtOleText.TabParent = this;
            pnlOleText.Visible = false;
            pnlOleText.ImageList = null;
            pnlOleText.InformTabRemoved = TabPageRemoved;
        }
        public Wooctrl ParentCtrl
        {
            set
            {
                _parent = value;
                pnlOleText.ImageList = value.ImageList;
            }
        }
        public NodeLocator NodeLocator
        {
            set
            {
                _nl = value;
            }
            get
            {
                return _nl;
            }
        }
        public void SetCurrentNode(TreeNode n)
        {
            if (pnlOleText.TabCount <= 1)
            {
                txtOleText.TreeNode = n;
            }
            else
            {
                (pnlOleText.SelectedTab.Tag as PnlOleText).TreeNode = n;
            }
        }
        public void RewindOne()
        {
            if (pnlOleText.TabCount <= 1)
            {
                txtOleText.RewindOne();
            }
            else
            {
                (pnlOleText.SelectedTab.Tag as PnlOleText).RewindOne();
            }
        }
        private void AddTabPage(TreeNode n)
        {
            var tp = new TabPage();
            tp.ImageIndex = 0;
            var txtctrl = new PnlOleText( x => tp.Text = x );
            txtctrl.TreeNode = n;
            txtctrl.TabParent = this;
            tp.Tag = txtctrl;
            tp.Controls.Add(txtctrl);
            txtctrl.Dock = DockStyle.Fill;
            pnlOleText.TabPages.Add(tp);
        }
        private void TabPageRemoved()
        {
            switch (pnlOleText.TabPages.Count)
            {
                case 0:
                    txtOleText.Visible = true;
                    pnlOleText.Visible = false;
                    break;
                case 1:
                    txtOleText.TreeNode = (pnlOleText.TabPages[0].Tag as PnlOleText).TreeNode;
                    pnlOleText.TabPages.Clear();
                    txtOleText.Visible = true;
                    pnlOleText.Visible = false;
                    break;
                default: // nothing.
                    break;
            }
        }
        public void AddTab(TreeNode n)
        {
            if (pnlOleText.TabPages.Count == 0)
            {
                AddTabPage(txtOleText.TreeNode);
                AddTabPage(n);
                txtOleText.Visible = false;
                pnlOleText.Visible = true;
            }
            else
            {
                AddTabPage(n);
            }
        }

        public void pnlOleText_TabIndexChanged(object sender, EventArgs e)
        {
            if (pnlOleText.SelectedTab == null) return;
            var pot = pnlOleText.SelectedTab.Tag as PnlOleText;
            if (pot == null) return;
            _parent.SelectTreeNode(pot.TreeNode);
        }
    }

    
}
