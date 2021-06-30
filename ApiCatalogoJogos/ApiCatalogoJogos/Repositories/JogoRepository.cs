using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("e12f4b10-91b1-4ce2-85be-d7d4131af9a8"), new Jogo{Id = Guid.Parse("e12f4b10-91b1-4ce2-85be-d7d4131af9a8"), Nome="Fifa 21", Produtora = "EA", Preco = 200 }},
            {Guid.Parse("494b4f1f-1536-458b-8a9e-d3feb5b9bf34"), new Jogo{Id = Guid.Parse("494b4f1f-1536-458b-8a9e-d3feb5b9bf34"), Nome="Fifa 20", Produtora = "EA", Preco = 190 }},
            {Guid.Parse("9edda99d-38c1-46c0-a85f-0b7474ac2bf6"), new Jogo{Id = Guid.Parse("9edda99d-38c1-46c0-a85f-0b7474ac2bf6"), Nome="Fifa 19", Produtora = "EA", Preco = 180 }},
            {Guid.Parse("78aad07f-94b3-44b3-ae5b-d8e7607b0333"), new Jogo{Id = Guid.Parse("78aad07f-94b3-44b3-ae5b-d8e7607b0333"), Nome="Fifa 18", Produtora = "EA", Preco = 170 }},
            {Guid.Parse("cd5e6bdf-07ff-4835-9f9c-b7afd84a5c2e"), new Jogo{Id = Guid.Parse("cd5e6bdf-07ff-4835-9f9c-b7afd84a5c2e"), Nome="Street Fighter V", Produtora = "Capcom", Preco = 80 }},
            {Guid.Parse("8d4e0ed6-b366-4363-b76d-bb8cad643ddf"), new Jogo{Id = Guid.Parse("8d4e0ed6-b366-4363-b76d-bb8cad643ddf"), Nome="Frostpunk", Produtora = "B11", Preco = 190 }}
        };

        public Task<List<Jogo>> ObtemJogos(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> ObtemJogoPorId(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> ObtemJogo(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task InsereJogo(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task AtualizaJogo(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task ExcluiJogo(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //fechar conexão com o banco
        }
    }
}
