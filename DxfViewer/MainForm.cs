using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnyCAD.Platform;

namespace DxfViewer
{
    public partial class MainForm : Form
    {
        private AnyCAD.Presentation.RenderWindow3d renderView = null;


        public MainForm()
        {
            InitializeComponent();

            this.renderView = new AnyCAD.Presentation.RenderWindow3d();
            this.renderView.Location = new System.Drawing.Point(0, 27);
            this.renderView.Size = this.Size;
            this.renderView.TabIndex = 1;
            this.Controls.Add(this.renderView);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.renderView.View3d.ShowCoordinateAxis(true);
            renderView.ExecuteCommand("ShadeWithEdgeMode");
            renderView.ExecuteCommand("TopView");
            this.renderView.RequestDraw();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (renderView != null)
                renderView.Size = this.Size;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "DXF (*.dxf)|*.dxf";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                AnyCAD.Exchange.DxfReader reader = new AnyCAD.Exchange.DxfReader();
                if (reader.Read(dlg.FileName, new AnyCAD.Exchange.ShowShapeReaderContext(renderView.SceneManager)))
                    renderView.RequestDraw();

            }

            renderView.View3d.FitAll();
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF (*.pdf)|*.pdf";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                renderView.View3d.Print(dlg.FileName);
            }

        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image File (*.jpg;*.png)|*.jpg;*.png";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                renderView.View3d.GetRenderWindow().CaptureImage(dlg.FileName);
            }
        }

    }
}
