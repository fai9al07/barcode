using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Web.Http;

namespace QRCode.Controllers
{
    public class BarcodeController : ApiController
    {
       
        public string Get()
        {
            var whatsAppImageBaseFolder = @"D:\Barcodes";
            var link = string.Format("{0}", "test");
            var returnFile = GenerateBacodeWithText("test");

            //if (barcode == null)
            //    throw new Exception("Error in HLN: Unable to generate barcode");

            //// Save the barcode to the folder specified.
            var uniqueFilename = string.Concat("test.", System.Drawing.Imaging.ImageFormat.Gif);
            
            var filePath = string.Concat(whatsAppImageBaseFolder, @"\", uniqueFilename);
            returnFile.Save(filePath);

            return "Done";
        }

        private static Bitmap GenerateBacode(string _filename)
        {

            Barcode128 code128 = new Barcode128();
            code128.CodeType = Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.StartStopText = true;
            code128.Code = "test";
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White));

            Graphics bmpgraphics = Graphics.FromImage(bm);
            bmpgraphics.Clear(Color.White); // Provide this, else the background will be black by default

            // generate the code128 barcode
            bmpgraphics.DrawImage(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White), new Point(0, 0));

            //generate the text below the barcode image. If you want the placement to be dynamic, calculate the point based on size of the image
            bmpgraphics.DrawString("1234567890", new System.Drawing.Font("Arial", 8, FontStyle.Regular), SystemBrushes.WindowText, new Point(15, 24));

            return bm;
            //bm.Save(_filename, System.Drawing.Imaging.ImageFormat.Gif);


        }

        private static Bitmap GenerateBacodeWithText(string _filename)
        {

            Barcode128 code128 = new Barcode128();
            code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.StartStopText = false;
            code128.Code = "test text on barcode file";

            // Create a blank image 
            System.Drawing.Bitmap bmpimg = new Bitmap(120, 35); // provide width and height based on the barcode image to be generated. harcoded for sample purpose

            Graphics bmpgraphics = Graphics.FromImage(bmpimg);
            bmpgraphics.Clear(Color.White); // Provide this, else the background will be black by default

            // generate the code128 barcode
            bmpgraphics.DrawImage(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White), new Point(0, 0));

            //generate the text below the barcode image. If you want the placement to be dynamic, calculate the point based on size of the image
            bmpgraphics.DrawString("1234567890", new System.Drawing.Font("Arial", 8, FontStyle.Regular), SystemBrushes.WindowText, new Point(15, 24));

            // Save the output stream as gif. You can also save it to external file
            return bmpimg;


        }
    }
}
