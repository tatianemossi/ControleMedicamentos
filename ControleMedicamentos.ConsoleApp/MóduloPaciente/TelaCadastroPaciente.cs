using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.MóduloPaciente
{
    public class TelaCadastroPaciente : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioPaciente _repositorioPaciente;
        private readonly Notificador _notificador;

        public RepositorioPaciente RepositorioPaciente => _repositorioPaciente;

        public TelaCadastroPaciente(RepositorioPaciente repositorioPaciente, Notificador notificador)
            : base("Cadastro de Pacientes")
        {
            _repositorioPaciente = repositorioPaciente;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Pacientes");

            Paciente novoPaciente = ObterPaciente();

            RepositorioPaciente.Inserir(novoPaciente);

            _notificador.ApresentarMensagem("Paciente cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Paciente");

            bool temPacientesCadastrados = Visualizar("Pesquisando");

            if (temPacientesCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum paciente cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            Paciente pacienteAtualizado = ObterPaciente();

            bool conseguiuEditar = RepositorioPaciente.Editar(numeroPaciente, pacienteAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Paciente");

            bool temPacientesRegistrados = Visualizar("Pesquisando");

            if (temPacientesRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum paciente cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            bool conseguiuExcluir = RepositorioPaciente.Excluir(numeroPaciente);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool Visualizar(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Pacientes");

            List<Paciente> pacientes = RepositorioPaciente.SelecionarTodos();

            if (pacientes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum paciente disponível.", TipoMensagem.Atencao);
                return false;
            }

            Console.WriteLine("Pacientes:");
            foreach (Paciente paciente in pacientes)
                Console.WriteLine(paciente.ToString());

            Console.ReadLine();

            return true;
        }

        private Paciente ObterPaciente()
        {
            Console.WriteLine("Digite o nome do paciente: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o cpf do paciente: ");
            string cpf = Console.ReadLine();

            return new Paciente(nome, cpf);
        } 

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do paciente que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = RepositorioPaciente.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do paciente não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
