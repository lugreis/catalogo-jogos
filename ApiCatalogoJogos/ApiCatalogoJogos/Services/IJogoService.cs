using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> ObtemJogos(int pagina, int quantidade);
        Task<JogoViewModel> ObtemJogoPorId(Guid id);
        Task<JogoViewModel> InsereJogo(JogoInputModel jogo);
        Task AtualizaJogo(Guid id, JogoInputModel jogo);
        Task AtualizaPrecoJogo(Guid id, double preco);
        Task ExcluiJogo(Guid id);
    }
}
