namespace Crud_Card.DAL.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string expiryDate { get; set; }
        public int cvv { get; set; }

    }
}
