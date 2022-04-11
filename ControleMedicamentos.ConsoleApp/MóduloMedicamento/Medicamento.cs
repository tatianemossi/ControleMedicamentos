using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.MóduloMedicamento
{
    public class Medicamento : EntidadeBase
    {
        public Medicamento(string nome, string descricao, int quantidade)
        {
            Nome = nome;
            Descricao = descricao;
            Quantidade = quantidade;            
        }
            
        public string Nome { get; }
        public string Descricao { get; }
        public int Quantidade { get; set; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Descrição: " + Descricao + Environment.NewLine +
                "Quantidade: " + Quantidade + Environment.NewLine;
        }
    }

}
