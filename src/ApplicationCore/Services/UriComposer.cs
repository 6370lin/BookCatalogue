using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class UriComposer : IUriComposer
    {
        public string ComposePicUri(string uriTemplate)
        {
            return uriTemplate.Replace("http://catalogbaseurltobereplaced", "");
        }
    }
}
