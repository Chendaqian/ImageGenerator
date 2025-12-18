using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageGenerator
{
    public partial class ImgGenForm : Form
    {
        public ImgGenForm()
        {
            InitializeComponent();

            string baseDir = AppDomain.CurrentDomain.BaseDirectory; // 程序目录
            string fmtInit = this.cmbFormat.SelectedItem != null ? this.cmbFormat.SelectedItem.ToString() : "PNG"; // 初始格式
            string extInit = GetExtensionForFormat(fmtInit); // 扩展名
            string tsInit = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"); // 时间戳
            this.txtPath.Text = Path.Combine(baseDir, tsInit + "." + extInit); // 默认路径+文件名

            this.cmbFormat.SelectedIndex = this.cmbFormat.Items.Count - 1;
            this.cmbUnit.SelectedIndex = this.cmbUnit.Items.Count - 1;
        }

        private void OnBrowseClick(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog(); // 保存对话框
            string fmt = GetSelectedFormat(); // 当前格式
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; // 默认目录
            string ts = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"); // 默认文件名
            string ext = GetExtensionForFormat(fmt); // 扩展名
            sfd.FileName = ts + "." + ext; // 默认文件名
            if (fmt == "BMP")
            {
                sfd.Filter = "BMP 图片|*.bmp"; // 过滤器
                sfd.DefaultExt = "bmp"; // 默认扩展名
            }
            else if (fmt == "JPG")
            {
                sfd.Filter = "JPEG 图片|*.jpg;*.jpeg"; // 过滤器
                sfd.DefaultExt = "jpg"; // 默认扩展名
            }
            else
            {
                sfd.Filter = "PNG 图片|*.png"; // 过滤器
                sfd.DefaultExt = "png"; // 默认扩展名
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = sfd.FileName; // 设置路径
            }
        }

        private void OnGenerateClick(object sender, EventArgs e)
        {
            if (this.txtPath.Text == null || this.txtPath.Text.Length == 0) // 校验路径
            {
                MessageBox.Show("请先选择保存路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); // 提示
                return;
            }
            string fmt = GetSelectedFormat(); // 读取格式
            string unit = GetSelectedUnit(); // 读取单位
            int value = (int)this.nudSizeValue.Value; // 读取数值
            long targetBytes = 0; // 目标字节数
            if (unit == "KB")
            {
                targetBytes = (long)value * 1024L; // KB 转字节
            }
            else
            {
                targetBytes = (long)value * 1024L * 1024L; // MB 转字节
            }

            try
            {
                GenerateImage(fmt, this.txtPath.Text, targetBytes); // 生成图片
                FileInfo fi = new FileInfo(this.txtPath.Text); // 文件信息
                this.lblStatus.Text = "生成成功，实际大小: " + fi.Length + " 字节"; // 显示结果
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); // 错误提示
            }
        }

        private string GetSelectedFormat()
        {
            string fmt = "PNG"; // 默认
            if (this.cmbFormat.SelectedItem != null)
            {
                fmt = this.cmbFormat.SelectedItem.ToString(); // 格式文本
            }
            return fmt; // 返回格式
        }

        private string GetSelectedUnit()
        {
            string unit = "KB"; // 默认
            if (this.cmbUnit.SelectedItem != null)
            {
                unit = this.cmbUnit.SelectedItem.ToString(); // 单位文本
            }
            return unit; // 返回单位
        }

        private string GetExtensionForFormat(string fmt)
        {
            if (fmt == "BMP") // BMP 扩展名
            {
                return "bmp"; // 返回扩展名
            }
            else if (fmt == "JPG") // JPG 扩展名
            {
                return "jpg"; // 返回扩展名
            }
            else
            {
                return "png"; // 默认PNG扩展名
            }
        }

        private void GenerateImage(string formatName, string path, long targetBytes)
        {
            if (formatName == "BMP")
            {
                GenerateBmp(path, targetBytes); // 生成BMP
            }
            else if (formatName == "JPG")
            {
                GenerateJpeg(path, targetBytes); // 生成JPEG
            }
            else
            {
                GeneratePng(path, targetBytes); // 生成PNG
            }
        }

        private void GenerateBmp(string path, long targetBytes)
        {
            int headerBytes = 54; // BMP头大小
            int width = 1024; // 固定宽度
            int bytesPerPixel = 3; // 24bpp
            int stride = ((width * bytesPerPixel + 3) / 4) * 4; // 每行对齐
            long payload = targetBytes - headerBytes; // 有效像素区
            if (payload < stride)
            {
                payload = stride; // 至少一行
            }
            int height = (int)(payload / stride); // 计算高度
            if (height < 1)
            {
                height = 1; // 最小高度
            }

            using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                FillNoise(bmp); // 填充随机像素
                bmp.Save(path, ImageFormat.Bmp); // 保存BMP
            }

            FileInfo fi = new FileInfo(path); // 文件信息
            if (fi.Length > targetBytes)
            {
                long expected = (long)stride * (long)height + (long)headerBytes; // 计算理论大小
                while (expected > targetBytes && height > 1) // 尝试减小高度
                {
                    height = height - 1; // 减少一行
                    using (Bitmap bmp2 = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                    {
                        FillNoise(bmp2); // 填充随机像素
                        bmp2.Save(path, ImageFormat.Bmp); // 保存BMP
                    }
                    expected = (long)stride * (long)height + (long)headerBytes; // 更新理论值
                }
                fi = new FileInfo(path); // 刷新文件信息
            }
            if (fi.Length < targetBytes)
            {
                AppendPadding(path, targetBytes - fi.Length); // 追加填充
            }
        }

        private void GenerateJpeg(string path, long targetBytes)
        {
            int width = 1920; // 初始宽度
            int height = 1080; // 初始高度
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb); // 位图
            FillNoise(bmp); // 填充随机像素

            ImageCodecInfo codec = GetEncoder(ImageFormat.Jpeg); // JPEG编码器
            if (codec == null)
            {
                bmp.Save(path, ImageFormat.Jpeg); // 备用保存
                bmp.Dispose(); // 释放
                FileInfo fiFallback = new FileInfo(path); // 文件信息
                if (fiFallback.Length < targetBytes)
                {
                    AppendPadding(path, targetBytes - fiFallback.Length); // 填充
                }
                return;
            }

            int qLow = 10; // 最低质量
            int qHigh = 100; // 最高质量
            byte[] bestBytes = null; // 最优数据
            long bestSize = 0; // 最优大小

            while (qLow <= qHigh)
            {
                int qMid = (qLow + qHigh) / 2; // 二分质量
                EncoderParameters ep = new EncoderParameters(1); // 编码参数
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)qMid); // 设置质量

                MemoryStream ms = new MemoryStream(); // 内存流
                bmp.Save(ms, codec, ep); // 保存到内存
                long len = ms.Length; // 长度

                if (len <= targetBytes) // 小于等于目标
                {
                    bestBytes = ms.ToArray(); // 更新数据
                    bestSize = len; // 更新大小
                    qLow = qMid + 1; // 提高质量
                }
                else
                {
                    qHigh = qMid - 1; // 降低质量
                }

                ms.Close(); // 关闭流
            }

            bmp.Dispose(); // 释放位图

            if (bestBytes != null)
            {
                File.WriteAllBytes(path, bestBytes); // 写入文件
            }
            else
            {
                bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb); // 兜底位图
                FillNoise(bmp); // 填充随机像素
                bmp.Save(path, ImageFormat.Jpeg); // 兜底保存
                bmp.Dispose(); // 释放
            }

            FileInfo fi = new FileInfo(path); // 文件信息
            if (fi.Length < targetBytes)
            {
                AppendPadding(path, targetBytes - fi.Length); // 追加填充
            }
        }

        private void GeneratePng(string path, long targetBytes)
        {
            int width = 1024; // 初始宽度
            int height = 768; // 初始高度
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb); // 位图
            FillNoise(bmp); // 填充随机像素

            MemoryStream ms = new MemoryStream(); // 内存流
            bmp.Save(ms, ImageFormat.Png); // 保存到内存
            long len = ms.Length; // 当前大小

            if (len > targetBytes)
            {
                while (len > targetBytes && width > 32 && height > 32) // 逐步减小尺寸
                {
                    width = width * 9 / 10; // 缩小宽度
                    height = height * 9 / 10; // 缩小高度
                    bmp.Dispose(); // 释放位图
                    bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb); // 新位图
                    FillNoise(bmp); // 填充随机像素
                    ms.SetLength(0); // 清空内存流
                    bmp.Save(ms, ImageFormat.Png); // 重新保存
                    len = ms.Length; // 更新大小
                }
            }
            else
            {
                while (len < targetBytes && width < 8000 && height < 8000) // 逐步放大尺寸
                {
                    width = width * 11 / 10; // 放大宽度
                    height = height * 11 / 10; // 放大高度
                    bmp.Dispose(); // 释放位图
                    bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb); // 新位图
                    FillNoise(bmp); // 填充随机像素
                    ms.SetLength(0); // 清空内存流
                    bmp.Save(ms, ImageFormat.Png); // 重新保存
                    len = ms.Length; // 更新大小
                }
            }

            bmp.Dispose(); // 释放位图
            File.WriteAllBytes(path, ms.ToArray()); // 写入文件
            ms.Close(); // 关闭内存流

            FileInfo fi = new FileInfo(path); // 文件信息
            if (fi.Length < targetBytes)
            {
                AppendPadding(path, targetBytes - fi.Length); // 追加填充
            }
        }

        private void FillNoise(Bitmap bmp)
        {
            Random rnd = new Random(12345); // 随机种子
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height); // 锁定位矩形
            BitmapData data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb); // 锁定位图
            try
            {
                int total = data.Stride * data.Height; // 字节总数
                byte[] buffer = new byte[total]; // 缓冲区
                int i = 0; // 索引
                while (i < total)
                {
                    buffer[i] = (byte)rnd.Next(0, 256); // 填充随机字节
                    i = i + 1; // 递增索引
                }
                Marshal.Copy(buffer, 0, data.Scan0, total); // 写入位图
            }
            finally
            {
                bmp.UnlockBits(data); // 解锁位图
            }
        }

        private void AppendPadding(string path, long paddingBytes)
        {
            if (paddingBytes <= 0) // 无需填充
            {
                return;
            }
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write); // 追加模式
            try
            {
                int chunk = 8192; // 块大小
                byte[] zeros = new byte[chunk]; // 填充数据
                long remaining = paddingBytes; // 剩余字节
                while (remaining > 0)
                {
                    int writeSize = remaining >= chunk ? chunk : (int)remaining; // 本次写入
                    fs.Write(zeros, 0, writeSize); // 写入零字节
                    remaining = remaining - writeSize; // 更新剩余
                }
            }
            finally
            {
                fs.Close(); // 关闭文件
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders(); // 获取编码器
            int i = 0; // 索引
            while (i < codecs.Length)
            {
                if (codecs[i].FormatID == format.Guid) // 匹配编码器
                {
                    return codecs[i]; // 返回编码器
                }
                i = i + 1; // 递增索引
            }
            return null; // 未找到返回空
        }
    }
}