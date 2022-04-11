using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class TelaCadastroRequisicao : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioRequisicao _repositorioRequisicao;
        private readonly RepositorioPaciente _repositorioPaciente;
        private readonly RepositorioMedicamento _repositorioMedicamento;
        private readonly TelaCadastroPaciente _telaCadastroPaciente;
        private readonly TelaCadastroMedicamento _telaCadastroMedicamento;
        private readonly Notificador _notificador;


        public TelaCadastroRequisicao(RepositorioRequisicao repositorioRequisicao, RepositorioPaciente repositorioPaciente, RepositorioMedicamento repositorioMedicamento, TelaCadastroPaciente telaCadastroPaciente, TelaCadastroMedicamento telaCadastroMedicamento, Notificador notificador) :
            base("Cadastro de Requisição")
        {
            _repositorioRequisicao = repositorioRequisicao;
            _repositorioPaciente = repositorioPaciente;
            _repositorioMedicamento = repositorioMedicamento;
            _telaCadastroPaciente = telaCadastroPaciente;
            _telaCadastroMedicamento = telaCadastroMedicamento;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Requisição");

            Requisicao novaRequisicao = ObterRequisicao();
            if (novaRequisicao != null)
            {
                _repositorioRequisicao.Inserir(novaRequisicao);

                _notificador.ApresentarMensagem("Requisição cadastrado com sucesso!", TipoMensagem.Sucesso);
            }



        }

        public void Editar()
        {
            MostrarTitulo("Editando Requição");

            bool temRequisicaoCadastrada = VisualizarRegistros("Pesquisando");

            if (temRequisicaoCadastrada == false)
            {
                _notificador.ApresentarMensagem("Nenhuma requisicao cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            Requisicao requisicaoAtualizada = ObterRequisicao();

            bool conseguiuEditar = _repositorioRequisicao.Editar(numeroRequisicao, requisicaoAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Requisição editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Requisicao");

            bool temRequisicaoCadastrada = VisualizarRegistros("Pesquisando");

            if (temRequisicaoCadastrada == false)
            {
                _notificador.ApresentarMensagem("Nenhuma requisição cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioRequisicao.Excluir(numeroRequisicao);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Requisição excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Requisições");

            List<Requisicao> requisicoes = _repositorioRequisicao.SelecionarTodos();

            if (requisicoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma requisição disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao requisicao in requisicoes)
                Console.WriteLine(requisicao.ToString());

            Console.ReadLine();

            return true;
        }

        private Requisicao ObterRequisicao()
        {
            Paciente pacienteSelecionado = ObtemPaciente();

            if (pacienteSelecionado != null)
            {
                Medicamento medicamentoSelecionado = ObtemMedicamento();

                if (medicamentoSelecionado != null)
                {
                    Console.WriteLine("Informe a quantidade de medicamento: ");
                    int quantidade = int.Parse(Console.ReadLine());

                    while (medicamentoSelecionado.Quantidade < quantidade)
                    {
                        _notificador.ApresentarMensagem("Quantidade máx disponivel: " + medicamentoSelecionado.Quantidade, TipoMensagem.Erro);
                        Console.WriteLine("Informe a quantidade de medicamento: ");
                        quantidade = int.Parse(Console.ReadLine());

                    }

                    //atualiza a quantidade de medicamentos
                    medicamentoSelecionado.Quantidade -= quantidade;

                    string status = "Aprovada";

                    Console.WriteLine("Informe da Data da Requisição (DD/MM/AAA):");
                    DateTime data = Convert.ToDateTime(Console.ReadLine());

                    return new Requisicao(pacienteSelecionado, medicamentoSelecionado, quantidade, status, data);

                }


            }

            return null;

        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da requisição: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioRequisicao.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da requisição não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        private Paciente ObtemPaciente()
        {
            bool temPacientes = _telaCadastroPaciente.VisualizarRegistros("Tela");


            if (!temPacientes)
            {
                return null;
            }

            Console.Write("Digite o ID do paciente: ");
            int numeroPaciente = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Paciente pacienteSelecionado = _repositorioPaciente.SelecionarRegistro(x => x.id == numeroPaciente);

            return pacienteSelecionado;
        }

        private Medicamento ObtemMedicamento()
        {
            bool temMedicamentos = _telaCadastroMedicamento.VisualizarRegistros("Tela");


            if (!temMedicamentos)
            {
                return null;
            }

            Console.Write("Digite o ID do medicamento: ");
            int numeroMedicamento = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Medicamento medicamentoSelecionado = _repositorioMedicamento.SelecionarRegistro(x => x.id == numeroMedicamento);

            return medicamentoSelecionado;
        }


    }
}
