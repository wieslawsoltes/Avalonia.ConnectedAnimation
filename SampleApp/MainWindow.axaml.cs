using Avalonia;
using Avalonia.ConnectedAnimation;
using Avalonia.Controls;
using Avalonia.Interactivity;

using FluentAvalonia.UI.Media.Animation;
using FluentAvalonia.UI.Navigation;

namespace SampleApp
{
    public partial class MainWindow : Window
    {
        // NOTE: When calling TryStart, if the Transition has not been completed, the Destination may be shifted.
        // NOTE: TryStart を呼び出すとき Transition が完了していない場合、Destination がずれる場合があります。
        public static readonly NavigationTransitionInfo DefaultTransition = new SuppressNavigationTransitionInfo();

        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(typeof(FirstPage), null, DefaultTransition);
            frame.Navigating += Frame_Navigating;
            frame.Navigated += Frame_Navigated;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private async void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is IPage page && e.Content is Control ctr)
            {
                await ctr.WaitVisualTreeAttached();
                page.OnNavigateTo(e);
            }
        }

        private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (frame.Content is IPage page)
            {
                page.OnNavigateFrom(e);
            }
        }

        private void Back_Click(object? sender, RoutedEventArgs e)
        {
            frame.GoBack();
        }
    }
}