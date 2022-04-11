using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.MóduloMedicamento
{
    public class TelaCadastroMedicamento : TelaBase
    {
        private readonly RepositorioMedicamento _repositorioMedicamento;
        private readonly Notificador _notificador;

        public TelaCadastroMedicamento(RepositorioMedicamento repositorioMedicamento, Notificador notificador)
            : base("Cadastro de Medicamentos")
        {
            _repositorioMedicamento = repositorioMedicamento;
            _notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo("Cadastro de Medicamentos");

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar Medicamentos em Falta");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Medicamento");

            Medicamento novoMedicamento = ObterMedicamento();

            var medicamentoExiste = _repositorioMedicamento.VerificarMedicamentoExiste(novoMedicamento);
            if (medicamentoExiste)
                _repositorioMedicamento.AtualizarQuantidadeMedicamento(novoMedicamento);
            else
                _repositorioMedicamento.Inserir(novoMedicamento);

            _notificador.ApresentarMensagem("Medicamento cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Medicamento");

            bool temMedicamentosCadastrados = Visualizar("Pesquisando");

            if (temMedicamentosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            Medicamento medicamentoAtualizado = ObterMedicamento();

            bool conseguiuEditar = _repositorioMedicamento.Editar(numeroMedicamento, medicamentoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Medicamento editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Medicamento");

            bool temMedicamentosRegistrados = Visualizar("Pesquisando");

            if (temMedicamentosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioMedicamento.Excluir(numeroMedicamento);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Medicamento excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool Visualizar(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Medicamentos");

            List<Medicamento> medicamentos = _repositorioMedicamento.SelecionarTodos();

            if (medicamentos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            Console.WriteLine("Medicamentos: ");
            foreach (Medicamento medicamento in medicamentos)
                Console.WriteLine(medicamento.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarMedicamentosEmFalta()
        {
            MostrarTitulo("Visualização de Medicamentos em Falta");

            List<Medicamento> medicamentosEmFalta = _repositorioMedicamento.VisualizarMedicamentosEmFalta();

            if (medicamentosEmFalta.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento em Falta.", TipoMensagem.Sucesso);
                return false;
            }

            Console.WriteLine("Medicamentos: ");
            foreach (var medicamentoEmFalta in medicamentosEmFalta)
                Console.WriteLine(medicamentoEmFalta.ToString());

            Console.ReadLine();

            return true;
        }

        private Medicamento ObterMedicamento()
        {
            Console.WriteLine("Digite o nome do medicamento: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a descrição do medicamento: ");
            string descricao = Console.ReadLine();

            Console.WriteLine("Digite a quantidade que irá inserir ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            return new Medicamento(nome, descricao, quantidade);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do medicamento que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMedicamento.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do medicamento não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
