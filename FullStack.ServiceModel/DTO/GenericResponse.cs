namespace FullStack.ServiceModel
{
    public class GenericResponse
    {
        public bool IsSuccessful { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
    }
}
