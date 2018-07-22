using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Org.Benf.OleWoo.Typelib;

namespace Org.Benf.OleWoo.GuiElem
{
    public partial class TabControlCB : TabControl
    {
        public TabControlCB()
        {
            InitializeComponent();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Bounds != RectangleF.Empty)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                for (var nIndex = 0; nIndex < TabCount; nIndex++)
                {
                    var tabTextArea = (RectangleF)GetTabRect(nIndex);
                    if (nIndex == SelectedIndex)
                    {
                        var icon = new Rectangle((int)tabTextArea.X, (int)tabTextArea.Y, (int)tabTextArea.Width, (int)tabTextArea.Height);
                        e.Graphics.DrawImageUnscaled(ImageList.Images[0], new Point(icon.Left + icon.Width - 16, icon.Top + icon.Height - 16));
                    }
                    else
                    {
                          var path = new GraphicsPath();
                          path.AddRectangle(tabTextArea);
                          using (var brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                          {
                              var colorBlend = new ColorBlend(3)
                              {
                                  Colors = new Color[]
                                  {
                                      SystemColors.ControlLightLight,
                                      Color.FromArgb(255, SystemColors.ControlLight), SystemColors.ControlDark,
                                      SystemColors.ControlLightLight
                                  },
                                  Positions = new float[] {0f, .4f, 0.5f, 1f}
                              };

                              brush.InterpolationColors = colorBlend;

                              e.Graphics.FillPath(brush, path);
                              using (var pen = new Pen(SystemColors.ActiveBorder))
                              {
                                  e.Graphics.DrawPath(pen, path);
                              }
                          }
                          path.Dispose();
                    }
                    var str = TabPages[nIndex].Text;
                    var stringFormat = new StringFormat {Alignment = StringAlignment.Near};
                    tabTextArea.Offset(2, 2);
                    e.Graphics.DrawString(str, Font, new SolidBrush(TabPages[nIndex].ForeColor), tabTextArea, stringFormat);                    
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
                var tabTextArea = (RectangleF)GetTabRect(SelectedIndex);
                var icon = new Rectangle((int)tabTextArea.X, (int)tabTextArea.Y, (int)tabTextArea.Width, (int)tabTextArea.Height);
                tabTextArea =
                    new RectangleF(tabTextArea.X + tabTextArea.Width - 16, 4, 16,16);
                var pt = new Point(e.X, e.Y);
                if (tabTextArea.Contains(pt))
                {
                    // IDispose not appropriate.
                    if (SelectedTab.Tag is IClearUp icu) icu.ClearUp();
                    TabPages.Remove(SelectedTab);
                    _itr?.Invoke();
                }
        }

        public InformTabRemoved InformTabRemoved
        {
            set => _itr = value;
        }

        private InformTabRemoved _itr;
    }

    public delegate void InformTabRemoved();
}
