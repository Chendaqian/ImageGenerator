using System; // 基础类型
using System.Windows.Forms; // WinForms

namespace ImageGenerator
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles(); // 启用视觉样式
            Application.SetCompatibleTextRenderingDefault(false); // 文本渲染
            Application.Run(new ImgGenForm()); // 运行主窗体
        }
    }
}