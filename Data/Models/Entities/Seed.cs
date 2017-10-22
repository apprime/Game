using Data.Models.Entities.EntityInterfaces;
using Data.Models.Exceptions;
using Data.Repositories;

namespace Data.Models.Entities
{
    public class Seed
    {
        private char v;

        public Seed(char prefix, int id)
        {
            Prefix = prefix;
            Id = id;
        }

        public char Prefix { get; set; }
        public int Id { get; set; }

        public static Seed Monster(int id)
        {
            return new Seed('M', id);
        }

        public IEntity Hydrate()
        {
            switch (Id)
            {
                case 'M':
                    var repo = new MonsterRepository();
                    return repo.Get(this);
                default:
                    throw new TodoException("We need a nicer way to hydrate seeds");
            }
        }
    }
}
