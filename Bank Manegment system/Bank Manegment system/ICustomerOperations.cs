namespace Bank_Manegment_system
{
    interface ICustomerOperations
    {
        void Deposit();
        void Withdraw();
        void DisplayBalance();
        void ShowTransactions();
        void CreateCreditCard();
        void MakePurchase();
        void RepayDebt();
        void ShowCreditInfo();
    }
}
