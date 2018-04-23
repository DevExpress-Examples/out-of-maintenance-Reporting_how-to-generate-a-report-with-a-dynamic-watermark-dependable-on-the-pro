Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraEditors

Namespace WindowsFormsApplication1
    Partial Public Class XtraReport1
        Inherits DevExpress.XtraReports.UI.XtraReport

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub xrTableCell1_PrintOnPage(ByVal sender As Object, ByVal e As PrintOnPageEventArgs) Handles xrTableCell1.PrintOnPage


        End Sub

        Private Sub xrTableCell1_AfterPrint(ByVal sender As Object, ByVal e As EventArgs) Handles xrTableCell1.AfterPrint

        End Sub

        Private Sub XtraReport1_AfterPrint(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.AfterPrint

            For Each page As Page In Me.PrintingSystem.Pages
                Dim myPageSize As Size = New System.Drawing.Size(CInt(page.PageSize.Width\3),CInt(page.PageSize.Height\3))
                Dim im As Image = New Bitmap(myPageSize.Width, myPageSize.Height)
                Dim bcc As New BarCodeControl()
                bcc.Orientation = DevExpress.XtraPrinting.BarCode.BarCodeOrientation.RotateLeft
                Dim barcodesize As New Size(CInt((myPageSize.Width * 0.1)), CInt((myPageSize.Height * 0.8)))
                bcc.Size = barcodesize
                bcc.AutoModule = True
                bcc.Text = ht(Me.PrintingSystem.Pages.IndexOf(page)).ToString()

                Dim symb As New DevExpress.XtraPrinting.BarCode.Code93ExtendedGenerator()
                bcc.Symbology = symb

                Dim im2 As New Bitmap(barcodesize.Width, barcodesize.Height)
                bcc.DrawToBitmap(im2, New Rectangle(0, 0, barcodesize.Width, barcodesize.Height))
                Using gr As Graphics = Graphics.FromImage(im)
                    gr.DrawImage(im2, New Rectangle(CInt((myPageSize.Width * 0.75)), CInt((myPageSize.Height * 0.1)), barcodesize.Width, barcodesize.Height))
                End Using
                page.AssignWatermark(New DevExpress.XtraPrinting.Drawing.PageWatermark() With {.Image = im, .ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Zoom})
            Next page
        End Sub
        Private ht As New Hashtable()
        Private Sub xrTableCell2_PrintOnPage(ByVal sender As Object, ByVal e As PrintOnPageEventArgs) Handles xrTableCell2.PrintOnPage
            ht.Add(e.PageIndex, DirectCast(sender, XRTableCell).Tag)
        End Sub

    End Class
End Namespace
