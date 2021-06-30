using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<Jogo>> ObtemJogos(int pagina, int quantidade);
        Task<Jogo> ObtemJogoPorId(Guid id);
        Task<List<Jogo>> ObtemJogo(string nome, string produtora);
        Task InsereJogo(Jogo jogo);
        Task AtualizaJogo(Jogo jogo);
        Task ExcluiJogo(Guid id);
    }
}
