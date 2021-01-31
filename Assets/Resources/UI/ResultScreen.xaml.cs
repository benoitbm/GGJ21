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
    /// Interaction logic for ResultScreen.xaml
    /// </summary>
    public partial class ResultScreen : UserControl
    {
        public ResultScreen()
        {
            this.InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/UI/ResultScreen.xaml");
        }
#endif
    }
}
