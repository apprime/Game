using Data.Models.Entities.EntityInterfaces;
using Data.Models.Exceptions;
using Data.Models.Nodes;
using Data.Repositories;

namespace Data.Models.Entities
{
    public class Seed
    {
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

        //public IEntity Hydrate(Position position)
        //{
        //    switch (Prefix)
        //    {
        //        case 'M':
        //            var repo = new MonsterRepository();
        //            return repo.Get(this, position);
        //        default:
        //            throw new TodoException("We need a nicer way to hydrate seeds");
        //    }
        //}
    }
}
