using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraEditors;

namespace WindowsFormsApplication1
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();
        }
        private void xrTableCell1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
            
        }

        private void xrTableCell1_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void XtraReport1_AfterPrint(object sender, EventArgs e)
        {

            foreach (Page page in this.PrintingSystem.Pages)
            {
                Size myPageSize = new System.Drawing.Size((int)(page.PageSize.Width/3),(int)(page.PageSize.Height/3));
                Image im = new Bitmap(myPageSize.Width, myPageSize.Height);
                BarCodeControl bcc = new BarCodeControl();
                bcc.Orientation = DevExpress.XtraPrinting.BarCode.BarCodeOrientation.RotateLeft;
                Size barcodesize = new Size((int)(myPageSize.Width * 0.1), (int)(myPageSize.Height * 0.8));
                bcc.Size = barcodesize;
                bcc.AutoModule = true;
                bcc.Text = ht[this.PrintingSystem.Pages.IndexOf(page)].ToString();

                DevExpress.XtraPrinting.BarCode.Code93ExtendedGenerator symb = new DevExpress.XtraPrinting.BarCode.Code93ExtendedGenerator();
                bcc.Symbology = symb;
                
                Bitmap im2 = new Bitmap(barcodesize.Width, barcodesize.Height);
                bcc.DrawToBitmap(im2, new Rectangle(0, 0, barcodesize.Width, barcodesize.Height));
                using (Graphics gr = Graphics.FromImage(im))
                {
                    gr.DrawImage(im2, new Rectangle((int)(myPageSize.Width * 0.75), (int)(myPageSize.Height * 0.1), barcodesize.Width, barcodesize.Height));
                }
                page.AssignWatermark(new DevExpress.XtraPrinting.Drawing.PageWatermark() { Image = im,ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Zoom});
            }
        }
        Hashtable ht = new Hashtable();
        private void xrTableCell2_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            ht.Add(e.PageIndex, ((XRTableCell)sender).Tag);
        }

    }
}
