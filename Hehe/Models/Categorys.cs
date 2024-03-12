using System.ComponentModel.DataAnnotations;

namespace Hehe.Models
{
    public class Categorys
    {
        [Key]
        [Display(Name = "ID")]
        public int Category_ID { get; set; }
        [Display(Name = "Ten the loai")]
        public string Category_Name { get; set; }
        [Display(Name = "Ghi chu")]
        public string Category_Note { get; set; }
        [Display (Name = "Trang thai")]
        public bool Category_Status { get; set; }

    }

    public class CategorysDB 
    {
        static List<Categorys> CategorysListDefault = new List<Categorys>
        {
            new Categorys() { Category_ID = 1, Category_Name = "GD", Category_Note = "", Category_Status = true},
            new Categorys() { Category_ID = 2, Category_Name = "CT", Category_Note = "", Category_Status = true},
            new Categorys() { Category_ID = 3, Category_Name = "VH", Category_Note = "", Category_Status = true},
        };
        public List<Categorys> ListAll()
        {
            var categoryList = CategorysListDefault;
            return categoryList.ToList();
        }
        public Categorys GetByID(int id)
        {
            return CategorysListDefault.Where(c => c.Category_ID == id).FirstOrDefault();
        }
        public void Add(Categorys category)
        {
            CategorysListDefault.Add(category);
        }
        public void Update(Categorys category)
        {
            var categories = CategorysListDefault.SingleOrDefault(c => c.Category_ID == category.Category_ID);
            categories.Category_Name = category.Category_Name;
            categories.Category_Note = category.Category_Note;
            categories.Category_Status = category.Category_Status;
        }
        public void Remove(int id)
        {
            var categories = CategorysListDefault.SingleOrDefault(c => c.Category_ID == id);
            CategorysListDefault.Remove(categories);
        }
    }
}
 
