namespace BL.Interfaces
{
    public interface ITullService
    {
        Task<decimal> BeraknaTullViaAPI(decimal orderSumma, string valtLand);
    }
}
