namespace Shopping.Core.Services
{
    public interface IPaymentService
    {
        void Charge(int memberId, decimal totalPayable);
    }
}
