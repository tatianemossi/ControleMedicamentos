using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.MóduloMedicamento;
using ControleMedicamentos.ConsoleApp.MóduloRequisicao;
using System;

namespace ControleMedicamentos.ConsoleApp
{
    internal class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroMedicamento)
                    GerenciarCadastroMedicamentos(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroRequisicao)
                    GerenciarCadastroRequisicoes(telaSelecionada, opcaoSelecionada);
            }
        }

        private static void GerenciarCadastroMedicamentos(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroMedicamento telaCadastroMedicamento = telaSelecionada as TelaCadastroMedicamento;

            if (telaCadastroMedicamento is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroMedicamento.Inserir();

            else if (opcaoSelecionada == "2")
                telaCadastroMedicamento.Editar();

            else if (opcaoSelecionada == "3")
                telaCadastroMedicamento.Excluir();

            else if (opcaoSelecionada == "4")
                telaCadastroMedicamento.Visualizar("Tela");

            else if (opcaoSelecionada == "5")
                telaCadastroMedicamento.VisualizarMedicamentosEmFalta();
        }

        private static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            ITelaCadastravel telaCadastroBasico = telaSelecionada as ITelaCadastravel;

            if (telaCadastroBasico is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroBasico.Inserir();

            else if (opcaoSelecionada == "2")
                telaCadastroBasico.Editar();

            else if (opcaoSelecionada == "3")
                telaCadastroBasico.Excluir();

            else if (opcaoSelecionada == "4")
                telaCadastroBasico.Visualizar("Tela");
        }
         
        private static void GerenciarCadastroRequisicoes(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroRequisicao telaCadastroRequisicao = telaSelecionada as TelaCadastroRequisicao;

            if (telaCadastroRequisicao is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroRequisicao.RegistrarRequisicao();

            else if (opcaoSelecionada == "2")
                telaCadastroRequisicao.Visualizar("Tela");

            //else if (opcaoSelecionada == "3")
            //    telaCadastroRequisicao.VisualizarMedicamentosMaisRetirados();
        }
    }
}
