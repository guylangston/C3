using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using C3.Core;
using C3.Core.Examples;

namespace C3.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WriteableBitmap bmp;
        private C3DrawingSystemWpfSkiaSharp drawingSystem;


        public MainWindow()
        {
            InitializeComponent();

            try
            {
                IC3Sketch sketch = GetSketch("Example");

                drawingSystem = new C3DrawingSystemWpfSkiaSharp(sketch)
                {
                    UpdateToolbar = (t) => this.toolbar.Text = t
                };

                drawingSystem.Setup((int)this.Width, (int)this.Height, 60);
                this.image.Source = drawingSystem.GetImageSource();

                drawingSystem.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), e.GetType().Name);
                Environment.Exit(-1);
            }
          
        }

        private IC3Sketch GetSketch(string example)
        {
            var factory = new SketchFactory();
            return factory.GetSketch(example);
        }
    }
}
