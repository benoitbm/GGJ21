#if UNITY_5_3_OR_NEWER
#define NOESIS
   using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif

namespace GGJ21_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Grid
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/UI/MainWindow.xaml");
        }
#endif
    }
}
