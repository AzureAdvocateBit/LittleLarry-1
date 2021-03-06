﻿using GHIElectronics.UWP.Shields;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlinkyPants
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FEZHAT _hat;
        private DispatcherTimer _timer;
        public MainPage()
        {
            InitializeComponent();
            SetupAsync();
        }

        private async void SetupAsync()
        {
            _hat = await FEZHAT.CreateAsync();

            _timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(100) };

            bool on = false;
            _timer.Tick += (s, e) =>
            {
                _hat.D2.Color = on ? FEZHAT.Color.Red : FEZHAT.Color.Black;
                on = !on;
            };

            _timer.Start();
        }
    }
}
