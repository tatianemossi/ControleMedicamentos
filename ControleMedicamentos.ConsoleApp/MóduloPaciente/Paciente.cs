using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.MóduloPaciente
{
    public class Paciente : EntidadeBase
    {
        public Paciente(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
        }

        public string Nome { get; }
        public string CPF { get; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "CPF: " + CPF + Environment.NewLine;
        }
    }
}
