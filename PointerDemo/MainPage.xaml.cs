using PointerDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PointerDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<DisplaySegmentViewModel> segments = new List<DisplaySegmentViewModel>();

        public MainPage()
        {
            var isGpioAvailable = ApiInformation.IsApiContractPresent("Windows.Devices.DevicesLowLevelContract", 2);
            var gpioController = isGpioAvailable ? GpioController.GetDefault() : null;

            for (int i = 0; i < 7; i++)
            {
                var item = new DisplaySegmentViewModel();

                if (gpioController != null)
                {
                    var pin = gpioController.OpenPin(i + 2);
                    item.GpioPin = pin;
                    pin.SetDriveMode(GpioPinDriveMode.Output);
                    pin.Write(GpioPinValue.Low);
                }

                segments.Add(item);
            }

            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Segment_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Touch || e.Pointer.IsInContact)
            {
                var viewModel = (DisplaySegmentViewModel)((FrameworkElement)sender).DataContext;
                viewModel.IsOn = !viewModel.IsOn;
            }
        }

        private void Segment_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType != PointerDeviceType.Touch)
            {
                var viewModel = (DisplaySegmentViewModel)((FrameworkElement)sender).DataContext;
                viewModel.IsOn = !viewModel.IsOn;
            }
        }
    }
}
