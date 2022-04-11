using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _telefone;
        private readonly string _cpf;
        public Paciente(string nome, string telefone, string cpf)
        {
            _nome = nome;
            _telefone = telefone;
            _cpf = cpf;
        }

        public string Nome => _nome;

        public string Telefone => _telefone;

        public string Cpf => _cpf;

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Telefone: " + Telefone + Environment.NewLine +
                "CPF: " + Cpf + Environment.NewLine;


        }
    }
}
