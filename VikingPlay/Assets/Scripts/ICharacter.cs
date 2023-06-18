namespace DefaultNamespace
{
    public interface ICharacter
    {
        int MaxHealth { get; set; }
        int Damage { get; set; }
        void TakeDamage(int damage);
    }
}