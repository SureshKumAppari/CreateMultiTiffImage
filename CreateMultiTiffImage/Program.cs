using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CreateMultiTiffImage
{
    class Program
    {
        static void Main(string[] args)
        {
            // Default Image for Tiff image
            Image bmp = Image.FromFile(@"C:\Users\suresh.appari\source\repos\CreateMultiTiffImage\CreateMultiTiffImage\Images\git.png");
            // 2nd and 3rd image that will add to the original Tiff image
            Image img1 = Image.FromFile(@"C:\Users\suresh.appari\source\repos\CreateMultiTiffImage\CreateMultiTiffImage\Images\leetcode.png");

            // Assign final TIFF image to BMP
            Bitmap bitmap = (Bitmap)bmp;
            //Select image encoder
            System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.SaveFlag;

            // Select ImageCodecInfo for tiff file format
            ImageCodecInfo info = null;
            info = (from ie in ImageCodecInfo.GetImageEncoders()
                    where ie.MimeType == "image/tiff"
                    select ie).FirstOrDefault();
            EncoderParameters encoderparams = new EncoderParameters(1);

            //make this file multi frame to be able to add multiple images
            encoderparams.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame);

            //Save the bitmap
            bitmap.Save(@"C:\Users\suresh.appari\source\repos\CreateMultiTiffImage\CreateMultiTiffImage\Images\MultiFrameImage.tiff", info, encoderparams);
            encoderparams.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage);

            //add another images and Repeat this process to Add Multiple Imagess
            bitmap.SaveAdd(img1, encoderparams);

            // Close the file
            if (encoderparams.Param[0].NumberOfValues > 0)
                encoderparams.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush);
            bitmap.SaveAdd(encoderparams);

            Console.ReadLine();
        }

    }
}