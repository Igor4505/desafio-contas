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
    public sealed partial class Transacoes : Page
    {
        public Transacoes()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TransacoesViewModel viewModel = new TransacoesViewModel();
                await viewModel.GetTransacoes();
                DataContext = viewModel;
            }
            catch (Exception ex)
            {
                ContentDialog transacoesResponse = new ContentDialog() { CloseButtonText = "Fechar" };
                transacoesResponse.Title = "Falha na requisição";
                transacoesResponse.Content = ex.Message;
                var popups = VisualTreeHelper.GetOpenPopups(Window.Current);
                if (!popups.Any())
                {
                    await transacoesResponse.ShowAsync();

                }
            }
        }
    }
}
