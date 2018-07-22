using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Org.Benf.OleWoo.GuiMisc;
using Org.Benf.OleWoo.Typelib;

namespace Org.Benf.OleWoo
{
    public sealed partial class Wooctrl : UserControl
    {
        private enum SortType
        {
            SortedNumerically,
            SortedAlphaUp,
            SortedAlphaDown,
            SortedMax
        }

        private readonly NodeLocator _nl;
        private SortType _sort;

        public Wooctrl(ImageList imglstTreeNodes, ImageList imglstMisc, OWTypeLib tlib)
        {
            _nl = new NodeLocator();
            ImageList = imglstMisc;
            _sort = SortType.SortedNumerically;

            InitializeComponent();
            txtOleDescrPlain.ParentCtrl = this;
            tvLibDisp.ImageList = imglstTreeNodes;
            Dock = DockStyle.Fill;

            tvLibDisp.Nodes.Add(GenNodeTree(tlib, _nl));
            txtOleDescrPlain.NodeLocator = _nl;
            tvLibDisp.Nodes[0].Expand();
        }

        public ImageList ImageList { get; }

        /*
         * Note that this generates redundant tree nodes, i.e. many definitions of (eg) IUnknown
         * this is because Trees don't have support for child sharing. (how would the parent property work? :)
         */
        private TreeNode GenNodeTree(ITlibNode tln, NodeLocator nl)
        {
            var tn = new TreeNode(tln.Name, tln.ImageIndex,
                (int) ITlibNode.ImageIndices.idx_selected,
                tln.Children.ConvertAll(x => GenNodeTree(x, nl)).ToArray()) {Tag = tln};
            nl.Add(tn);
            return tn;
        }

        private void tvLibDisp_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtOleDescrPlain.SetCurrentNode(e.Node);
        }

        private void ClearMatches()
        {
            pnlMatchesList.Visible = false;
            lstNodeMatches.Items.Clear();
        }
        /*
         * Search through the registered names for the tree nodes.
         * 
         * When we hit one, select that node.
         */
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearch.Text;
            if (text == "")
            {
                ClearMatches();
            }
            else
            {
                try
                {
                    var tn = _nl.FindMatches(text);
                    lstNodeMatches.Items.Clear();
                    if (tn != null && tn.Count > 0)
                    {
                        tvLibDisp.ActivateNode(tn.First().TreeNode);
                        lstNodeMatches.Items.AddRange(tn.ToArray<object>());
                    }
                    pnlMatchesList.Visible = true;
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
            }
        }

        private void lstNodeMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(lstNodeMatches.SelectedItem is NamedNode nn)) return;
            tvLibDisp.ActivateNode(nn.TreeNode);
        }

        private void btnHideMatches_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        private void btnAddNodeTabl_Click(object sender, EventArgs e)
        {
            txtOleDescrPlain.AddTab(tvLibDisp.SelectedNode);
        }
        public void SelectTreeNode(TreeNode tn)
        {
            tvLibDisp.ActivateNode(tn);
        }

        private delegate int CompDelg(ITlibNode x, ITlibNode y);

        private class OleNodeComparer : IComparer<TreeNode>
        {
            private readonly CompDelg _cd;
            public OleNodeComparer(SortType t)
            {
                switch (t)
                {
                    default: // shouldn't happen...
                        _cd = CompareNum;
                        break;
                    case SortType.SortedAlphaUp:
                        _cd = CompareAlphaUp;
                        break;
                    case SortType.SortedAlphaDown:
                        _cd = CompareAlphaDown;
                        break;
                }
            }
            public int Compare(TreeNode x, TreeNode y)
            {
                if (x == null || y == null)
                {
                    if (x == null && y == null) return 0;
                    if (x == null) return -1;
                    return 1;
                }
                return _cd(x.Tag as ITlibNode, y.Tag as ITlibNode);
            }

            private static int CompareNum(ITlibNode x, ITlibNode y)
            {
                return x.Idx.CompareTo(y.Idx);
            }

            private static int CompareAlphaUp(ITlibNode x, ITlibNode y)
            {
                return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }

            private static int CompareAlphaDown(ITlibNode x, ITlibNode y)
            {
                return string.Compare(y.Name, x.Name, StringComparison.Ordinal);
            }
        }

        private void btnSortAlpha_Click(object sender, EventArgs e)
        {
            using (new UpdateSuspender(tvLibDisp))
            {
                _sort++;
                if (_sort == SortType.SortedMax) _sort = SortType.SortedNumerically;
                var nlst = new List<TreeNode>();
                var root = tvLibDisp.Nodes[0];
                var enm = root.Nodes.GetEnumerator();
                while (enm.MoveNext())
                {
                    nlst.Add(enm.Current as TreeNode);
                }
                nlst.Sort(new OleNodeComparer(_sort));
                root.Nodes.Clear();
                root.Nodes.AddRange(nlst.ToArray());
            }
        }

        private void btnRewind_Click(object sender, EventArgs e)
        {
            txtOleDescrPlain.RewindOne();
        }

    }

    internal static class Xtras
    {
        public static void ActivateNode(this TreeView tv, TreeNode tn)
        {
            if (tn != null)
            {
                tv.SelectedNode = tn;
                tn.EnsureVisible();
            }
        }
    }

    public class NamedNode
    {
        private readonly ITlibNode _tln;

        public NamedNode(TreeNode tn)
        {
            TreeNode = tn;
            _tln = tn.Tag as ITlibNode;
        }
        public string Name => _tln.ShortName;
        public string ObjectName => _tln.ObjectName;
        public TreeNode TreeNode { get; }

        public override string ToString()
        {
            return _tln.ShortName;
        }
    }

    public class NodeLocator
    {
        private readonly List<NamedNode> _nodes;
        private readonly Dictionary<string, NamedNode> _linkmap;

        public NodeLocator()
        {
            _nodes = new List<NamedNode>();
            _linkmap = new Dictionary<string, NamedNode>();
        }

        public void Add(TreeNode tn)
        {
            var tli = tn.Tag as ITlibNode;
            var name = tli?.ShortName;
            if (name == null) return;
            var nn = new NamedNode(tn);
            _nodes.Add(nn);
            var oname = tli.ObjectName;
            if (!String.IsNullOrEmpty(oname) && !_linkmap.ContainsKey(oname))
            {
                _linkmap[oname] = nn;
            }
        }

        // O(N).  FIX!
        public List<NamedNode> FindMatches(string text)
        {
            var re = new Regex("^.*" + text, RegexOptions.IgnoreCase);
            return _nodes.FindAll(x => re.IsMatch(x.Name));
        }

        public NamedNode FindLinkMatch(string text)
        {
            return _linkmap.TryGetValue(text, out var res) ? res : null;
        }
    }
}
