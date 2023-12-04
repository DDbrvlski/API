namespace BookStoreAPI.Helpers
{
    public class BaseOperations
    {
        public static decimal CalculateBruttoPrice(decimal priceNetto, decimal vat)
        {
            if (priceNetto < 0 || vat < 0)
            {
                throw new ArgumentException("Cena netto i stawka VAT muszą być nieujemne.");
            }

            return priceNetto * (1 + vat / 100);
        }
    }
}
