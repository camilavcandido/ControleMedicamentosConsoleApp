using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class Medicamento : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _descricao;
        private  int _quantidade;

        public Medicamento(string nome, string descricao, int quantidade)
        {
            _nome = nome;
            _descricao = descricao;
            _quantidade = quantidade;
        }

        public string Nome => _nome;

        public string Descricao => _descricao;

        public int Quantidade
        {
            get => _quantidade;
            set => _quantidade = value;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Descrição: " + Descricao + Environment.NewLine +
                "Quantidade: " + Quantidade + Environment.NewLine;
        }

    }
}
