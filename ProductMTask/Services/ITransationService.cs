using ProductMTask.Dtos;

namespace ProductMTask.Services
{
    public interface ITransationService
    {
        Task Create(TransationDto dto);
        Task<List<TransactionViewModel>> GetTransations(DateTime? from, DateTime? to);
    }
}
