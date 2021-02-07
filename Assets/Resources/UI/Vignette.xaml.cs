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
    /// Interaction logic for Vignette.xaml
    /// </summary>
    public partial class Vignette : UserControl
    {
        public Vignette()
        {
            this.InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/UI/Vignette.xaml");
        }
#endif
    }
}
