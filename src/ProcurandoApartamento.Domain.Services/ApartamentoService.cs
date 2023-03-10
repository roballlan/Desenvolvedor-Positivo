using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using ProcurandoApartamento.Domain.Services.Interfaces;
using ProcurandoApartamento.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProcurandoApartamento.Domain.Services
{
    public class ApartamentoService : IApartamentoService
    {
        protected readonly IApartamentoRepository _apartamentoRepository;

        public ApartamentoService(IApartamentoRepository apartamentoRepository)
        {
            _apartamentoRepository = apartamentoRepository;
        }

        public virtual async Task<Apartamento> Save(Apartamento apartamento)
        {
            await _apartamentoRepository.CreateOrUpdateAsync(apartamento);
            await _apartamentoRepository.SaveChangesAsync();
            return apartamento;
        }

        public virtual async Task<IPage<Apartamento>> FindAll(IPageable pageable)
        {
            var page = await _apartamentoRepository.QueryHelper()
                .GetPageAsync(pageable);
            return page;
        }

        public virtual async Task<Apartamento> FindOne(long id)
        {
            var result = await _apartamentoRepository.QueryHelper()
                .GetOneAsync(apartamento => apartamento.Id == id);
            return result;
        }

        public virtual async Task Delete(long id)
        {
            await _apartamentoRepository.DeleteByIdAsync(id);
            await _apartamentoRepository.SaveChangesAsync();
        }

        public virtual async Task<string> GetMelhorApartamento(string[] estabelecimentos)
        {
            var listApartamentos = await _apartamentoRepository.QueryHelper()
                .GetAllAsync(apartamento => apartamento.EstabelecimentoExiste && apartamento.ApartamentoDisponivel && estabelecimentos.Contains(apartamento.Estabelecimento));

            listApartamentos.OrderByDescending(apartamento => apartamento.Quadra);

            var ret = listApartamentos.GroupBy(a => a.Quadra).ToList();

            var qtdeImoveis = 0;
            var melhorQuadra = 0;
            foreach(var item in ret)
            {
                if(item.Count() >= qtdeImoveis)
                {
                    melhorQuadra = item.Key;
                    qtdeImoveis = item.Count();
                }
            }
            return  string.Format("QUADRA: {0}",melhorQuadra.ToString());
        }
    }
}
