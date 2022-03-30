using System;
using System.Collections.Generic;
using Sait2022.Domain.Model;

namespace Sait2022.Repositories
{
    public interface IAnswersRepository
    {
        Answers GetById (long id);
        void Upsert(Answers answer);
        IEnumerable<Answers> GetAll { get; }
    }
}
