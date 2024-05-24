namespace EbisconDemo.Services.Models.Configuration
{
    public class ProductSources
    {
        public string[] SynchronizationQueue { get; set; }
        public Dictionary<string, string> Sources { get; set; }
    }
}
