using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Deiofiber.Common
{
    public class ImageHelper
    {
        #region Create Thumbnail

        /// <summary>
        /// Creates the thumbnail.
        /// </summary>
        /// <param name="imageContent">Content of the image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="setRatio">if set to <c>true</c> [no ratio].</param>
        /// <param name="drawArea">if set to <c>true</c> [draw area].</param>
        /// <returns></returns>
        public static byte[] CreateThumbnail(byte[] imageContent, int width, int height, bool setRatio, bool drawArea)
        {
            // put input image content into stream
            using (MemoryStream origStream = new MemoryStream(imageContent))
            {
                // create image
                Image origImage = Image.FromStream(origStream);

                if (origImage.Height > origImage.Width)
                {
                    return CreateThumbnail(imageContent, width, height);
                }
                else
                {
                    // if image is smaller, then send image
                    if (origImage.Width < width && origImage.Height < height) return imageContent;

                    if (setRatio)
                    {
                        // find ratio
                        double ratio = FindRatio(origImage.Width, origImage.Height, width, height);

                        // get new dimensions
                        width = Convert.ToInt32(Convert.ToDouble(origImage.Width) / ratio);
                        height = Convert.ToInt32(Convert.ToDouble(origImage.Height) / ratio);
                    }

                    // create new bitmap
                    Bitmap resized = new Bitmap(width, height);
                    resized.SetResolution(resized.HorizontalResolution, resized.VerticalResolution);

                    // set up high quality graphics
                    Graphics gr = Graphics.FromImage(resized);
                    gr.SmoothingMode = SmoothingMode.AntiAlias;
                    gr.InterpolationMode = InterpolationMode.High; //InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    // put image in
                    if (!drawArea)
                        gr.DrawImage(origImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, origImage.Width, origImage.Height), GraphicsUnit.Pixel);
                    else
                    {
                        gr.DrawImage(origImage, new Rectangle(0, 0, width, height), new Rectangle(10, 10, width + 10, height + 10), GraphicsUnit.Pixel);
                    }

                    // save it
                    using (MemoryStream resizedStream = new MemoryStream())
                    {
                        resized.Save(resizedStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        //Dispose resources
                        gr.Dispose();
                        resized.Dispose();
                        origImage.Dispose();

                        return resizedStream.GetBuffer();
                    }
                }
            }
        }

        /// <summary>
        /// Find ratio for resizing
        /// </summary>
        /// <param name="widthOrig">The width orig.</param>
        /// <param name="heightOrig">The height orig.</param>
        /// <param name="widthNew">The width new.</param>
        /// <param name="heightNew">The height new.</param>
        /// <returns>ratio</returns>
        private static double FindRatio(int widthOrig, int heightOrig, int widthNew, int heightNew)
        {
            double ratio = 1;

            // ratioWidth
            double ratioWidth = Convert.ToDouble(widthOrig) / Convert.ToDouble(widthNew);
            double ratioHeight = Convert.ToDouble(heightOrig) / Convert.ToDouble(heightNew);

            // take the smaller ratio for a bigger thumbnail
            //ratio = (ratioWidth > ratioHeight) ? ratioHeight : ratioWidth;

            // take the bigger ratio for a smaller thumbnail
            ratio = (ratioWidth > ratioHeight) ? ratioWidth : ratioHeight;

            return ratio;
        }

        #endregion Create Thumbnail

        public static byte[] CreateThumbnail(byte[] image, int width, int height)
        {
            using (MemoryStream origStream = new MemoryStream(image))
            {
                // create image
                Image imgPhoto = Image.FromStream(origStream);

                int sourceWidth = imgPhoto.Width;
                int sourceHeight = imgPhoto.Height;
                int sourceX = 0;
                int sourceY = 0;
                int destX = 0;
                int destY = 0;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)width / (float)sourceWidth);
                nPercentH = ((float)height / (float)sourceHeight);
                if (nPercentH < nPercentW)
                {
                    nPercent = nPercentH;
                    destX = System.Convert.ToInt16((width -
                                  (sourceWidth * nPercent)) / 2);
                }
                else
                {
                    nPercent = nPercentW;
                    destY = System.Convert.ToInt16((height -
                                  (sourceHeight * nPercent)) / 2);
                }

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.Clear(Color.Gray);
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

                grPhoto.DrawImage(imgPhoto,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);

                // save it
                using (MemoryStream resizedStream = new MemoryStream())
                {
                    bmPhoto.Save(resizedStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    grPhoto.Dispose();
                    return resizedStream.GetBuffer();
                }
            }
        }
    }
}