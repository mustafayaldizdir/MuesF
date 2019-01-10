using MuesF.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MuesF.Base
{
    public class HelperMethods
    {
       
        public static string UrlDuzenle(string gelen)
        {
            string url = gelen.ToLower().Trim().Replace(" ", "-").Replace("ğ", "g").Replace("ı", "i").Replace("ü", "u").Replace("ş", "s").Replace("ç", "c").Replace("ö", "o").Replace("'", "-").Replace("?", "").Replace(";", "").Replace("*", "").Replace("+", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\\", "").Replace("<", "").Replace(">", "").Replace("^", "").Replace("&", "").Replace("%", "").Replace("=", "").Replace("$", "").Replace("€", "").Replace("æ", "").Replace("ß", "").Replace(",", "").Replace(".", "").Replace(";", "").Replace("@", "").Replace("½", "").Replace("!", "").Replace("|", "").Replace("_", "").Replace("#", "").Replace("'", "");
            return url;
        }

        public static string CustomReplace(string text)
        {
            char[] türkcekarakterler = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] ingilizce = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };//karakterler sırayla ingilizce karakter karşılıklarıyla yazıldı
            for (int i = 0; i < türkcekarakterler.Length; i++)
            {

                text = text.Replace(türkcekarakterler[i], ingilizce[i]);

            }
            return text;
        }

        //public static bool CropImage(int Width, int Height, Bitmap sourceFilePath, string saveFilePath,bool isBasePhoto)
        //{
        //    try
        //    {

        //        // variable for percentage resize 
        //        float percentageResize = 0;
        //        float percentageResizeW = 0;
        //        float percentageResizeH = 0;

        //        // variables for the dimension of source and cropped image 
        //        int sourceX = 0;
        //        int sourceY = 0;
        //        int destX = 0;
        //        int destY = 0;

        //        // Create a bitmap object file from source file 
        //        Bitmap sourceImage = new Bitmap(sourceFilePath);

        //        // Set the source dimension to the variables 
        //        int sourceWidth = sourceImage.Width;
        //        int sourceHeight = sourceImage.Height;

        //        // Calculate the percentage resize 
        //        percentageResizeW = ((float)Width / (float)sourceWidth);
        //        percentageResizeH = ((float)Height / (float)sourceHeight);

        //        // Checking the resize percentage 
        //        if (percentageResizeH < percentageResizeW)
        //        {
        //            percentageResize = percentageResizeW;
        //            destY = System.Convert.ToInt16((Height - (sourceHeight * percentageResize)) / 2);
        //        }
        //        else
        //        {
        //            percentageResize = percentageResizeH;
        //            destX = System.Convert.ToInt16((Width - (sourceWidth * percentageResize)) / 2);
        //        }

        //        // Set the new cropped percentage image
        //        int destWidth = (int)Math.Round(sourceWidth * percentageResize);
        //        int destHeight = (int)Math.Round(sourceHeight * percentageResize);

        //        // Create the image object 
        //        using (Bitmap objBitmap = new Bitmap(Width, Height))
        //        {
        //            objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
        //            using (Graphics objGraphics = Graphics.FromImage(objBitmap))
        //            {
        //                // Set the graphic format for better result cropping 
        //                objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //                objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //                objGraphics.DrawImage(sourceImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

        //                // Save the file path, note we use png format to support png file 
        //                //objBitmap.Save(saveFilePath, ImageFormat.Png);
        //                if (sourceFilePath.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
        //                {
        //                    #region Kalite Ayarı
        //                    // Get a bitmap.
        //                    Bitmap bmp1 = objBitmap;
        //                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

        //                    // Create an Encoder object based on the GUID
        //                    // for the Quality parameter category.
        //                    System.Drawing.Imaging.Encoder myEncoder =
        //                        System.Drawing.Imaging.Encoder.Quality;

        //                    // Create an EncoderParameters object.
        //                    // An EncoderParameters object has an array of EncoderParameter
        //                    // objects. In this case, there is only one
        //                    // EncoderParameter object in the array.
        //                    EncoderParameters myEncoderParameters = new EncoderParameters(1);

        //                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
        //                    myEncoderParameters.Param[0] = myEncoderParameter;
        //                    bmp1.Save(saveFilePath, jpgEncoder, myEncoderParameters);

        //                    #endregion
        //                }
        //                else if (sourceFilePath.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
        //                {
        //                    if (isBasePhoto)
        //                    {
        //                        if (objBitmap.Width > objBitmap.Height)
        //                        {
        //                            if (objBitmap.Width > 700)
        //                            {
        //                                Bitmap resim = PictureCrop.CustomResizeImage(objBitmap, 700);
        //                                resim.Save(saveFilePath, ImageFormat.Png);
        //                            }
        //                            else
        //                            {
        //                                objBitmap.Save(saveFilePath);
        //                            }

        //                        }
        //                        else
        //                        {
        //                            if (objBitmap.Height > 700)
        //                            {
        //                                Bitmap resim = PictureCrop.CustomResizeImageH(objBitmap, 700);
        //                                resim.Save(saveFilePath, ImageFormat.Png);
        //                            }
        //                            else
        //                            {
        //                                objBitmap.Save(saveFilePath);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        objBitmap.Save(saveFilePath);
        //                    }
                           
        //                }
        //                else
        //                {
        //                    objBitmap.Save(saveFilePath);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        

        //public static Bitmap CropImageBitmap(int Width, int Height, Bitmap sourceFilePath, string saveFilePath)
        //{
        //    try
        //    {
        //        // variable for percentage resize 
        //        float percentageResize = 0;
        //        float percentageResizeW = 0;
        //        float percentageResizeH = 0;

        //        // variables for the dimension of source and cropped image 
        //        int sourceX = 0;
        //        int sourceY = 0;
        //        int destX = 0;
        //        int destY = 0;

        //        // Create a bitmap object file from source file 
        //        Bitmap sourceImage = new Bitmap(sourceFilePath);

        //        // Set the source dimension to the variables 
        //        int sourceWidth = sourceImage.Width;
        //        int sourceHeight = sourceImage.Height;

        //        // Calculate the percentage resize 
        //        percentageResizeW = ((float)Width / (float)sourceWidth);
        //        percentageResizeH = ((float)Height / (float)sourceHeight);

        //        // Checking the resize percentage 
        //        if (percentageResizeH < percentageResizeW)
        //        {
        //            percentageResize = percentageResizeW;
        //            destY = System.Convert.ToInt16((Height - (sourceHeight * percentageResize)) / 2);
        //        }
        //        else
        //        {
        //            percentageResize = percentageResizeH;
        //            destX = System.Convert.ToInt16((Width - (sourceWidth * percentageResize)) / 2);
        //        }

        //        // Set the new cropped percentage image
        //        int destWidth = (int)Math.Round(sourceWidth * percentageResize);
        //        int destHeight = (int)Math.Round(sourceHeight * percentageResize);
        //        // Create the image object 
        //        Bitmap objBitmap = new Bitmap(Width, Height);

        //        objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
        //        Graphics objGraphics = Graphics.FromImage(objBitmap);

        //        // Set the graphic format for better result cropping 
        //        objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //        objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        objGraphics.DrawImage(sourceImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

        //        // Save the file path, note we use png format to support png file 

        //        return objBitmap;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //private static ImageCodecInfo GetEncoder(ImageFormat format)
        //{

        //    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        //    foreach (ImageCodecInfo codec in codecs)
        //    {
        //        if (codec.FormatID == format.Guid)
        //        {
        //            return codec;
        //        }
        //    }
        //    return null;
        //}

        //public static void PictureLowQuality(Bitmap sourceFilePath, string saveFilePath, string saveFilePath2, string saveFilePath3, string saveFilePath4)
        //{
        //    // Get a bitmap.
        //    Bitmap bmp1 = sourceFilePath;
        //    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

        //    // Create an Encoder object based on the GUID
        //    // for the Quality parameter category.
        //    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

        //    // Create an EncoderParameters object.
        //    // An EncoderParameters object has an array of EncoderParameter
        //    // objects. In this case, there is only one
        //    // EncoderParameter object in the array.
        //    EncoderParameters myEncoderParameters = new EncoderParameters(1);

        //    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    bmp1.Save(saveFilePath, jpgEncoder, myEncoderParameters);

        //    myEncoderParameter = new EncoderParameter(myEncoder, 50L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    bmp1.Save(saveFilePath2, jpgEncoder, myEncoderParameters);

        //    // Save the bitmap as a JPG file with zero quality level compression.
        //    myEncoderParameter = new EncoderParameter(myEncoder, 30L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    bmp1.Save(saveFilePath3, jpgEncoder, myEncoderParameters);

        //    // Save the bitmap as a JPG file with zero quality level compression.
        //    myEncoderParameter = new EncoderParameter(myEncoder, 15L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    bmp1.Save(saveFilePath4, jpgEncoder, myEncoderParameters);

        //}
        public static string GetPostNameByType(PostType posttype)
        {
            switch ((int)posttype)
            {
                case 0:
                case 1:
                    return "";
                case 2:
                   return "Blog";
                case 3:
                   return "Haber";
                default:
                    return "";
            }
        }

    }
}
