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

            drawingSystem = new C3DrawingSystemWpfSkiaSharp(new SampleSketch())
            {
                UpdateToolbar = (t) => this.toolbar.Text = t
            };

            drawingSystem.Setup((int) this.Width, (int) this.Height, 60);
            this.image.Source = drawingSystem.GetImageSource();

            drawingSystem.Start();
        }
    }
}
