using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ParallelTasks
{
    internal class Examples
    {
        public void ParallelInvoke()
        {
            Action lowerTask = () => WriteCount(0, 1000);
            Action upperTask = () => WriteCount(1000, 2000);
            Parallel.Invoke(lowerTask, upperTask);
        }

        public void TaskFactoryStartNew()
        {
            var lowerTask = Task.Factory.StartNew(() => WriteCount(0, 1000));
            var upperTask = Task.Factory.StartNew(() => WriteCount(1000, 2000));

            Task.WaitAll(lowerTask, upperTask);
        }

        public void ParallelInvokeImageProcessing()
        {
            var directory = Directory.GetCurrentDirectory();
            var firstImageFilePath = Path.Combine(directory, "Image1.jpg");
            var secondImageFilePath = Path.Combine(directory, "Image2.jpg");
            var resultFilePath = Path.Combine(directory, "Result.jpg");

            var original1 = new Bitmap(firstImageFilePath);
            var original2 = new Bitmap(secondImageFilePath);

            var source1 = original1.ResizeBitmap(new Size(original1.Width / 4, original1.Height / 4));
            var source2 = original2.ResizeBitmap(new Size(original2.Width / 4, original2.Height / 4));

            var layer1 = new Bitmap(source1.Width, source1.Height);
            var layer2 = new Bitmap(source2.Width, source2.Height);

            using (var result = new Bitmap(source1.Width, source1.Height))
            {
                var blender = Graphics.FromImage(result);
                blender.CompositingMode = CompositingMode.SourceOver;

                Parallel.Invoke(() => SetToGray(source1, layer1), () => Rotate(source2, layer2));
                Blend(layer1, layer2, blender);

                result.Save(resultFilePath, ImageFormat.Jpeg);
            }
        }

        private static void Blend(Image layer1, Image layer2, Graphics blender)
        {
            blender.DrawImage(layer1, 0, 0);
            blender.DrawImage(layer2, 0, 0);
        }

        private void Rotate(Bitmap source, Bitmap layer)
        {
            source.CopyPixels(layer);
            layer.RotateFlip(RotateFlipType.Rotate90FlipNone);
            layer.SetAlpha(128);
        }

        private void SetToGray(Bitmap source, Bitmap layer)
        {
            source.CopyPixels(layer);
            layer.SetGray();
            layer.SetAlpha(128);
        }

        private void WriteCount(int fromInclusive, int toExclusive)
        {
            for (var i = fromInclusive; i < toExclusive; i++)
            {
                Console.WriteLine(i + 1);
            }
        }
    }
}