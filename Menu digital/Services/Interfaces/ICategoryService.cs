using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using System.Collections.Generic;

namespace Menu_Digital.Services.Interfaces
{
    public interface ICategoryService
    {
        // 🔹 Devuelve todas las categorías globales
        List<CategoryDto> GetAllCategories();

        // 🔹 Devuelve una categoría por su ID
        CategoryDto GetCategoryById(int id);

        //  Crea una nueva categoría global (si no existe)
        CategoryDto Create(CreateAndUpdateCategoryDto request);

        // 🔹 Actualiza el nombre de una categoría existente
        CategoryDto Update(CreateAndUpdateCategoryDto updatedCategoryDto, int categoryId);

        // 🔹 Elimina una categoría global (si no tiene productos asociados)
        void Delete(int categoryId);
    }
}

