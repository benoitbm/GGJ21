﻿#if UNITY_5_3_OR_NEWER
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
    /// Interaction logic for PlayerResources.xaml
    /// </summary>
    public partial class PlayerResources : UserControl
    {
        public PlayerResources()
        {
            this.InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/UI/PlayerResources.xaml");
        }
#endif
    }
}
