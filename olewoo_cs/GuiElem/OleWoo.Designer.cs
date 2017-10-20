namespace Org.Benf.OleWoo.GuiElem
{
    partial class OleWoo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(OleWoo));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerContextHandlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unregisterContextHandlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutOleWooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.imglstTreeNodes = new System.Windows.Forms.ImageList(this.components);
            this.imgListMisc = new System.Windows.Forms.ImageList(this.components);
            this.tcTypeLibs = new TabControlCB();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(57, 6);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerContextHandlerToolStripMenuItem,
            this.unregisterContextHandlerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // registerContextHandlerToolStripMenuItem
            // 
            this.registerContextHandlerToolStripMenuItem.Name = "registerContextHandlerToolStripMenuItem";
            this.registerContextHandlerToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.registerContextHandlerToolStripMenuItem.Text = "&Register context handler";
            this.registerContextHandlerToolStripMenuItem.Click += new System.EventHandler(this.registerContextHandlerToolStripMenuItem_Click);
            // 
            // unregisterContextHandlerToolStripMenuItem
            // 
            this.unregisterContextHandlerToolStripMenuItem.Name = "unregisterContextHandlerToolStripMenuItem";
            this.unregisterContextHandlerToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.unregisterContextHandlerToolStripMenuItem.Text = "&Unregister context handler";
            this.unregisterContextHandlerToolStripMenuItem.Click += new System.EventHandler(this.unregisterContextHandlerToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutOleWooToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutOleWooToolStripMenuItem
            // 
            this.aboutOleWooToolStripMenuItem.Name = "aboutOleWooToolStripMenuItem";
            this.aboutOleWooToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.aboutOleWooToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutOleWooToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutOleWooToolStripMenuItem.Text = "&About OleWoo";
            this.aboutOleWooToolStripMenuItem.Click += new System.EventHandler(this.aboutOleWooToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // imglstTreeNodes
            // 
            this.imglstTreeNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstTreeNodes.ImageStream")));
            this.imglstTreeNodes.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstTreeNodes.Images.SetKeyName(0, "tv_coclass");
            this.imglstTreeNodes.Images.SetKeyName(1, "tv_const");
            this.imglstTreeNodes.Images.SetKeyName(2, "tv_dispinterface");
            this.imglstTreeNodes.Images.SetKeyName(3, "tv_enum");
            this.imglstTreeNodes.Images.SetKeyName(4, "tv_interface");
            this.imglstTreeNodes.Images.SetKeyName(5, "tv_strucmem");
            this.imglstTreeNodes.Images.SetKeyName(6, "tv_struct");
            this.imglstTreeNodes.Images.SetKeyName(7, "tv_typelib");
            this.imglstTreeNodes.Images.SetKeyName(8, "tv_methodlist");
            this.imglstTreeNodes.Images.SetKeyName(9, "tv_method");
            this.imglstTreeNodes.Images.SetKeyName(10, "tv_typedef");
            this.imglstTreeNodes.Images.SetKeyName(11, "tv_module");
            this.imglstTreeNodes.Images.SetKeyName(12, "tv_constlist");
            this.imglstTreeNodes.Images.SetKeyName(13, "tv_selected");
            this.imglstTreeNodes.Images.SetKeyName(14, "tv_properties");
            // 
            // imgListMisc
            // 
            this.imgListMisc.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListMisc.ImageStream")));
            this.imgListMisc.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListMisc.Images.SetKeyName(0, "close_icon.png");
            this.imgListMisc.Images.SetKeyName(1, "search_icon.png");
            // 
            // tcTypeLibs
            // 
            this.tcTypeLibs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTypeLibs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcTypeLibs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcTypeLibs.Location = new System.Drawing.Point(0, 24);
            this.tcTypeLibs.Name = "tcTypeLibs";
            this.tcTypeLibs.SelectedIndex = 0;
            this.tcTypeLibs.Size = new System.Drawing.Size(784, 416);
            this.tcTypeLibs.TabIndex = 2;
            this.tcTypeLibs.TabStop = false;
            this.tcTypeLibs.SelectedIndexChanged += new System.EventHandler(this.tcTypeLibs_SelectedIndexChanged);
            // 
            // OleWoo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.tcTypeLibs);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OleWoo";
            this.Text = "OleWoo";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ImageList imglstTreeNodes;
        private System.Windows.Forms.ImageList imgListMisc;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutOleWooToolStripMenuItem;
        private TabControlCB tcTypeLibs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerContextHandlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unregisterContextHandlerToolStripMenuItem;

    }
}

