namespace OShop.Domain.Entities
{
    public abstract class BaseEntity /*: IBaseEntity*/
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
