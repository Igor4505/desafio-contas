using Conductor.Desafio.Database.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
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
    public sealed partial class Pessoas : Page
    {
        public Pessoas()
        {
            this.InitializeComponent();
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PessoaViewModel viewModel = new PessoaViewModel();
                await viewModel.GetPessoas();
                DataContext = viewModel;
            }
            catch (Exception ex)
            {
                ContentDialog pessoasResponse = new ContentDialog() { CloseButtonText = "Fechar" };
                pessoasResponse.Title = "Falha na requisição";
                pessoasResponse.Content = ex.Message;
                var popups = VisualTreeHelper.GetOpenPopups(Window.Current);
                if (!popups.Any())
                {
                    await pessoasResponse.ShowAsync();
                }
            }
        }
    }
}
