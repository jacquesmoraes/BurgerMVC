namespace BurgerMVC.Models
{
    public class FileManagerModel
    {
        public FileInfo[] Files { get; set; }
        public IFormFile FormFile { get; set; }
        public List<IFormFile> FormFiles { get; set; } = new();
        public string PathImageProduto { get; set; }
    }
}
