namespace airbnb;

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Clase para encapsular opciones de filtrado múltiples.
/// </summary>
public class FilterOptions
{
    public string SearchText { get; set; } = string.Empty;
    public bool PetFriendly { get; set; } = false;
    public string? PropertyType { get; set; } = null;
    public decimal? MinPrice { get; set; } = null;
    public decimal? MaxPrice { get; set; } = null;
    public int? MinBedrooms { get; set; } = null;
    public int? MaxBedrooms { get; set; } = null;
    public decimal? MinRating { get; set; } = null;
}

/// <summary>
/// Clase de soporte para procesar y filtrar datos JSON de forma dinámica.
/// Separa la lógica de negocio de la interfaz gráfica.
/// Soporta múltiples opciones de filtrado avanzado.
/// </summary>
internal class JsonDataProcessor
{
    private List<Dictionary<string, object?>> _originalData = new();
    private List<Dictionary<string, object?>> _filteredData = new();
    private HashSet<string> _availableColumns = new();
    private HashSet<string> _availablePropertyTypes = new();

    /// <summary>
    /// Propiedades dinámicas detectadas en el JSON.
    /// </summary>
    public IEnumerable<string> AvailableColumns => _availableColumns;

    /// <summary>
    /// Tipos de propiedad disponibles en los datos.
    /// </summary>
    public IEnumerable<string> AvailablePropertyTypes => _availablePropertyTypes.OrderBy(x => x);

    /// <summary>
    /// Datos actualmente filtrados disponibles para mostrar.
    /// </summary>
    public List<Dictionary<string, object?>> FilteredData => _filteredData;

    /// <summary>
    /// Carga y deserializa un archivo JSON de forma flexible.
    /// Normaliza la estructura para trabajar con arrays de objetos.
    /// Soporta múltiples encodings y estructuras JSON.
    /// </summary>
    /// <param name="filePath">Ruta del archivo JSON (puede ser relativa o absoluta).</param>
    /// <returns>true si la carga fue exitosa; false en caso contrario.</returns>
    public bool LoadFromJson(string filePath)
    {
        try
        {
            // Obtener ruta absoluta
            var absolutePath = Path.IsPathRooted(filePath) 
                ? filePath 
                : Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (!File.Exists(absolutePath))
            {
                System.Diagnostics.Debug.WriteLine($"Archivo no encontrado: {absolutePath}");
                return false;
            }

            // Leer con detección automática de encoding
            string json = File.ReadAllText(absolutePath, System.Text.Encoding.UTF8);

            if (string.IsNullOrWhiteSpace(json))
            {
                System.Diagnostics.Debug.WriteLine("El archivo JSON está vacío.");
                return false;
            }

            var token = JToken.Parse(json);

            // Normalizar: convertir a array si es un objeto único
            JArray dataArray = token switch
            {
                JArray arr => arr,
                JObject obj => new JArray(obj),
                _ => throw new InvalidOperationException("JSON debe ser un array u objeto.")
            };

            if (dataArray.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("El array JSON está vacío.");
                return false;
            }

            // Convertir JArray a lista de diccionarios con configuración especial de JSON
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            _originalData = dataArray
                .Cast<JObject>()
                .Select(obj => obj.ToObject<Dictionary<string, object?>>(Newtonsoft.Json.JsonSerializer.Create(settings)) ?? new())
                .ToList();

            if (_originalData.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No se pudieron deserializar los objetos JSON.");
                return false;
            }

            // Detectar todas las columnas disponibles (propiedades únicas)
            _availableColumns.Clear();
            _availablePropertyTypes.Clear();

            foreach (var row in _originalData)
            {
                foreach (var key in row.Keys)
                {
                    _availableColumns.Add(key);
                }

                // Detectar tipos de propiedad disponibles (flexible, busca en diferentes columnas)
                var propType = row.Values.FirstOrDefault(v => 
                    v?.ToString()?.Contains("property") == true || 
                    v?.ToString()?.Contains("type") == true);

                if (propType != null && !string.IsNullOrWhiteSpace(propType.ToString()))
                {
                    _availablePropertyTypes.Add(propType.ToString() ?? "");
                }

                // También busca específicamente en "property_type"
                if (row.TryGetValue("property_type", out var specificPropType) && specificPropType != null)
                {
                    _availablePropertyTypes.Add(specificPropType.ToString() ?? "");
                }
            }

            _filteredData = new List<Dictionary<string, object?>>(_originalData);

            System.Diagnostics.Debug.WriteLine(
                $"JSON cargado exitosamente: {_originalData.Count} registros, " +
                $"{_availableColumns.Count} columnas");

            return true;
        }
        catch (Newtonsoft.Json.JsonException ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error de formato JSON: {ex.Message}");
            return false;
        }
        catch (System.Text.Json.JsonException ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error de parseo JSON: {ex.Message}");
            return false;
        }
        catch (IOException ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error de I/O al leer archivo: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error inesperado al cargar JSON: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Aplica filtros maestros a los datos.
    /// </summary>
    /// <param name="searchText">Texto de búsqueda general (busca en nombre/descripción).</param>
    /// <param name="petFriendly">Si true, filtra solo inmuebles pet-friendly.</param>
    public void ApplyFilters(string searchText, bool petFriendly)
    {
        var options = new FilterOptions
        {
            SearchText = searchText,
            PetFriendly = petFriendly
        };
        ApplyFilters(options);
    }

    /// <summary>
    /// Aplica filtros avanzados con múltiples criterios.
    /// </summary>
    public void ApplyFilters(FilterOptions options)
    {
        _filteredData = _originalData.Where(row =>
            MatchesSearchText(row, options.SearchText.Trim().ToLower()) &&
            (!options.PetFriendly || IsPetFriendly(row)) &&
            (options.PropertyType == null || MatchesPropertyType(row, options.PropertyType)) &&
            (!options.MinPrice.HasValue || MatchesMinPrice(row, options.MinPrice.Value)) &&
            (!options.MaxPrice.HasValue || MatchesMaxPrice(row, options.MaxPrice.Value)) &&
            (!options.MinBedrooms.HasValue || MatchesMinBedrooms(row, options.MinBedrooms.Value)) &&
            (!options.MaxBedrooms.HasValue || MatchesMaxBedrooms(row, options.MaxBedrooms.Value)) &&
            (!options.MinRating.HasValue || MatchesMinRating(row, options.MinRating.Value))
        ).ToList();
    }

    /// <summary>
    /// Verifica si una fila coincide con el texto de búsqueda (búsqueda tipo Google).
    /// Busca en columnas como name, description, title, etc.
    /// </summary>
    private bool MatchesSearchText(Dictionary<string, object?> row, string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return true;

        var searchColumns = new[] { "name", "description", "title", "summary", "property_type" };
        
        foreach (var col in searchColumns)
        {
            if (row.TryGetValue(col, out var value) && value != null)
            {
                if (value.ToString()?.ToLower().Contains(searchText) ?? false)
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Detecta si un inmueble es pet-friendly basándose en amenities o house_rules.
    /// Busca palabras clave: "pets", "mascotas", "allowed", "permitido".
    /// </summary>
    private bool IsPetFriendly(Dictionary<string, object?> row)
    {
        var petKeywords = new[] { "pets", "mascotas", "allowed", "permitido" };
        var relevantColumns = new[] { "amenities", "house_rules", "property_description" };

        foreach (var col in relevantColumns)
        {
            if (row.TryGetValue(col, out var value) && value != null)
            {
                var text = value.ToString()?.ToLower() ?? "";
                
                // Si amenities es un array (JArray serializado), procesarlo
                if (value is JArray arr)
                {
                    var arrayText = string.Join(" ", arr.Select(x => x.ToString().ToLower()));
                    if (petKeywords.Any(kw => arrayText.Contains(kw)))
                        return true;
                }
                else if (petKeywords.Any(kw => text.Contains(kw)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Limpia los datos cargados.
    /// </summary>
    public void Clear()
    {
        _originalData.Clear();
        _filteredData.Clear();
        _availableColumns.Clear();
        _availablePropertyTypes.Clear();
    }

    /// <summary>
    /// Verifica si el tipo de propiedad coincide con el filtro.
    /// </summary>
    private bool MatchesPropertyType(Dictionary<string, object?> row, string propertyType)
    {
        if (!row.TryGetValue("property_type", out var value) || value == null)
            return false;

        return value.ToString()?.Equals(propertyType, StringComparison.OrdinalIgnoreCase) ?? false;
    }

    /// <summary>
    /// Verifica si el precio es mayor o igual al mínimo.
    /// </summary>
    private bool MatchesMinPrice(Dictionary<string, object?> row, decimal minPrice)
    {
        if (!TryGetPrice(row, out var price))
            return false;

        return price >= minPrice;
    }

    /// <summary>
    /// Verifica si el precio es menor o igual al máximo.
    /// </summary>
    private bool MatchesMaxPrice(Dictionary<string, object?> row, decimal maxPrice)
    {
        if (!TryGetPrice(row, out var price))
            return false;

        return price <= maxPrice;
    }

    /// <summary>
    /// Obtiene el precio de la propiedad de forma segura.
    /// Busca en columnas comunes: price_per_night, price, cost, etc.
    /// </summary>
    private bool TryGetPrice(Dictionary<string, object?> row, out decimal price)
    {
        price = 0m;
        var priceColumns = new[] { "price_per_night", "price", "cost", "nightly_rate", "rate" };

        foreach (var col in priceColumns)
        {
            if (row.TryGetValue(col, out var value) && value != null)
            {
                // Manejar diferentes tipos numéricos
                if (value is decimal decVal)
                {
                    price = decVal;
                    return true;
                }
                else if (value is int intVal)
                {
                    price = intVal;
                    return true;
                }
                else if (value is long longVal)
                {
                    price = longVal;
                    return true;
                }
                else if (value is double doubleVal)
                {
                    price = (decimal)doubleVal;
                    return true;
                }
                else if (decimal.TryParse(value.ToString(), out var parsedPrice))
                {
                    price = parsedPrice;
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Verifica si el número de dormitorios es mayor o igual al mínimo.
    /// </summary>
    private bool MatchesMinBedrooms(Dictionary<string, object?> row, int minBedrooms)
    {
        if (!TryGetBedrooms(row, out var bedrooms))
            return false;

        return bedrooms >= minBedrooms;
    }

    /// <summary>
    /// Verifica si el número de dormitorios es menor o igual al máximo.
    /// </summary>
    private bool MatchesMaxBedrooms(Dictionary<string, object?> row, int maxBedrooms)
    {
        if (!TryGetBedrooms(row, out var bedrooms))
            return false;

        return bedrooms <= maxBedrooms;
    }

    /// <summary>
    /// Obtiene el número de dormitorios de forma segura.
    /// </summary>
    private bool TryGetBedrooms(Dictionary<string, object?> row, out int bedrooms)
    {
        bedrooms = 0;
        var bedroomColumns = new[] { "bedrooms", "rooms", "bed_rooms", "num_bedrooms" };

        foreach (var col in bedroomColumns)
        {
            if (row.TryGetValue(col, out var value) && value != null)
            {
                // Manejar diferentes tipos numéricos (long, int, decimal, double, etc.)
                if (value is int intVal)
                {
                    bedrooms = intVal;
                    return true;
                }
                else if (value is long longVal)
                {
                    bedrooms = (int)longVal;
                    return true;
                }
                else if (value is decimal decVal)
                {
                    bedrooms = (int)decVal;
                    return true;
                }
                else if (value is double doubleVal)
                {
                    bedrooms = (int)doubleVal;
                    return true;
                }
                else if (int.TryParse(value.ToString(), out var parsedBedrooms))
                {
                    bedrooms = parsedBedrooms;
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Verifica si el rating es mayor o igual al mínimo.
    /// </summary>
    private bool MatchesMinRating(Dictionary<string, object?> row, decimal minRating)
    {
        if (!TryGetRating(row, out var rating))
            return false;

        return rating >= minRating;
    }

    /// <summary>
    /// Obtiene el rating de forma segura.
    /// </summary>
    private bool TryGetRating(Dictionary<string, object?> row, out decimal rating)
    {
        rating = 0m;
        var ratingColumns = new[] { "rating", "review_rating", "score", "stars", "average_rating" };

        foreach (var col in ratingColumns)
        {
            if (row.TryGetValue(col, out var value) && value != null)
            {
                // Manejar diferentes tipos numéricos
                if (value is decimal decVal)
                {
                    rating = decVal;
                    return true;
                }
                else if (value is int intVal)
                {
                    rating = intVal;
                    return true;
                }
                else if (value is long longVal)
                {
                    rating = longVal;
                    return true;
                }
                else if (value is double doubleVal)
                {
                    rating = (decimal)doubleVal;
                    return true;
                }
                else if (decimal.TryParse(value.ToString(), out var parsedRating))
                {
                    rating = parsedRating;
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Obtiene la cantidad de registros filtrados.
    /// </summary>
    public int FilteredRecordCount => _filteredData.Count;

    /// <summary>
    /// Obtiene la cantidad total de registros cargados.
    /// </summary>
    public int TotalRecordCount => _originalData.Count;
}
