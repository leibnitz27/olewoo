using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Org.Benf.OleWoo.Typelib;

namespace Org.Benf.OleWoo.GuiElem
{
    public partial class OleWoo : Form
    {
        private MRUList _mrufiles;

        public OleWoo()
        {
            InitializeComponent();
            tcTypeLibs.ImageList = imgListMisc;
            _mrufiles = new MRUList(@"Software\benf.org\olewoo\MRU");
            var args = Environment.GetCommandLineArgs().ToList();
            args.RemoveAt(0);
            foreach (var arg in args)
            {
                OpenFile(System.IO.Path.GetFullPath(arg));
            }
        }

        private void OpenFile(string filePath)
        {
            try
            {
                string lower = filePath.ToLower();
                string extension = Path.GetExtension(lower);
                string[] typeLibs = NativeMethods.EnumerateTypeLibs(lower);

                if (typeLibs.Length == 0)
                    typeLibs = new string[] { filePath };
                else 
                    typeLibs = typeLibs.Select(name => filePath + "\\" + name).ToArray();
                
                foreach (string typeLibPath in typeLibs)
                {
                    var typeLib = new OWTypeLib(typeLibPath);
                    var tabPage = new TabPage(typeLib.ShortName) { ImageIndex = 0, ToolTipText = typeLibPath };
                    var wooCtrl = new Wooctrl(imglstTreeNodes, imgListMisc, typeLib);
                    tabPage.Controls.Add(wooCtrl);
                    tabPage.Tag = typeLib;
                    wooCtrl.Dock = DockStyle.Fill;
                    tcTypeLibs.TabPages.Add(tabPage);
                    _mrufiles.AddItem(typeLibPath);
                }
                _mrufiles.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK);
            }
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter =
                    "Dll files (*.dll)|*.dll|Type libraries (*.tlb)|*.tlb|Executables (*.exe)|*.exe|ActiveX controls (*.ocx)|*.ocx|Object Libraries (*.olb)|*.olb|All files (*.*)|*.*",
                CheckFileExists = true
            };
            switch (ofd.ShowDialog(this))
            {
                case DialogResult.OK:
                    OpenFile(ofd.FileName);
                    break;
                default:
                    break;
            }
        }
              


        private void aboutOleWooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ab = new AboutBox();
            ab.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openMRUItem_Click(object sender, EventArgs e)
        {
            var mni = sender as ToolStripMenuItem;
            if (mni == null) return;
            OpenFile(mni.Tag as string);            
        }

        private void clearMRUItem_Click(object sender, EventArgs e)
        {
            _mrufiles.Clear();
            _mrufiles.Flush();
        }

        private delegate void VoidDelg();

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Dynamically populate the file item with the MRU */
            fileToolStripMenuItem.DropDownItems.Clear();

            var tsis = new List<ToolStripItem>();

            // 
            // openToolStripMenuItem
            // 
            var tmiOpen = new ToolStripMenuItem
            {
                Name = "openToolStripMenuItem",
                ShortcutKeys = ((Keys)((Keys.Control | Keys.O))),
                Size = new System.Drawing.Size(208, 22),
                Text = "&Open Typelibrary"
            };
            tmiOpen.Click += new EventHandler(openToolStripMenuItem_Click);
            tsis.Add(tmiOpen);

            void addSep()
            {
                var tmiSep = new ToolStripSeparator
                {
                    Name = "toolStripMenuItem1",
                    Size = new System.Drawing.Size(205, 6)
                };
                tsis.Add(tmiSep);
            }

            addSep();

            var mru = _mrufiles.Items;
            if (mru.Length > 0)
            {
                var idx = 1;
                foreach (var mrui in mru)
                {
                    var tmiMru = new ToolStripMenuItem
                    {
                        Tag = mrui,
                        Size = new System.Drawing.Size(208, 22)
                    };
                    var label = mrui;
                    if (label.Length > 35) label = label.Substring(0,10) + "..."+ label.Substring(label.Length - 20);
                    tmiMru.Text = "&" + (idx++) + " " + label;
                    tmiMru.Click += new EventHandler(openMRUItem_Click);
                    tsis.Add(tmiMru);
                }
                addSep();
                {
                    var tmiMru = new ToolStripMenuItem
                    {
                        Size = new System.Drawing.Size(208, 22),
                        Text = "&Clear Recent items list."
                    };
                    tmiMru.Click += new EventHandler(clearMRUItem_Click);
                    tsis.Add(tmiMru);
                }
                addSep();
            }

            var tmiExit = new ToolStripMenuItem
            {
                Name = "exitToolStripMenuItem",
                Size = new System.Drawing.Size(208, 22),
                Text = "E&xit"
            };
            tmiExit.Click += new EventHandler(exitToolStripMenuItem_Click);
            tsis.Add(tmiExit);

            fileToolStripMenuItem.DropDownItems.AddRange(tsis.ToArray());
        }

        private void registerContextHandlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuCommand = $"\"{Application.ExecutablePath}\" \"%L\"";
            FileShellExtension.Register("dllfile", "OleWoo Context Menu",
                                        "Open with OleWoo", menuCommand);
        }

        private void unregisterContextHandlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileShellExtension.Unregister("dllfile", "OleWoo Context Menu");
        }

        private void tcTypeLibs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
