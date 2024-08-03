namespace PEMS.Domain
{
    public class PredictValidate : BaseValidate<Predict>, IPredictValidate
    {
        public PredictValidate(IPredictRepository predictRepository) : base(predictRepository)
        {
        }
    }
}
