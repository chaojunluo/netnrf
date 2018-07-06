using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace DB
{
    public class Common
    {
        #region 生成验证码

        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="num">随机码</param>
        public static byte[] CreateImg(string num)
        {
            Bitmap image = new Bitmap(130, 45);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.FromArgb(212, 215, 251));
                //画图片的干扰线
                for (int i = 0; i < 10; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.FromArgb(56, 173, 181), 1), x1, y1, x2, y2);
                }

                Font font = new Font("Ravie", 22, (FontStyle.Italic | FontStyle.Bold));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.FromArgb(146, 146, 145), Color.FromArgb(66, 66, 66), 1.9f, true);
                g.DrawString(num, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 40; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion

        #region 随机字符串
        /// <summary>
        /// 随机字符 验证码
        /// </summary>
        /// <param name="strLen">长度 默认4个字符</param>
        /// <param name="source">自定义随机的字符源</param>
        /// <returns></returns>
        public static string RandomCode(int strLen = 4, string source = "3456789ABCDEFGHJKMNPQRSUVWXY3456789")
        {
            string result = string.Empty;
            if (strLen > 0)
            {
                Random rd = new Random(Guid.NewGuid().GetHashCode());
                for (int i = 0; i < strLen; i++)
                    result += source[rd.Next(source.Length)].ToString();
            }
            return result;
        }
        #endregion
    }
}