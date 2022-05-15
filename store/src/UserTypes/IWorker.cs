namespace store.UserTypes
{
    
    public interface IWorker
    {
        void SetDiscount(int itemCode, float precents = 10);
        int GetSalary(string WorkerId = null); 

    }
}
