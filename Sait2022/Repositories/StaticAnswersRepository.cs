using Sait2022.Domain.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sait2022.Repositories
{
    public class StaticAnswersRepository:IAnswersRepository
    {
        public static readonly ConcurrentDictionary<long,Answers> Answer = new ConcurrentDictionary<long,Answers>();

        public Answers GetById(long Id)
        {
            return !Answer.ContainsKey(Id) ? null : Answer[Id];
        }

        public void Upsert(Answers answers)
        {
            Answer[answers.Id] = answers;
        }

        public IEnumerable<Answers> GetAll { get { return Answer.Values; } }
    }
}
