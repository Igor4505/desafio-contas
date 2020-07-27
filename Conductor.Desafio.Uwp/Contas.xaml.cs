using Conductor.Desafio.Database.ViewModels;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Conductor.Desafio.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Contas : Page
    {
        public Contas()
        {
            this.InitializeComponent();
        }

     

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContaViewModel viewModel = new ContaViewModel();
                await viewModel.GetContas();
                DataContext = viewModel;
            }
            catch (Exception ex)
            {
                ContentDialog contasResponse = new ContentDialog() { CloseButtonText = "Fechar" };
                contasResponse.Title = "Falha na requisição";
                contasResponse.Content = ex.Message;
                var popups = VisualTreeHelper.GetOpenPopups(Window.Current);
                if (!popups.Any())
                {
                    await contasResponse.ShowAsync();
                }
            }
        }
    }
}
