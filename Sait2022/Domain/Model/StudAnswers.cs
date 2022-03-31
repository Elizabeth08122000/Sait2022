using Sait2022.Domain.Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sait2022.Domain.Model
{
    public class StudAnswers:Entity
    {
        

        public Questions Questions { get; set; }

    }
}
