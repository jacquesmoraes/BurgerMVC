﻿namespace BurgerMVC.Models
{
    public class FileManagerModel
    {
        public FileInfo[] Files { get; set; }
        public IFormFile FormFile { get; set; }
        public List<IFormFile> FormFiles { get; set; }
        public string PathImageProduto { get; set; }
    }
}
