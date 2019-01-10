using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

namespace MuesF.Base
{
    public class PictureCrop
    {
    //    public static Bitmap CustomCropImage(int x1, int y1, int width, int height, Stream responseStream)
    //    {
    //        /*
    //           C# tarafında bir resmi kesme ve boyutlandırma için ihtiyacımız olan belli parametreler vardır. Öncelikle kesmemiz gereken resim elimizde olmalı. Eğer ki bitmap olarak resim elimizdeyse işleme devam edebiliriz fakat elimizde resmin urli varsa öncelikle bu urlden resmi çekmemiz gerekir. Bunun için şu kod bloğu kullanılabilir:
    //         */
    //        //HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(src);
    //        //WebResponse imageResponse = imageRequest.GetResponse();
    //        //Stream responseStream = imageResponse.GetResponseStream();
    //        Bitmap bmp = Image.FromStream(responseStream) as Bitmap;
    //        /*
    //          Bu şekilde resmi aldıktan sonra ikinci aşama resim kesme(crop) işlemidir. Bunun için ihtiyacımız olan aslında resmi kesmek istediğimiz şekilden başka birşey değildir. Bu örneğimizde bunun bir dikdörtgen olduğunu varsayalım:

    //         */

    //        Rectangle cropRect = new Rectangle(x1, y1, width, height);

    //        /*
    //            x1 : kesilmesini istediğimiz yerin soldan uzaklığı
    //            y1 : kesilmesini istediğimiz yerin üstten uzaklığı
    //            width : dikdörtgenin genişliği(kesmek istediğimiz alanın genişliği)
    //            height : dikdörtgenin yüksekliği(kesmek istediğimiz alanın yüksekliği)

    //            Bu şekilde kesmek istediğimiz bölümü oluşturduktan sonra geriye o bölümü resmin içinden kesmek kalıyor:

    //         */
    //        Bitmap croppedImage = bmp.Clone(cropRect, bmp.PixelFormat);

    //        /*
    //            Kestiğimiz kısmı aynı zamanda yeniden boyutlandırabiliriz de (resize).

    //            Örneğin kestiğimiz kısmın boyutunun 200 px genişlik ve 200 px yükseklikte olmasını istediğimizi varsayalım.
    //         */

    //        //Bitmap resizedImage = new Bitmap(200, 200);
    //        //using (Graphics g = Graphics.FromImage(resizedImage))
    //        //    g.DrawImage(croppedImage, 0, 0, 200, 200);

    //        /*
    //         Bu işlemin ardından yeniden boyutlandırılmış resmi elde etmiş oluruz.
    //         */
    //        return croppedImage;
    //    }

    //    public static Bitmap CustomResizeImage(Bitmap resim, int boyut)
    //    {
    //        Bitmap sresim = resim;
    //        using (Bitmap OrjinalResim = resim)
    //        {
    //            double yukseklik = OrjinalResim.Height;
    //            double genislik = OrjinalResim.Width;
    //            double oran = 0;

    //            if (genislik >= boyut)
    //            {
    //                oran = genislik / yukseklik;
    //                genislik = boyut;
    //                yukseklik = genislik / oran;
    //                Size ydeger = new Size(Convert.ToInt32(genislik), Convert.ToInt32(yukseklik));
    //                Bitmap yresim = new Bitmap(OrjinalResim, ydeger);
    //                sresim = yresim;
    //            }
    //        }
    //        return sresim;
    //    }

    //    public static Bitmap CustomResizeImageH(Bitmap resim, int boyut)
    //    {
    //        Bitmap sresim = resim;
    //        using (Bitmap OrjinalResim = resim)
    //        {
    //            double yukseklik = OrjinalResim.Height;
    //            double genislik = OrjinalResim.Width;
    //            double oran = 0;

    //            if (yukseklik >= boyut)
    //            {
    //                oran = yukseklik / genislik;
    //                yukseklik = boyut;
    //                genislik = yukseklik / oran;
    //                Size ydeger = new Size(Convert.ToInt32(genislik), Convert.ToInt32(yukseklik));
    //                Bitmap yresim = new Bitmap(OrjinalResim, ydeger);
    //                sresim = yresim;
    //            }
    //        }
    //        return sresim;
    //    }
    }
}
