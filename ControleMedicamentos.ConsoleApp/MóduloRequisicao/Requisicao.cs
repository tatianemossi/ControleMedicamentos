using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.MóduloMedicamento;
using ControleMedicamentos.ConsoleApp.MóduloPaciente;
using System;

namespace ControleMedicamentos.ConsoleApp.MóduloRequisicao
{
    public class Requisicao : EntidadeBase
    {
        public Paciente Paciente { get; }
        public Medicamento Medicamento { get; }
        public bool Aprovada { get; set; }
        public DateTime Data { get; }
        public int Hora { get; }
        public int QuantidadeCaixas { get; }

        public Requisicao(Paciente paciente, Medicamento medicamento, DateTime data, int hora, int quantidadeCaixas)
        {
            Paciente = paciente;
            Medicamento = medicamento;
            Data = data;
            Hora = hora;
            QuantidadeCaixas = quantidadeCaixas;

        }
        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Paciente: " + Paciente + Environment.NewLine +
                "Medicamento: " + Medicamento + Environment.NewLine +
                "Data: " + Data + Environment.NewLine +
                "Hora: " + Hora + Environment.NewLine;
        }

        public void VerificarRequisicaoAprovada()
        {

        }
    }
}
