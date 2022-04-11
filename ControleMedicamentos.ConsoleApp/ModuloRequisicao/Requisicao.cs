using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using System;


namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class Requisicao : EntidadeBase
    {
        private  Paciente _paciente;
        private  Medicamento _medicamento;
        private int _quantidade;
        private string _status;
        private DateTime _data;

        public Requisicao(Paciente paciente, Medicamento medicamento, int quantidade, string status, DateTime data)
        {
            Paciente = paciente;
            Medicamento = medicamento;
            Quantidade = quantidade;
            Status = status;
            Data = data;
        }

        public Paciente Paciente { get => _paciente; set => _paciente = value; }
        public Medicamento Medicamento { get => _medicamento; set => _medicamento = value; }
        public int Quantidade { get => _quantidade; set => _quantidade = value; }
        public string Status { get => _status; set => _status = value; }
        public DateTime Data { get => _data; set => _data = value; }

        public override string ToString()
        {
            return "Id requisição: " + id + Environment.NewLine +
                "Id Paciente: " + Paciente.id + Environment.NewLine +
                "CPF Paciente: " + Paciente.Cpf + Environment.NewLine +
                "Id Medicamento: " + Medicamento.id + Environment.NewLine +
                "Nome Medicamento: " + Medicamento.Nome + Environment.NewLine +
                "Status: " + Status + Environment.NewLine +
                "Data: " + Data + Environment.NewLine;
        }
    }
}
