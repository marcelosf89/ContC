using ContC.crosscutting.Exceptions;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.domain.services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ContC.tests.Services
{
    [TestClass]
    public class TestFuncionarioBuilder
    {
        [TestInitialize]
        public void SetUp()
        {
            _funcionarioRepository = new Mock<IFuncionarioRepository>();
            _empresaRepository = new Mock<IEmpresaRepository>();
            _funcionarioBuilder = new FuncionarioBuilder(_funcionarioRepository.Object, _empresaRepository.Object);
        }

        [TestMethod]
        public void QuandoRegistrarNome_DeveConstruirFuncionarioComNome()
        {
            string expected = "Mauro";
            Funcionario funcionario = 
            _funcionarioBuilder
                    .SetNome(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Nome);
        }

        [TestMethod]
        public void QuandoRegistrarEmail_DeveConstruirFuncionarioComEmail()
        {
            string expected = "abc@def.org";
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetEmail(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Email);
        }

        [TestMethod]
        public void QuandoRegistrarTelefone_DeveConstruirFuncionarioComTelefone()
        {
            string expected = "99999999";
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetTelefone(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Telefone);
        }

        [TestMethod]
        public void QuandoRegistrarNascimento_DeveConstruirFuncionarioComNascimento()
        {
            DateTime expected = DateTime.Now;
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetNascimento(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Nascimento);
        }

        [TestMethod]
        public void QuandoRegistrarIdentificacao1_DeveConstruirFuncionarioComIdentificacao1()
        {
            string expected = "aaaa";
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetIdentificacao1(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Identificacao1);
        }

        [TestMethod]
        public void QuandoRegistrarIdentificacao2_DeveConstruirFuncionarioComIdentificacao2()
        {
            string expected = "aaaa";
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetIdentificacao2(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Identificacao2);
        }

        [TestMethod]
        public void QuandoRegistrarEmpresa_DeveConstruirFuncionarioComEmpresa()
        {
            int expected = 9;
            _empresaRepository.Setup(p => p.Find(expected)).Returns(new Empresa { Id = expected });
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetEmpresa(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Empresas[0].Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ConstrucaoObjetoException))]
        public void QuandoRegistrarEmpresaENaoEncontrar_DeveDispararExcecao()
        {
            int expected = 9;
            _empresaRepository.Setup(p => p.Find(expected)).Returns(null as Empresa);
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetEmpresa(expected)
                    .Build();
        }

        [TestMethod]
        public void QuandoRegistrarLider_DeveConstruirFuncionarioComLider()
        {
            int expected = 99;
            _funcionarioRepository.Setup(p => p.Find(expected)).Returns(new Funcionario{ Id = expected});
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetLider(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Lider.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ConstrucaoObjetoException))]
        public void QuandoRegistrarLiderENaoEncontrar_DeveDispararExcecao()
        {
            int expected = 9;
            _funcionarioRepository.Setup(p => p.Find(expected)).Returns(null as Funcionario);
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetLider(expected)
                    .Build();
        }

        [TestMethod]
        public void QuandoRegistrarTipoPagamento_DeveConstruirFuncionarioComTipoPagamento()
        {
            int expected = 999;
            _funcionarioRepository.Setup(p => p.GetTipoPagamento(expected)).Returns(new TipoPagamento { Id = expected });
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetTipoPagamento(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.TipoPagamento.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ConstrucaoObjetoException))]
        public void QuandoRegistrarTipoPagamentoENaoEncontrar_DeveDispararExcecao()
        {
            int expected = 9;
            _funcionarioRepository.Setup(p => p.GetTipoPagamento(expected)).Returns(null as TipoPagamento);
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetTipoPagamento(expected)
                    .Build();
        }

        [TestMethod]
        public void QuandoRegistrarTipoRegime_DeveConstruirFuncionarioComTipoRegime()
        {
            int expected = 999;
            _funcionarioRepository.Setup(p => p.GetRegime(expected)).Returns(new TipoRegimeFuncionario { Id = expected });
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetTipoRegime(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.TipoRegimeFuncionario.Id);
        }


        [TestMethod]
        [ExpectedException(typeof(ConstrucaoObjetoException))]
        public void QuandoRegistrarTipoRegimeENaoEncontrar_DeveDispararExcecao()
        {
            int expected = 9;
            _funcionarioRepository.Setup(p => p.GetRegime(expected)).Returns(null as TipoRegimeFuncionario);
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetTipoRegime(expected)
                    .Build();
        }

        [TestMethod]
        public void QuandoRegistrarValor_DeveConstruirFuncionarioComValor()
        {
            int expected = 1001;
            Funcionario funcionario =
            _funcionarioBuilder
                    .SetValor(expected)
                    .Build();

            Assert.AreEqual(expected, funcionario.Valor);
        }

        private FuncionarioBuilder _funcionarioBuilder;

        private Mock<IFuncionarioRepository> _funcionarioRepository;

        private Mock<IEmpresaRepository> _empresaRepository;
    }
}
