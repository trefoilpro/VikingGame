namespace DefaultNamespace
{
    public interface ICharacter
    {
        int Health { get; set; }
        int Damage { get; set; }
        void TakeDamage(int damage);
    }
}