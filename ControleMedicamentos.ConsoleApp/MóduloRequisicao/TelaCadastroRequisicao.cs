using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.MóduloMedicamento;
using ControleMedicamentos.ConsoleApp.MóduloPaciente;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.MóduloRequisicao
{
    public class TelaCadastroRequisicao : TelaBase
    {
        private readonly Notificador _notificador;
        private readonly IRepositorio<Requisicao> _repositorioRequisicao;
        private readonly IRepositorio<Paciente> _repositorioPaciente;
        private readonly IRepositorio<Medicamento> _repositorioMedicamento;
        private readonly TelaCadastroPaciente _telaCadastroPaciente;
        private readonly TelaCadastroMedicamento _telaCadastroMedicamento;

        public TelaCadastroRequisicao(
            Notificador notificador,
            IRepositorio<Requisicao> repositorioRequisicao,
            IRepositorio<Paciente> repositorioPaciente,
            IRepositorio<Medicamento> repositorioMedicamento,
            TelaCadastroPaciente telaCadastroPaciente,
            TelaCadastroMedicamento telaCadastroMedicamento) : base("Cadastro de Requisições")
        {
            this._notificador = notificador;
            this._repositorioRequisicao = repositorioRequisicao;
            this._repositorioPaciente = repositorioPaciente;
            this._repositorioMedicamento = repositorioMedicamento;
            this._telaCadastroPaciente = telaCadastroPaciente;
            this._telaCadastroMedicamento = telaCadastroMedicamento;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Registrar Requisição");
            Console.WriteLine("Digite 2 para Visualizar");
            Console.WriteLine("Digite 3 para Visualizar Medicamentos mais retirados");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void RegistrarRequisicao()
        {
            MostrarTitulo("Cadastro de Requisição");

            //Obtendo Paciente
            Paciente pacienteSelecionado = ObtemPaciente();

            if (pacienteSelecionado == null)
            {
                _notificador.ApresentarMensagem("Nenhum paciente selecionado", TipoMensagem.Erro);
                return;
            }

            //Obtendo Medicamento
            Medicamento medicamentoSelecionado = ObtemMedicamento();

            if (medicamentoSelecionado == null)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento selecionado", TipoMensagem.Erro);
                return;
            }

            var novaRequisicao = ObterRequisicao(pacienteSelecionado, medicamentoSelecionado);
            if (novaRequisicao == null)
            {
                _notificador.ApresentarMensagem("Não foi possível cadastrar.", TipoMensagem.Atencao);
            }
            else
            {
                _repositorioRequisicao.Inserir(novaRequisicao);
                _notificador.ApresentarMensagem("Requisição cadastrada com sucesso!", TipoMensagem.Sucesso);
            }
        }

        private Paciente ObtemPaciente()
        {
            bool temPacientesDisponiveis = _telaCadastroPaciente.Visualizar("Pesquisando");

            if (!temPacientesDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhum paciente disponível para cadastrar requisições.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do paciente que irá registrar a requisição: ");
            int numeroPacienteRequisicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Paciente pacienteSelecionado = _repositorioPaciente.SelecionarRegistro(numeroPacienteRequisicao);

            return pacienteSelecionado;
        }

        public void VisualizarMedicamentosMaisRetirados()
        {
            Console.WriteLine("Função não implementada.");
        }

        private Medicamento ObtemMedicamento()
        {
            bool temMedicamentosDisponiveis = _telaCadastroMedicamento.Visualizar("Pesquisando");

            if (!temMedicamentosDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhum medicamento disponível para cadastrar requisições.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do medicamento que irá registrar a requisição: ");
            int numeroMedicamentoRequisicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Medicamento medicamentoSelecionado = _repositorioMedicamento.SelecionarRegistro(numeroMedicamentoRequisicao);

            return medicamentoSelecionado;
        }

        public bool Visualizar(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Requisições");

            List<Requisicao> requisicoes = _repositorioRequisicao.SelecionarTodos();

            if (requisicoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Requisição disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (var requisicao in requisicoes)
                Console.WriteLine(requisicao.ToString());

            Console.ReadLine();

            return true;
        }

        private Requisicao ObterRequisicao(Paciente paciente, Medicamento medicamento)
        {
            Requisicao novaRequisicao = new Requisicao(paciente, medicamento, DateTime.Now.Date, DateTime.Now.Hour);

            Console.WriteLine("Digite a quantidade de Caixas do medicamento que deseja pegar: ");
            int quantidadeCaixasMedicamento = Convert.ToInt32(Console.ReadLine());

            if (quantidadeCaixasMedicamento > 5 || quantidadeCaixasMedicamento > medicamento.Quantidade)
                return null;
            else
            {
                novaRequisicao.Aprovada = true;
                medicamento.Quantidade = medicamento.Quantidade - quantidadeCaixasMedicamento;
            }

            novaRequisicao.QuantidadeCaixas = quantidadeCaixasMedicamento;

            return novaRequisicao;
        }
    }
}
