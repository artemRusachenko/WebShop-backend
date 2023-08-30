using AutoMapper;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.Models.Filters;
using lesson1_Simple_Functions___Controller.Responses;
using Newtonsoft.Json.Schema;
using System.Linq;

namespace lesson1_Simple_Functions___Controller.Services.ProductService
{
    public class ProductService : IProductService 
    {
        private readonly PostgreSqlContext sqlServerContext;
        private readonly IMapper mapper;

        public ProductService(PostgreSqlContext context, IMapper mapper)
        {
            sqlServerContext = context;
            this.mapper = mapper;
        }

        public async Task<PagedResponse<List<GetProductDto>>> GetProducts(PaginationFilter filter)
        {
            var data =  await sqlServerContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Skip((filter.PageNumber - 1) * filter.ProductCount)
                .Take(filter.ProductCount)
                .Select(p => mapper.Map<GetProductDto>(p)).ToListAsync();

            var totalProducts = await sqlServerContext.Products.CountAsync();

            return new PagedResponse<List<GetProductDto>>(data, filter.PageNumber, totalProducts);
        }

        public async Task<List<GetProductDto>> GetPopularProducts()
        {
            List<GetProductDto> products = await sqlServerContext.Products.Select(p => mapper.Map<GetProductDto>(p)).ToListAsync();
            products.Sort((prod1, prod2) => prod2.Popularity.CompareTo(prod1.Popularity));
            products.RemoveAt(products.Count>10 ? 10: products.Count-1);
            return products;
        }

        public async Task<GetProductDto?> GetProductById(int id)
        {
            var product = sqlServerContext.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if(product is null)
                return null;

            return mapper.Map<GetProductDto>(product);
        }

        public async Task<List<GetProductDto>> AddProduct(AddProductDto product)
        {
            var newProduct = mapper.Map<Product>(product);
            newProduct.Category = await sqlServerContext.Categories.FirstOrDefaultAsync(c => c.Id == product.CategoryId);
            sqlServerContext.Products.Add(newProduct);
            await sqlServerContext.SaveChangesAsync();
            return  sqlServerContext.Products.Select(p => mapper.Map<GetProductDto>(p)).ToList();
        }

        public async Task<GetProductDto?> UpdateProduct(UpdateProductDto updatedProduct)
        {
            var product = await sqlServerContext.Products.FindAsync(updatedProduct.Id);
            if(product == null)
                return null;

            product.Name = updatedProduct.Name == "" ? product.Name : updatedProduct.Name;
            product.Images = updatedProduct.Images == "" ? product.Images : updatedProduct.Images;
            product.Price = updatedProduct.Price == 0 ? product.Price : updatedProduct.Price;
            product.Description = updatedProduct.Description == "" ? product.Description : updatedProduct.Description;
            product.CategoryId = updatedProduct.CategoryId == 0 ? product.CategoryId : updatedProduct.CategoryId;

            await sqlServerContext.SaveChangesAsync();
            return mapper.Map<GetProductDto>(product);
        }

        public async Task<Dictionary<string, List<GetFilterDto>>> GetFilteredFilters(QueryParams filters)
        {
            Dictionary<string, List<GetFilterDto>> res = new();

/*            var brands = await sqlServerContext.Brands.Select(b => mapper.Map<GetFilterDto>(b)).ToListAsync();
            var colors = await sqlServerContext.Colors.Select(b => mapper.Map<GetFilterDto>(b)).ToListAsync();*/

            var products = await sqlServerContext.Products.ToListAsync();
            List<Product> test = new();
            if (filters.CategoryId != null)
            {
                var subCtgIds = await sqlServerContext.Categories.Where(c => c.ParentId.ToString() == filters.CategoryId).Select(p => p.Id).ToListAsync();
                products = products.Where(p => subCtgIds.Contains(p.CategoryId)).ToList();
                if(filters.Brands != null)
                {
                    var testBrandsIds = products.Select(p => p.BrandId).Distinct().ToList();
                    var testBrands = await sqlServerContext.Brands.Where(b => testBrandsIds.Contains(b.Id)).Select(b => mapper.Map<GetFilterDto>(b)).ToListAsync();
                    res.Add("brands", testBrands);
                }
                if(filters.Colors != null)
                {
                    var testColorsIds = products.Select(p => p.ColorId).Distinct().ToList();
                    var testColors = await sqlServerContext.Colors.Where(c => testColorsIds.Contains(c.Id)).Select(c => mapper.Map<GetFilterDto>(c)).ToListAsync();
                    res.Add("colors", testColors);
                }
            }
            if (filters.SubCategoryId != null)
            {
                products = products.Where(p => filters.SubCategoryId.Contains(p.CategoryId.ToString())).ToList();
                /*    if (filters.Brands != null)
                {
                    var testBrandsIds = products.Select(p => p.BrandId).Distinct().ToList();
                    var testBrands = await sqlServerContext.Brands.Where(b => testBrandsIds.Contains(b.Id)).Select(b => mapper.Map<GetFilterDto>(b)).ToListAsync();
                    res.Add("brands", testBrands);
                }
                if (filters.Colors != null)
                {
                    var testColorsIds = products.Select(p => p.ColorId).Distinct().ToList();
                    var testColors = await sqlServerContext.Colors.Where(c => testColorsIds.Contains(c.Id)).Select(c => mapper.Map<GetFilterDto>(c)).ToListAsync();
                    res.Add("colors", testColors);
                }*/
            }
            if (filters.Brands != null)
            {
                products = products.Where(p => filters.Brands.Contains(p.BrandId.ToString())).ToList();
            }
            if (filters.Colors != null)
            {
                products = products.Where(p => filters.Colors.Contains(p.ColorId.ToString())).ToList();
            }

            var brandsIds = products.Select(p => p.BrandId).Distinct().ToList();
            var brands = await sqlServerContext.Brands.Where(b => brandsIds.Contains(b.Id)).Select(b => mapper.Map<GetFilterDto>(b)).ToListAsync();

            var colorsIds = products.Select(p => p.ColorId).Distinct().ToList();
            var colors = await sqlServerContext.Colors.Where(c => colorsIds.Contains(c.Id)).Select(c => mapper.Map<GetFilterDto>(c)).ToListAsync();

            if (!res.ContainsKey("brands"))
            {

                res.Add("brands", brands);
            }

            if (!res.ContainsKey("colors"))
            {
                
                res.Add("colors", colors);
            }
            
            return res;
        }

        public async Task<PagedResponse<List<GetProductDto>>> GetFilteredProducts(QueryParams filters/*, PaginationFilter pFilters*/)
        {
             List<Product> products = await sqlServerContext.Products.ToListAsync();
            if(filters.SubCategoryId != null)
            {
                var subCategoriesIds = filters.SubCategoryId.Split(',').ToList();
                if(subCategoriesIds.Count > 0)
                {
                    products = products.Where(p => subCategoriesIds.Contains(p.CategoryId.ToString())).ToList();
                }
            }
            if(filters.CategoryId != null)
            {
                var subCategoriesIds = await sqlServerContext.Categories.Where(c => c.ParentId.ToString() == filters.CategoryId).Select(c => c.Id).ToListAsync();
                if (subCategoriesIds.Count > 0) 
                {
                    products = products.Where(p => subCategoriesIds.Contains(p.CategoryId)).ToList();
                }
                else
                {
                    products = products.Where(p => p.CategoryId.ToString() == filters.CategoryId).ToList();
                }
            }
            if(filters.Name!= null)
            {
                products = products.Where(p => p.Name == filters.Name).ToList();
            }
            if(filters.SortingMethod != null)
            {
                if(filters.SortingMethod == "LowToHigh")
                {
                    products.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));
                }
                else if (filters.SortingMethod == "HighToLow")
                {
                    products.Sort((p1, p2) => p2.Price.CompareTo(p1.Price));
                }
                else if(filters.SortingMethod == "BestSellers")
                {
                    products.Sort((prod1, prod2) => prod2.Popularity.CompareTo(prod1.Popularity));
                }
            }
            if(filters.Brands != null)
            {
                var brandsIds = filters.Brands.Split(',').ToList();
                if (brandsIds.Count > 0)
                {
                    products = products.Where(p => brandsIds.Contains(p.BrandId.ToString())).ToList();
                }
            }
            if (filters.Colors != null)
            {
                var colorsIds = filters.Colors.Split(',').ToList();
                if (colorsIds.Count > 0)
                {
                    products = products.Where(p => colorsIds.Contains(p.ColorId.ToString())).ToList();
                }
            }
            if (filters.PriceRange != null)
            {
                string[] priceRange = filters.PriceRange.Split('-');
                if(priceRange.Length == 2)
                {
                    products = products.Where(p => p.Price > decimal.Parse(priceRange[0]) && p.Price <= decimal.Parse(priceRange[1])).ToList();
                }
            }

            var data = products
                .Skip((filters.PageNumber - 1) * filters.ProductCount)
                .Take((filters.ProductCount))
                .Select(mapper.Map<GetProductDto>).ToList();

            var totalProducts = products.Count;

            return new PagedResponse<List<GetProductDto>>(data, filters.PageNumber, totalProducts);
        }

        


            /*public async Task<List<Product>?> DeleteProduct(int id)
            {
                var product = await sqlServerContext.Products.FindAsync(id);
                if(product == null)
                {
                    return null;
                }

                sqlServerContext.Products.Remove(product);
                await sqlServerContext.SaveChangesAsync();
                return await sqlServerContext.Products.ToListAsync();
            }*/
        }
}
