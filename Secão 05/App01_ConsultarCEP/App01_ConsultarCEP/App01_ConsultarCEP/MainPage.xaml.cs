using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - VALIDAÇÕES
            string cep = CEP.Text.Trim(); //CEP EM MAIUSCULO É O ELEMENTO DA TELA

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if(end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} - {3}, {0}-{1} ", end.localidade, end.uf,
                        end.logradouro, end.bairro);

                        //TESTE MEU
                        ENDERECO.Text = string.Format("{0}", end.logradouro);
                        CIDADE.Text = string.Format("{0}", end.localidade);
                        UF.Text = string.Format("{0}", end.uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado. " + cep, "OK");
                    }

                    
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter 8 caracteres.", "OK");

                valido = false;
            }

            int NovoCEP = 0;
            if (!int.TryParse(cep, out NovoCEP)) //TryParse = tente converter
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter somente números.", "OK");

                valido = false;
            }
            return valido;
        }
    }
}
