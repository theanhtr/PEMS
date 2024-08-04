using System.ComponentModel.DataAnnotations;

namespace PEMS.Domain
{
    public class PredictFilterResult
    {
        public IEnumerable<Predict> Predicts { get; set; }

        public int Total { get; set; }
    }
}
