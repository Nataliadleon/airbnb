# 👨‍💻 Referencia para Desarrolladores

## 🎯 Inicio Rápido para Desarrolladores

### Estructura del Proyecto
```
airbnb/
├── Form1.cs                    # Lógica principal (240 líneas)
├── Form1.Designer.cs           # UI (130 líneas)
├── JsonDataProcessor.cs        # Lógica de datos (180 líneas)
├── airbnb.csproj               # Configuración proyecto
├── Program.cs                  # Entry point (auto-generado)
├── sample_airbnb.json          # Datos de ejemplo
└── /obj                        # Archivos compilados
```

---

## 🔧 Clase Principal: JsonDataProcessor

### Responsabilidad
Encapsular toda la lógica de procesamiento de JSON, separada de la UI.

### Métodos Públicos

#### LoadFromJson(filePath: string) → bool
```csharp
// Carga archivo JSON y detecta columnas automáticamente
var success = _processor.LoadFromJson("ruta/archivo.json");
if (!success) MessageBox.Show("Error al cargar");
```
**Maneja:**
- Lectura de archivo
- Deserialización JSON
- Conversión a array si es objeto único
- Detección de todas las propiedades

#### ApplyFilters(searchText: string, petFriendly: bool)
```csharp
// Aplica filtros maestros
_processor.ApplyFilters(txtSearch.Text, chkPetFriendly.Checked);
RefreshDataGridView(); // Actualizar UI después
```

#### Propiedades Públicas

```csharp
IEnumerable<string> AvailableColumns    // Columnas detectadas
List<Dictionary<string, object?>> FilteredData  // Datos filtrados
int FilteredRecordCount                 // Registros visibles
int TotalRecordCount                    // Total de registros
```

### Métodos Privados (Lógica de Negocio)

#### MatchesSearchText(row, searchText) → bool
**Objetivo:** Filtrado por búsqueda general

```csharp
// Busca en columnas: name, description, title, summary, property_type
private bool MatchesSearchText(Dictionary<string, object?> row, string searchText)
{
    if (string.IsNullOrWhiteSpace(searchText)) return true;
    
    var searchColumns = new[] { "name", "description", "title", "summary", "property_type" };
    foreach (var col in searchColumns)
    {
        if (row.TryGetValue(col, out var value) && value?.ToString()?.ToLower().Contains(searchText) ?? false)
            return true;
    }
    return false;
}
```

**Personalizar:**
- Agregar/remover columnas en `searchColumns`
- Cambiar lógica de búsqueda (substring, regex, etc.)

#### IsPetFriendly(row) → bool
**Objetivo:** Detectar propiedades pet-friendly

```csharp
// Busca palabras clave en amenities, house_rules
private bool IsPetFriendly(Dictionary<string, object?> row)
{
    var petKeywords = new[] { "pets", "mascotas", "allowed", "permitido" };
    var relevantColumns = new[] { "amenities", "house_rules", "property_description" };
    
    foreach (var col in relevantColumns)
    {
        if (row.TryGetValue(col, out var value) && value != null)
        {
            // ... buscar palabras clave
        }
    }
    return false;
}
```

**Personalizar:**
- Agregar palabras clave: `new[] { "pets", "dog", "cat", ... }`
- Cambiar columnas relevantes
- Usar regex para búsqueda más sofisticada

---

## 🖥️ Clase Principal: Form1

### Responsabilidad
Gestionar la interfaz gráfica y coordinador entre usuario y JsonDataProcessor.

### Flujo de Eventos

```
Usuario hace clic en "📁 Cargar JSON"
    ↓
BtnLoad_Click() se ejecuta
    ↓
OpenFileDialog abre
    ↓
Usuario selecciona archivo
    ↓
JsonDataProcessor.LoadFromJson() carga datos
    ↓
PopulateDynamicColumns() genera columnas
    ↓
RefreshDataGridView() muestra datos
    ↓
UpdateStatus() actualiza estado
```

### Métodos Principales

#### PopulateDynamicColumns()
**Genera columnas basadas en propiedades del JSON**

```csharp
private void PopulateDynamicColumns()
{
    dgvData.Columns.Clear();
    
    // Columna de índice
    dgvData.Columns.Add(new DataGridViewTextBoxColumn 
    { 
        Name = "Index", 
        HeaderText = "#", 
        Width = 40 
    });
    
    // Columnas dinámicas
    foreach (var column in _processor.AvailableColumns.OrderBy(x => x))
    {
        var textColumn = new DataGridViewTextBoxColumn
        {
            Name = column,
            HeaderText = FormatColumnName(column),
            Width = 150,
            ReadOnly = true,
            Resizable = DataGridViewTriState.True
        };
        dgvData.Columns.Add(textColumn);
    }
}
```

**Personalizar:**
- Cambiar ancho de columnas: `Width = 200`
- Ocultar columnas específicas: agregar lógica `if (column != "id")`
- Cambiar orden: reemplazar `OrderBy(x => x)` por lógica personalizada

#### RefreshDataGridView()
**Actualiza tabla con datos filtrados**

```csharp
private void RefreshDataGridView()
{
    dgvData.Rows.Clear();
    int rowIndex = 0;
    
    foreach (var row in _processor.FilteredData)
    {
        var cells = new DataGridViewCell[dgvData.Columns.Count];
        cells[0] = new DataGridViewTextBoxCell { Value = rowIndex + 1 };
        
        int columnIndex = 1;
        foreach (var column in _processor.AvailableColumns.OrderBy(x => x))
        {
            var value = row.TryGetValue(column, out var v) ? v : null;
            cells[columnIndex] = new DataGridViewTextBoxCell 
            { 
                Value = NormalizeValueForDisplay(value) 
            };
            columnIndex++;
        }
        
        dgvData.Rows.Add(cells);
        rowIndex++;
    }
}
```

#### NormalizeValueForDisplay(value: object?) → string
**Convierte valores complejos a strings legibles**

```csharp
private string NormalizeValueForDisplay(object? value)
{
    return value switch
    {
        null => "-",
        string str => str,
        bool b => b ? "Sí" : "No",
        Newtonsoft.Json.Linq.JArray arr => 
            string.Join(", ", arr.Select(x => x.ToString().Trim('"'))),
        Newtonsoft.Json.Linq.JObject obj => 
            Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None),
        _ => value.ToString() ?? "-"
    };
}
```

**Casos manejados:**
- `null` → "-" (legible)
- `string` → como es
- `bool` → "Sí"/"No" (humanizado)
- `JArray` → "item1, item2, item3" (lista legible)
- `JObject` → JSON comprimido
- Otros → string normal

#### ApplyFilters()
**Coordina la aplicación de filtros**

```csharp
private void ApplyFilters()
{
    if (_processor.TotalRecordCount == 0) return;
    
    _processor.ApplyFilters(txtSearch.Text, chkPetFriendly.Checked);
    RefreshDataGridView();
    UpdateStatus();
}
```

#### UpdateStatus()
**Actualiza label de estado con información**

```csharp
private void UpdateStatus()
{
    var total = _processor.TotalRecordCount;
    var filtered = _processor.FilteredRecordCount;
    
    if (total == 0)
    {
        lblStatus.Text = "Cargue un archivo JSON para comenzar";
        lblStatus.ForeColor = Color.Gray;
    }
    else if (filtered == total)
    {
        lblStatus.Text = $"Mostrando {filtered} de {total} registros";
        lblStatus.ForeColor = Color.Green;
    }
    else
    {
        lblStatus.Text = $"Mostrando {filtered} de {total} registros (filtrados)";
        lblStatus.ForeColor = Color.Orange;
    }
}
```

### Event Handlers

```csharp
// Evento del botón Cargar
private void BtnLoad_Click(object? sender, EventArgs e) { ... }

// Evento de búsqueda en tiempo real
private void TxtSearch_TextChanged(object? sender, EventArgs e)
{
    if (!_isInitialized) return;
    ApplyFilters();
}

// Evento de Pet Friendly
private void ChkPetFriendly_CheckedChanged(object? sender, EventArgs e)
{
    if (!_isInitialized) return;
    ApplyFilters();
}
```

---

## 🎨 Controles en Form1.Designer.cs

### Declaraciones
```csharp
private Button btnLoad = null!;           // Cargar archivo
private TextBox txtSearch = null!;        // Búsqueda general
private CheckBox chkPetFriendly = null!;  // Pet Friendly filter
private Label lblSearch = null!;          // Etiqueta "Búsqueda"
private Label lblStatus = null!;          // Información de registros
private DataGridView dgvData = null!;     // Tabla de datos
private OpenFileDialog openFileDialog = null!;  // Diálogo archivo
```

### Propiedades de Controles
```csharp
btnLoad.BackColor = Color.DodgerBlue;     // Color azul
btnLoad.ForeColor = Color.White;          // Texto blanco

txtSearch.PlaceholderText = "Buscar...";  // Hint de texto

dgvData.AutoGenerateColumns = false;      // Generación manual
dgvData.ReadOnly = true;                  // Datos de solo lectura

openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
```

---

## 📦 Dependencias y Paquetes NuGet

### Paquetes Requeridos
```xml
<ItemGroup>
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
</ItemGroup>
```

### Importaciones Clave
```csharp
using Newtonsoft.Json.Linq;    // JToken, JArray, JObject
using System.Collections.Generic;
using System.Linq;
```

### Métodos Newtonsoft.Json Usados
```csharp
JToken.Parse(json)              // Parsear JSON genérico
JObject.ToObject<T>()           // Convertir a Dictionary
JsonConvert.SerializeObject()   // Serializar objetos
```

---

## 🧪 Patrones de Diseño Implementados

### Pattern: Separation of Concerns
- **JsonDataProcessor**: Lógica de datos
- **Form1**: Lógica de UI
- **Form1.Designer**: Definición de controles

### Pattern: Data Normalization
Conversión de formatos diversos a formato estándar:
```csharp
JArray → Dictionary[] → UI
JObject → Dictionary[] → UI
```

### Pattern: Defensive Programming
```csharp
// Usar TryGetValue en lugar de acceso directo
if (row.TryGetValue(column, out var value))
{
    // value es seguro
}
```

### Pattern: Filter Pipeline
```
Datos originales
    ↓ Filtro 1: Búsqueda general
    ↓ Filtro 2: Pet Friendly
    ↓ Resultado: datos filtrados
```

---

## 🔍 Debugging Tips

### Ver datos cargados
```csharp
var data = _processor.FilteredData;
Debug.WriteLine($"Registros: {data.Count}");
foreach (var col in _processor.AvailableColumns)
{
    Debug.WriteLine($"Columna: {col}");
}
```

### Verificar filtrado
```csharp
var before = _processor.TotalRecordCount;
_processor.ApplyFilters("test", false);
var after = _processor.FilteredRecordCount;
Debug.WriteLine($"Antes: {before}, Después: {after}");
```

### Inspeccionar JSON
```csharp
var token = JToken.Parse(json);
Debug.WriteLine($"Tipo: {token.Type}");  // Array, Object, etc.
Debug.WriteLine($"Contenido: {token.ToString()}");
```

---

## 🚀 Extensiones Posibles

### 1. Agregar Nuevo Filtro
```csharp
// En JsonDataProcessor.cs, agregar método:
private bool MatchesPriceRange(Dictionary<string, object?> row, decimal min, decimal max)
{
    if (row.TryGetValue("price_per_night", out var value))
    {
        if (decimal.TryParse(value.ToString(), out var price))
            return price >= min && price <= max;
    }
    return false;
}

// En Form1.cs, agregar controles y llamar método
```

### 2. Exportar a CSV
```csharp
private void ExportToCsv()
{
    var csv = new StringBuilder();
    
    // Headers
    var headers = string.Join(",", _processor.AvailableColumns);
    csv.AppendLine(headers);
    
    // Rows
    foreach (var row in _processor.FilteredData)
    {
        var values = _processor.AvailableColumns
            .Select(col => row.TryGetValue(col, out var v) ? v?.ToString() : "");
        csv.AppendLine(string.Join(",", values));
    }
    
    File.WriteAllText("export.csv", csv.ToString());
}
```

### 3. Guardar Filtros
```csharp
private void SaveFilter(string filterName)
{
    var settings = new
    {
        Name = filterName,
        SearchText = txtSearch.Text,
        PetFriendly = chkPetFriendly.Checked
    };
    
    File.WriteAllText($"{filterName}.json", 
        JsonConvert.SerializeObject(settings));
}
```

---

## 📋 Checklist de Cambios

Cuando hagas modificaciones:

- [ ] Editar `JsonDataProcessor.cs` para cambios de lógica
- [ ] Editar `Form1.cs` para cambios de UI
- [ ] Compilar con `Ctrl+Shift+B`
- [ ] Probar con `F5`
- [ ] Verificar en Visual Studio Output window (Debug pane)
- [ ] Agregar comentarios XML si es lógica compleja
- [ ] Actualizar documentación si corresponde

---

## 📚 Referencias Rápidas

### Newtonsoft.Json
- Docs: https://www.newtonsoft.com/json/help
- JToken: Clase base para JSON
- JArray: Array de tokens
- JObject: Objeto clave-valor

### DataGridView
- Docs: Microsoft Learn DataGridView
- Agregar columna: `dgvData.Columns.Add(new DataGridViewColumn())`
- Agregar fila: `dgvData.Rows.Add(cell1, cell2, ...)`
- Limpiar: `dgvData.Rows.Clear()` y `dgvData.Columns.Clear()`

### LINQ
- `OrderBy()`, `Where()`, `Select()`, `ToList()`
- En este proyecto: Filtrado y transformación de datos

---

## 🎯 Quick Commands

```powershell
# Compilar
dotnet build

# Ejecutar
dotnet run

# Limpiar binarios
dotnet clean

# Ver versión .NET
dotnet --version

# Restaurar NuGet
dotnet restore
```

---

**¡Documentación de referencia completa!** 📚
