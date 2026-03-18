# Gestor Dinámico de Datasets Airbnb - JSON

## 📋 Descripción

Aplicación Windows Forms en C# (.NET 8) que permite gestionar datasets de Airbnb en formato JSON de manera completamente **dinámica**. El programa se adapta automáticamente a cualquier estructura de JSON que el usuario cargue.

## ✨ Características Principales

### 1. **Carga Dinámica de JSON**
- Abre cualquier archivo JSON con estructura flexible
- Soporta arrays de objetos directamente o un objeto único
- Usa Newtonsoft.Json para deserialización robusta
- Normaliza automáticamente la estructura de datos

### 2. **Visualización Adaptativa**
- Genera columnas dinámicamente basadas en las propiedades encontradas en el JSON
- Las columnas se crean automáticamente sin necesidad de configuración previa
- Formatea nombres de columnas legibles (ej: "property_type" → "Property Type")
- Normaliza valores complejos (arrays, objetos anidados) para visualización clara

### 3. **Sistema de Filtrado Maestro en Tiempo Real**

#### 🐾 Filtro Pet Friendly
- Busca automáticamente las palabras clave: "pets", "mascotas", "allowed", "permitido"
- Examina columnas: `amenities`, `house_rules`, `property_description`
- Se activa/desactiva instantáneamente con el CheckBox
- Se desactiva automáticamente si las columnas necesarias no existen en el JSON

#### 🔍 Búsqueda General (Tipo Google)
- Busca en columnas predefinidas: `name`, `description`, `title`, `summary`, `property_type`
- Búsqueda parcial (substring) sin distinción de mayúsculas/minúsculas
- Filtrado en tiempo real mientras escribes

### 4. **Robustez y Manejo de Errores**
- Try-Catch para archivos corruptos o mal formados
- Verifica que el JSON sea válido antes de procesarlo
- Los filtros se adaptan si faltan columnas esperadas
- Mensajes de error claros y amigables para el usuario
- Indicador de estado mostrando registros filtrados vs. totales

## 🛠️ Tecnologías Utilizadas

- **Lenguaje**: C# 12 (características modernas)
- **Framework**: .NET 8 (Windows Forms)
- **JSON Processing**: Newtonsoft.Json v13.0.3
- **Características C#**:
  - File-scoped namespaces
  - Target-typed new expressions
  - Top-level statements
  - Nullable reference types habilitados

## 📁 Estructura del Código

### `JsonDataProcessor.cs`
Clase de soporte que encapsula toda la lógica de procesamiento de datos:
- Carga y deserialización de JSON
- Normalización de estructuras
- Aplicación de filtros
- Mantenimiento de datos originales vs. filtrados

**Métodos principales:**
- `LoadFromJson()` - Carga archivo y detecta columnas
- `ApplyFilters()` - Aplica filtros de búsqueda y pet-friendly
- `AvailableColumns` - Propiedades detectadas en el JSON
- `FilteredData` - Datos actuales filtrados

### `Form1.cs`
Formulario principal con la interfaz de usuario:
- Carga de archivos y generación de columnas
- Actualización del DataGridView en tiempo real
- Gestión de eventos de filtrado
- Normalización de valores para visualización

**Métodos principales:**
- `BtnLoad_Click()` - Carga archivo JSON
- `PopulateDynamicColumns()` - Genera columnas dinámicas
- `RefreshDataGridView()` - Actualiza visualización
- `ApplyFilters()` - Aplica filtros maestros
- `NormalizeValueForDisplay()` - Convierte arrays/objetos a strings legibles

### `Form1.Designer.cs`
Inicialización de controles UI:
- Button: Cargar JSON
- TextBox: Búsqueda general
- CheckBox: Pet Friendly
- DataGridView: Visualización de datos
- OpenFileDialog: Selección de archivo
- Labels: Información de estado

## 🚀 Cómo Usar

### 1. **Cargar un Archivo JSON**
   - Haz clic en el botón "📁 Cargar JSON"
   - Selecciona un archivo `.json` válido
   - El programa cargará automáticamente los datos y generará las columnas

### 2. **Aplicar Filtros**

   **Búsqueda General:**
   - Escribe en el cuadro de búsqueda
   - Los registros se filtran automáticamente
   - Busca por nombre, descripción, tipo de propiedad, etc.

   **Pet Friendly:**
   - Activa el CheckBox "🐾 Pet Friendly"
   - Solo muestra inmuebles que permitan mascotas
   - Combina con búsqueda general para filtrado más específico

### 3. **Visualizar Resultados**
   - El DataGridView muestra los registros filtrados
   - La barra de estado indica cuántos registros se muestran vs. total
   - Las columnas se ajustan automáticamente al contenido

## 📝 Formato JSON Esperado

La aplicación soporta dos formatos:

**Formato 1: Array de objetos (recomendado)**
```json
[
  { "name": "Apartment", "price": 100, "pets": "allowed" },
  { "name": "House", "price": 200, "pets": "not allowed" }
]
```

**Formato 2: Objeto único**
```json
{
  "name": "Apartment",
  "price": 100,
  "pets": "allowed"
}
```

## 📊 Ejemplo de Datos

Se incluye `sample_airbnb.json` con datos de ejemplo para probar la aplicación:
- 5 propiedades diferentes
- Mezcla de propiedades pet-friendly y no pet-friendly
- Diversos tipos de propiedades (Apartment, Villa, Studio, House, Loft)

## 🔧 Instalación y Compilación

### Requisitos
- Visual Studio 2022 (o superior)
- .NET 8 SDK
- NuGet Package Manager

### Pasos
1. Abre el proyecto en Visual Studio
2. Visual Studio restaurará automáticamente los paquetes NuGet (Newtonsoft.Json)
3. Presiona `F5` o `Build > Build Solution`
4. La aplicación se ejecutará

### Línea de comandos
```powershell
# Restaurar paquetes
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run
```

## 💡 Detalles de Implementación

### Normalización de JSON
La clase `JsonDataProcessor` normaliza automáticamente:
- **Arrays**: Se convierten a strings separados por comas (ej: amenities)
- **Objetos anidados**: Se serializan a JSON comprimido
- **Valores nulos**: Se muestran como "-"
- **Booleanos**: Se muestran como "Sí"/"No"

### Filtrado Inteligente
- Los filtros buscan columnas que podrían no existir sin causar errores
- Si una columna esperada (amenities, house_rules) no existe, el filtro sigue funcionando
- La búsqueda es case-insensitive para mejor UX

### Rendimiento
- Los datos originales se mantienen intactos en memoria
- Cada aplicación de filtro crea una nueva lista filtrada
- El DataGridView se regenera completamente en cada filtro (eficiente para datasets pequeños/medianos)

## 🎨 Interfaz de Usuario

- **Tema**: Sistema Windows
- **Controles**:
  - Botón azul "📁 Cargar JSON" - Abre diálogo de archivo
  - TextBox de búsqueda con placeholder - Entrada flexible
  - CheckBox "🐾 Pet Friendly" - Filtro booleano
  - Label de estado - Feedback en tiempo real
  - DataGridView - Visualización completa y escalable

## ⚙️ Configuración Avanzada

### Columnas de Búsqueda (PersonalizableSearchable Columns)
Modificar el método `MatchesSearchText` en `JsonDataProcessor.cs`:
```csharp
var searchColumns = new[] { "name", "description", "title", "summary", "property_type" };
```

### Palabras Clave Pet-Friendly (Personalizables)
Modificar el método `IsPetFriendly` en `JsonDataProcessor.cs`:
```csharp
var petKeywords = new[] { "pets", "mascotas", "allowed", "permitido" };
var relevantColumns = new[] { "amenities", "house_rules", "property_description" };
```

## 📋 Requisitos Cumplidos

✅ Carga dinámica de JSON con OpenFileDialog  
✅ Uso de Newtonsoft.Json para deserialización flexible  
✅ Generación automática de columnas en DataGridView  
✅ Filtrado Pet Friendly inteligente  
✅ Búsqueda general tipo Google  
✅ Manejo robusto de errores  
✅ Código organizado (separación de capas)  
✅ C# moderno (.NET 8, namespaces file-scoped, target-typed new)  
✅ Comentarios explicativos de lógica compleja  
✅ Adaptabilidad a cualquier estructura JSON  

## 🐛 Notas de Depuración

Si experimentas problemas:
1. Verifica que el JSON sea válido usando un validador online
2. Asegúrate de que el archivo esté codificado en UTF-8
3. Comprueba que el JSON sea un array o un objeto (no un string primitivo)
4. Los mensajes de error se mostrarán en ventanas emergentes

## 📄 Licencia

Código desarrollado para demostración educativa.

---

**Desarrollado con ❤️ en C# .NET 8**
