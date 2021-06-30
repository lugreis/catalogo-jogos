using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> ObtemJogos(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.ObtemJogos(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> ObtemJogoPorId(Guid id)
        {
            var jogo = await _jogoRepository.ObtemJogoPorId(id);

            if (jogo is null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> InsereJogo(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.ObtemJogo(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepository.InsereJogo(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task AtualizaJogo(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.ObtemJogoPorId(id);

            if (entidadeJogo is null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepository.AtualizaJogo(entidadeJogo);
        }

        public async Task AtualizaPrecoJogo(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.ObtemJogoPorId(id);

            if (entidadeJogo is null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _jogoRepository.AtualizaJogo(entidadeJogo);
        }

        public async Task ExcluiJogo(Guid id)
        {
            var jogo = await _jogoRepository.ObtemJogoPorId(id);

            if (jogo is null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.ExcluiJogo(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
