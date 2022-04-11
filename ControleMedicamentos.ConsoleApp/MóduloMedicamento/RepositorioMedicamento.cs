
using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleMedicamentos.ConsoleApp.MóduloMedicamento
{
    public class RepositorioMedicamento : RepositorioBase<Medicamento>
    {
        public void AtualizarQuantidadeMedicamento(Medicamento novoMedicamento)
        {
            var medicamento = registros.FirstOrDefault(x => x.Nome == novoMedicamento.Nome);
            medicamento.Quantidade += novoMedicamento.Quantidade;
        }

        public bool VerificarMedicamentoExiste(Medicamento novoMedicamento)
        {
            return registros.Any(x => x.Nome == novoMedicamento.Nome);
        }

        public List<Medicamento> VisualizarMedicamentosEmFalta()
        {
            return registros.FindAll(x => x.Quantidade <= 5);
        }
    }
}
