using Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Codenation.Challenge
{
    public class FIFACupStats
    {       
        public string CSVFilePath { get; set; } = "data.csv";
        public List<Jogador> Jogadores { get; set; } = new List<Jogador>();

        public Encoding CSVEncoding { get; set; } = Encoding.UTF8;
        public FIFACupStats()
        {
            StreamReader rd = new StreamReader(CSVFilePath);
            string linha = null;
            rd.ReadLine();
            var i = 0;
            while ((linha = rd.ReadLine()) != null)
            {
                ++i;
                var palavras = linha.Split(',');

                try {
                    var jogador = new Jogador();
                    jogador.Nacionalidade = palavras[14];
                    jogador.Club = palavras[3];
                    jogador.Nome = palavras[2];
                    jogador.Rescisao = string.IsNullOrEmpty(palavras[18]) 
                        ? 0 : Convert.ToDouble(palavras[18].Replace(".", ","));
                    jogador.Nascimento = Convert.ToDateTime(palavras[8]);
                    jogador.Salario = Convert.ToDouble(palavras[17]);
                    jogador.Idade = Convert.ToInt32(palavras[6]);
                    Jogadores.Add(jogador);
                } catch (Exception ex) {
                    throw new Exception($"Erro ao criar jogador linha {i}, {palavras[18]}", ex);
                }
            }
        }

        public int NationalityDistinctCount()
        {
            var nacionalidades = new List<string>();
            foreach (var jogador in Jogadores)
            {
                if (!string.IsNullOrEmpty(jogador.Nacionalidade) && !nacionalidades.Contains(jogador.Nacionalidade))
                {
                    nacionalidades.Add(jogador.Nacionalidade);
                }
            }
            return nacionalidades.Count;
        }

        public int ClubDistinctCount()
        {
            var clubes = new List<string>();
            foreach (var jogador in Jogadores)
            {
                if (!string.IsNullOrEmpty(jogador.Club) && !clubes.Contains(jogador.Club))
                {
                    clubes.Add(jogador.Club);
                }
            }
            return clubes.Count;
        }

        public List<string> First20Players()
        {
            var nomes = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                var jogador = Jogadores.ElementAt(i);
                nomes.Add(jogador.Nome);
            }
            return nomes;
        }

        public List<string> Top10PlayersByReleaseClause()
        {
            var topPlayers = Jogadores.OrderByDescending(x => x.Rescisao)
                .Select(x => x.Nome)
                .Take(10)
                .ToList();
            return topPlayers;
        }
        
        public List<string> Top10PlayersByAge()
        {
            var top10 = Jogadores.OrderBy(x => x.Nascimento)
                .ThenBy(x => x.Salario)
                .Select(x => x.Nome)
                .Take(10)
                .ToList();
            return top10;
        }

        public Dictionary<int, int> AgeCountMap()
        {
            return Jogadores
                .GroupBy(x => x.Idade)
                .ToDictionary(x => x.Key, group => group.Count());
        }
    }
}
